using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using System.Threading;
using CPF.CefGlue.JSExtenstions;
using CPF.Controls;
using CPF.Reflection;

namespace CPF.CefGlue
{
    public class CpfCefClient : CefClient
    {
        public WebBrowser Owner { get; private set; }

        private readonly MessageDispatcher _messageDispatcher = new MessageDispatcher();
        public MessageDispatcher Dispatcher => _messageDispatcher;

        //private CpfCefLifeSpanHandler _lifeSpanHandler;
        //private CpfCefDisplayHandler _displayHandler;
        //private CpfCefRenderHandler _renderHandler;
        //private CpfCefLoadHandler _loadHandler;
        //private CpfCefJSDialogHandler _jsDialogHandler;

        public CpfCefClient(WebBrowser browser)
        {
            Owner = browser;
            //if (owner == null) throw new ArgumentNullException("owner");

            //_owner = owner;

            //_lifeSpanHandler = new CpfCefLifeSpanHandler(owner);
            //_displayHandler = new CpfCefDisplayHandler(owner);
            //_renderHandler = new CpfCefRenderHandler(owner);
            //_loadHandler = new CpfCefLoadHandler(owner);
            //_jsDialogHandler = new CpfCefJSDialogHandler();
        }
#if Net4
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (message.Name == "CallerCSharp")
            {
                OnCallCSharp(message);
            }
            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
#else
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (message.Name == "CallerCSharp")
            {
                OnCallCSharp(message);
            }
            else if (message.Name == "CreateProcessMessage")
            {
                OnRenderProcessThreadCreated(message.Arguments.GetString(0));
            }
            else if (message.Name == "OnFocusedNodeChanged")
            {
                if (WebBrowser.browsers.TryGetValue(browser.Identifier, out var webBrowser))
                {
                    webBrowser.OnFocusedNodeChanged(message.Arguments.GetInt(0), message.Arguments.GetInt(1));
                }
            }
            _messageDispatcher.DispatchMessage(browser, frame, sourceProcess, message);
            return base.OnProcessMessageReceived(browser, frame, sourceProcess, message);
        }
#endif
        void OnRenderProcessThreadCreated(string name)
        {
            //string name = Guid.NewGuid().ToString();
            //extraInfo.SetString("messageId", name);
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", name + "Render", PipeDirection.Out);
            CpfCefApp.App.BrowserProcessHandler.clients.TryAdd(name, new BinaryWriter(pipeClient, Encoding.Unicode));
            ThreadPool.QueueUserWorkItem(a => pipeClient.Connect());

            new Thread(n =>
            {
                var serverStream = new NamedPipeServerStream((string)n + "Browser", PipeDirection.In, 1);
                CpfCefApp.App.BrowserProcessHandler.servers.TryAdd((string)n, serverStream);
                serverStream.WaitForConnection();

                using (BinaryReader br = new BinaryReader(serverStream, Encoding.Unicode))
                {
                    List<(ValueType type, Type ctype, object value)> values = new List<(ValueType, Type, object)>();
                    string callback = null;
                    while (true)
                    {
                        ValueType type;
                        try
                        {
                            type = (ValueType)br.ReadByte();
                        }
                        catch (Exception e)
                        {
                            CpfCefApp.App.BrowserProcessHandler.clients.TryRemove((string)n, out _);
                            CpfCefApp.App.BrowserProcessHandler.servers.TryRemove((string)n, out _);
                            //Debug.WriteLine("断开与渲染进程的连接："+e.ToString());
                            return;
                        }
                        switch (type)
                        {
                            case ValueType.Undefined:
                                values.Add((type, typeof(string), null));
                                break;
                            case ValueType.Null:
                                values.Add((type, typeof(string), null));
                                break;
                            case ValueType.Bool:
                                values.Add((type, typeof(bool), br.ReadBoolean()));
                                break;
                            case ValueType.Date:
                                values.Add((type, typeof(DateTime), new DateTime(br.ReadInt64(), DateTimeKind.Utc)));
                                break;
                            case ValueType.Double:
                                values.Add((type, typeof(double), br.ReadDouble()));
                                break;
                            case ValueType.Int:
                                values.Add((type, typeof(int), br.ReadInt32()));
                                break;
                            case ValueType.String:
                                values.Add((type, typeof(string), br.ReadStringEx()));
                                break;
                            case ValueType.CallBack:
                                callback = br.ReadStringEx();
                                break;
                            case ValueType.End:
                                var rType = ValueType.Undefined;
                                object r = null;
                                if (WebBrowser.browsers.TryGetValue((int)values[0].value, out var browser))
                                {
                                    object global = null;
                                    global = browser.CommandContext;
                                    var fn = (string)values[1].value;
                                    if (global != null && !string.IsNullOrWhiteSpace(fn))
                                    {
                                        var method = global.GetType().GetMethod(fn);
                                        if (method != null)
                                        {
                                            var jsf = method.GetCustomAttributes(typeof(JSFunction), true);
                                            if (jsf != null && jsf.Length > 0)
                                            {
                                                var ps = method.GetParameters();
                                                if (ps.Length != values.Count - 2)
                                                {
                                                    throw new Exception("方法" + fn + "参数数量不对");
                                                }
                                                var list = new List<object>();
                                                for (int i = 0; i < ps.Length; i++)
                                                {
                                                    var item = ps[i];
                                                    var pt = item.ParameterType;
                                                    if (pt != typeof(bool) && pt != typeof(int) && pt != typeof(string) && pt != typeof(DateTime) && pt != typeof(double))
                                                    {
                                                        throw new Exception("方法" + fn + "参数类型不支持" + pt + "，必须是int bool string double DateTime这几种类型");
                                                    }
                                                    if (values[i + 2].ctype != pt)
                                                    {
                                                        throw new Exception("方法" + fn + "参数类型不对" + pt);
                                                    }
                                                    list.Add(values[i + 2].value);
                                                }
                                                var pt1 = method.ReturnType;
                                                if (pt1 == typeof(bool))
                                                {
                                                    rType = ValueType.Bool;
                                                }
                                                else if (pt1 == typeof(int))
                                                {
                                                    rType = ValueType.Int;
                                                }
                                                //else if (pt1 == typeof(uint))
                                                //{
                                                //    rType = ValueType.UInt;
                                                //}
                                                else if (pt1 == typeof(string))
                                                {
                                                    rType = ValueType.String;
                                                }
                                                else if (pt1 == typeof(DateTime))
                                                {
                                                    rType = ValueType.Date;
                                                }
                                                else if (pt1 == typeof(double))
                                                {
                                                    rType = ValueType.Double;
                                                }
                                                else if (pt1 != typeof(void))
                                                {
                                                    throw new Exception("方法" + fn + "返回值类型不支持" + pt1);
                                                }
                                                if (callback == null)
                                                {
                                                    browser.Invoke(() =>
                                                    {
                                                        r = method.FastInvoke(global, list.ToArray());
                                                    });
                                                }
                                                else
                                                {
                                                    new Thread(a =>
                                                    {
                                                        var item = (ValueTuple<string, ValueType, object, List<object>, MethodInfo, string>)a;
                                                        var cr = item.Item5.FastInvoke(item.Item3, item.Item4.ToArray());

                                                        if (CpfCefApp.App.BrowserProcessHandler.clients.TryGetValue(item.Item6, out var bw))
                                                        {
                                                            try
                                                            {
                                                                bw.Write((byte)ValueType.CallBack);
                                                                bw.Write(item.Item1);
                                                                SetResult(item.Item2, cr, bw);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                Debug.WriteLine("JS调用C#进程中断：" + e);
                                                                Console.WriteLine("JS调用C#进程中断：" + e);
                                                            }
                                                        }
                                                    })
                                                    { Name = "JS异步调用C#", IsBackground = true }.Start((callback, rType, global, list, method, (string)n));
                                                }
                                                if (rType == ValueType.String && r == null)
                                                {
                                                    rType = ValueType.Null;
                                                }
                                            }
                                        }

                                    }
                                }
                                if (CpfCefApp.App.BrowserProcessHandler.clients.TryGetValue((string)n, out var binaryWriter))
                                {
                                    if (callback != null)
                                    {
                                        SetResult(ValueType.Undefined, null, binaryWriter);
                                    }
                                    else
                                    {
                                        SetResult(rType, r, binaryWriter);
                                    }
                                }
                                values.Clear();
                                callback = null;
                                break;
                        }
                    }
                }
            })
            { IsBackground = true, Name = "JS调用C#" }.Start(name);
        }

        private static void SetResult(ValueType rType, object r, BinaryWriter binaryWriter)
        {
            try
            {
                binaryWriter.Write((byte)rType);
                if (rType != ValueType.Null && rType != ValueType.Undefined)
                {
                    switch (rType)
                    {
                        case ValueType.Bool:
                            binaryWriter.Write((bool)r);
                            break;
                        case ValueType.Date:
                            binaryWriter.Write(((DateTime)r).Ticks);
                            break;
                        case ValueType.Double:
                            binaryWriter.Write((double)r);
                            break;
                        case ValueType.Int:
                            binaryWriter.Write((int)r);
                            break;
                        case ValueType.String:
                            binaryWriter.WriteStringEx((string)r);
                            break;
                            //case ValueType.UInt:
                            //    binaryWriter.Write((uint)r);
                            //    break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("JS调用C#进程中断：" + e);
                Console.WriteLine("JS调用C#进程中断：" + e);
            }
        }
        /// <summary>
        /// 接受JS的返回结果
        /// </summary>
        /// <param name="message"></param>
        void OnCallCSharp(CefProcessMessage message)
        {
            var guid = message.Arguments.GetValue(0).GetString();
            if (CefExtenstions.EJSMessages.TryGetValue(guid, out var msg))
            {
                var type = (ValueType)message.Arguments.GetValue(1).GetInt();
                switch (type)
                {
                    case ValueType.Bool:
                        msg.Result = message.Arguments.GetValue(2).GetBool();
                        break;
                    case ValueType.Date:
                        msg.Result = new DateTime(long.Parse(message.Arguments.GetValue(2).GetString()), DateTimeKind.Utc);
                        break;
                    case ValueType.Double:
                        msg.Result = message.Arguments.GetValue(2).GetDouble();
                        break;
                    case ValueType.Int:
                        msg.Result = message.Arguments.GetValue(2).GetInt();
                        break;
                    case ValueType.String:
                        msg.Result = message.Arguments.GetValue(2).GetString();
                        break;
                }
                msg.ManualReset.Set();
            }
        }

        void SendProcessMessage(CefBrowser _browser, CefProcessId targetProcess, CefProcessMessage message)
        {
#if Net4
            _browser.SendProcessMessage(targetProcess, message);
#else
            _browser.GetMainFrame().SendProcessMessage(targetProcess, message);
#endif
        }
        public CpfCefLifeSpanHandler LifeSpanHandler { get; set; }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return LifeSpanHandler;
        }
        public CpfCefDisplayHandler DisplayHandler { get; set; }
        protected override CefDisplayHandler GetDisplayHandler()
        {
            return DisplayHandler;
        }
        public CpfCefRenderHandler RenderHandler { get; set; }
        protected override CefRenderHandler GetRenderHandler()
        {
            return RenderHandler;
        }
        public CpfCefLoadHandler LoadHandler { get; set; }
        protected override CefLoadHandler GetLoadHandler()
        {
            return LoadHandler;
        }
        public CpfCefJSDialogHandler JSDialogHandler { get; set; }
        protected override CefJSDialogHandler GetJSDialogHandler()
        {
            return JSDialogHandler;
        }
        public CpfCefContextMenuHandler ContextMenuHandler { get; set; }
        protected override CefContextMenuHandler GetContextMenuHandler()
        {
            return ContextMenuHandler;
        }
        public CpfCefDialogHandler DialogHandler { get; set; }
        protected override CefDialogHandler GetDialogHandler()
        {
            return DialogHandler;
        }
        public CpfCefDownloadHandler DownloadHandler { get; set; }
        protected override CefDownloadHandler GetDownloadHandler()
        {
            return DownloadHandler;
        }
        public CpfCefDragHandler DragHandler { get; set; }
        protected override CefDragHandler GetDragHandler()
        {
            return DragHandler;
        }
        public CpfCefFindHandler FindHandler { get; set; }
        protected override CefFindHandler GetFindHandler()
        {
            return FindHandler;
        }
        public CpfCefFocusHandler FocusHandler { get; set; }
        protected override CefFocusHandler GetFocusHandler()
        {
            return FocusHandler;
        }
        public CpfCefKeyboardHandler KeyboardHandler { get; set; }
        protected override CefKeyboardHandler GetKeyboardHandler()
        {
            return KeyboardHandler;
        }
        public CpfCefRequestHandler RequestHandler { get; set; }
        protected override CefRequestHandler GetRequestHandler()
        {
            return RequestHandler;
        }

        public CefRequestContext RequestContext { get; set; }
    }
}
