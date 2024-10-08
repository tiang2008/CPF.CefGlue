using CPF.CefGlue.Wrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using CPF.Controls;
using System.Linq;
using CPF.CefGlue.JSExtenstions;

namespace CPF.CefGlue
{
    public class CpfCefRenderProcessHandler : CefRenderProcessHandler
    {
        public CpfCefRenderProcessHandler()
        {
            Cef.RenderProcessHandler = this;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }
        JsObject Cef = new JsObject();
        protected override void OnWebKitInitialized()
        {
            CefRuntime.RegisterExtension(Cef.GetType().Name, "if (!$csharpCaller) var $csharpCaller = { };(function () {$csharpCaller.$CallCSharp = function (name,args) {native function $CallCSharp(); return $CallCSharp(name,args);};})();", Cef);

            _javascriptExecutionEngine = new JavascriptExecutionEngineRenderSide(_messageDispatcher);
            _javascriptToNativeDispatcher = new JavascriptToNativeDispatcherRenderSide(_messageDispatcher);
        }
        NamedPipeServerStream serverStream;
        internal BinaryWriter clientStream;
        string name;

#if Net4
        protected override void OnRenderThreadCreated(CefListValue extraInfo)
        {
            name = extraInfo.GetValue(0).GetString();
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", name + "Browser", PipeDirection.Out);
            clientStream = new BinaryWriter(pipeClient, Encoding.Unicode);
            ThreadPool.QueueUserWorkItem(a => pipeClient.Connect());
            new Thread(() =>
            {
                serverStream = new NamedPipeServerStream(name + "Render", PipeDirection.In, 1);
                serverStream.WaitForConnection();
                using (BinaryReader br = new BinaryReader(serverStream, Encoding.Unicode))
                {
                    string callback = null;
                    while (true)
                    {
                        var type = (ValueType)br.ReadByte();
                        object result = null;
                        switch (type)
                        {
                            case ValueType.Undefined:
                                break;
                            case ValueType.Null:
                                break;
                            case ValueType.Bool:
                                result = br.ReadBoolean();
                                break;
                            case ValueType.Date:
                                result = new DateTime(br.ReadInt64(), DateTimeKind.Utc);
                                break;
                            case ValueType.Double:
                                result = br.ReadDouble();
                                break;
                            case ValueType.Int:
                                result = br.ReadInt32();
                                break;
                            case ValueType.String:
                                result = br.ReadStringEx();
                                break;
                            case ValueType.CallBack:
                                callback = br.ReadStringEx();
                                continue;
                            default:
                                break;
                        }
                        if (callback != null)
                        {
                            if (Cef.v8Context.TryRemove(callback, out var v8Context))
                            {
                                var cb = callback;
                                //try
                                //{
                                Helpers.PostTask(CefThreadId.Renderer, () =>
                                {
                                    v8Context.Enter();
                                    var fun = v8Context.GetGlobal().GetValue(cb);
                                    if (fun.IsFunction)
                                    {
                                        fun.ExecuteFunction(v8Context.GetGlobal(), new CefV8Value[] { JsObject.ConvertValue(result, type) });
#if Net4
                                        v8Context.TryEval("delete window." + cb, out _, out _);
#else
                                        v8Context.TryEval("delete window." + cb, "", 0, out _, out _);
#endif
                                    }
                                    v8Context.Exit();
                                });
                                //}
                                //catch (Exception e)
                                //{
                                //    File.AppendAllText("test.txt", e + "\r\n");
                                //}
                            }
                            callback = null;
                        }
                        else
                        {
                            Cef.Result = result;
                            Cef.ResultType = type;
                            Cef.ManualResetEvent.Set();
                        }
                    }
                }
            })
            { IsBackground = true, Name = "接受消息" }.Start();
        }
#endif
        public static ConcurrentDictionary<int, CefBrowser> browsers = new ConcurrentDictionary<int, CefBrowser>();
        protected override void OnBrowserCreated(CefBrowser browser, CefDictionaryValue extraInfo)
        {
            //_browser = browser;
            browsers.TryAdd(browser.Identifier, browser);
        }

        protected override void OnBrowserDestroyed(CefBrowser browser)
        {
            browsers.TryRemove(browser.Identifier, out _);
        }

#if Net4
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (message.Name == "CharpCallJS")
            {
                OnCallJS(browser, message);
            }
            WithErrorHandling(() =>
            {
                using (CefObjectTracker.StartTracking())
                {
                    _messageDispatcher.DispatchMessage(browser, frame, sourceProcess, message);
                }
            }, frame);
            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
#else
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (message.Name == "CharpCallJS")
            {
                OnCallJS(browser, message);
            }
            WithErrorHandling(() =>
            {
                using (CefObjectTracker.StartTracking())
                {
                    _messageDispatcher.DispatchMessage(browser, frame, sourceProcess, message);
                }
            }, frame);
            return base.OnProcessMessageReceived(browser, frame, sourceProcess, message);
        }
#endif

        void OnCallJS(CefBrowser browser, CefProcessMessage message)
        {
            var guid = message.Arguments.GetValue(3).GetString();
            var msg = CefProcessMessage.Create("CallerCSharp");
            msg.Arguments.SetString(0, guid);

            //if (browsers.TryGetValue(message.Arguments.GetValue(0).GetInt(), out var browser))
            //{
            var frame = browser.GetFrame(long.Parse(message.Arguments.GetValue(1).GetString()));
            if (frame != null)
            {
                try
                {
#if Net4
                frame.V8Context.TryEval(message.Arguments.GetValue(2).GetString(), out var returnValue, out _);
#else
                    if (!frame.V8Context.TryEval(message.Arguments.GetValue(2).GetString(), "", 0, out var returnValue, out var exception))
                    {
                        var m = exception.Message;
                        if (!string.IsNullOrEmpty(m))
                        {
                            frame.V8Context.TryEval("console.error('" + m.Replace("'", "\'") + "')", "", 0, out _, out _);
                        }
                    }
#endif
                    if (returnValue.IsBool)
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.Bool);
                        msg.Arguments.SetBool(2, returnValue.GetBoolValue());
                    }
                    else if (returnValue.IsDate)
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.Date);
                        msg.Arguments.SetString(2, returnValue.GetDateValue().Ticks.ToString());
                    }
                    else if (returnValue.IsDouble)
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.Double);
                        msg.Arguments.SetDouble(2, returnValue.GetDoubleValue());
                    }
                    else if (returnValue.IsString)
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.String);
                        msg.Arguments.SetString(2, returnValue.GetStringValue());
                    }
                    else if (returnValue.IsInt)
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.Int);
                        msg.Arguments.SetInt(2, returnValue.GetIntValue());
                    }
                    else
                    {
                        msg.Arguments.SetInt(1, (int)ValueType.Null);
                    }
                }
                catch (Exception e)
                {
                    msg.Arguments.SetInt(1, (int)ValueType.Null);
                }
            }
            //}
            SendProcessMessage(browser, CefProcessId.Browser, msg);
        }

        void SendProcessMessage(CefBrowser _browser, CefProcessId targetProcess, CefProcessMessage message)
        {
#if Net4
            _browser.SendProcessMessage(targetProcess, message);
#else
            _browser.GetMainFrame().SendProcessMessage(targetProcess, message);
#endif
        }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            //进程通讯是为了可以JS和C#互相调用，如果只用Cef内置SendProcessMessage容易出现死锁，不能同时发送和接收
            try
            {
                //var keys = extraInfo.GetKeys();
                ////File.AppendAllText(Path.Combine(CPF.Platform.Application.StartupPath, "cef.log"), string.Join(",", keys) + "\r\n");
                //if (keys == null || !keys.Any(a => a == "messageId"))
                //{
                //    return;
                //}
                //var message = extraInfo.GetValue("messageId");
                //name = message.GetString();
                name = Guid.NewGuid().ToString();
                CefProcessMessage message = CefProcessMessage.Create("CreateProcessMessage");
                message.Arguments.SetString(0, name);
                SendProcessMessage(browser, CefProcessId.Browser, message);
                //File.AppendAllText(Path.Combine(CPF.Platform.Application.StartupPath, "cef.log"), name + "\r\n");
                NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", name + "Browser", PipeDirection.Out);
                clientStream = new BinaryWriter(pipeClient, Encoding.Unicode);
                ThreadPool.QueueUserWorkItem(a =>
                {
                    try
                    {
                        pipeClient.Connect();
                    }
                    catch (Exception e)
                    {
                        //File.AppendAllText(Path.Combine(CPF.Platform.Application.StartupPath, "cef.log"), DateTime.Now + ":" + e.ToString() + "\r\n");
                    }
                });
                new Thread(() =>
                {
                    try
                    {
                        serverStream = new NamedPipeServerStream(name + "Render", PipeDirection.In, 1);
                        serverStream.WaitForConnection();
                        using (BinaryReader br = new BinaryReader(serverStream, Encoding.Unicode))
                        {
                            string callback = null;
                            while (true)
                            {
                                var type = (ValueType)br.ReadByte();
                                object result = null;
                                switch (type)
                                {
                                    case ValueType.Undefined:
                                        break;
                                    case ValueType.Null:
                                        break;
                                    case ValueType.Bool:
                                        result = br.ReadBoolean();
                                        break;
                                    case ValueType.Date:
                                        result = new DateTime(br.ReadInt64(), DateTimeKind.Utc);
                                        break;
                                    case ValueType.Double:
                                        result = br.ReadDouble();
                                        break;
                                    case ValueType.Int:
                                        result = br.ReadInt32();
                                        break;
                                    case ValueType.String:
                                        result = br.ReadStringEx();
                                        break;
                                    case ValueType.CallBack:
                                        callback = br.ReadStringEx();
                                        continue;
                                    default:
                                        break;
                                }
                                if (callback != null)
                                {
                                    if (Cef.v8Context.TryRemove(callback, out var v8Context))
                                    {
                                        var cb = callback;
                                        //try
                                        //{
                                        Helpers.PostTask(CefThreadId.Renderer, () =>
                                        {
                                            v8Context.Enter();
                                            var fun = v8Context.GetGlobal().GetValue(cb);
                                            if (fun.IsFunction)
                                            {
                                                fun.ExecuteFunction(v8Context.GetGlobal(), new CefV8Value[] { JsObject.ConvertValue(result, type) });
#if Net4
                                        v8Context.TryEval("delete window." + cb, out _, out _);
#else
                                                v8Context.TryEval("delete window." + cb, "", 0, out _, out _);
#endif
                                            }
                                            v8Context.Exit();
                                        });
                                        //}
                                        //catch (Exception e)
                                        //{
                                        //    File.AppendAllText("test.txt", e + "\r\n");
                                        //}
                                    }
                                    callback = null;
                                }
                                else
                                {
                                    Cef.Result = result;
                                    Cef.ResultType = type;
                                    Cef.ManualResetEvent.Set();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //File.AppendAllText(Path.Combine(CPF.Platform.Application.StartupPath, "cef.log"), DateTime.Now + ":" + e.ToString() + "\r\n");
                    }
                })
                { IsBackground = true, Name = "接受消息" }.Start();
            }
            catch (Exception e)
            {
                //File.AppendAllText(Path.Combine(CPF.Platform.Application.StartupPath, "cef.log"), DateTime.Now + ":" + e.ToString() + "\r\n");
            }


            WithErrorHandling(() =>
            {
                using (CefObjectTracker.StartTracking())
                {
                    if (_javascriptToNativeDispatcher != null)
                        _javascriptToNativeDispatcher.HandleContextCreated(context, frame.IsMain);

                    var message = new Messages.JsContextCreated();
                    var cefMessage = message.ToCefProcessMessage();
                    frame.SendProcessMessage(CefProcessId.Browser, cefMessage);
                }
            }, frame);
        }

        //private CefBrowser _browser;
        private JavascriptExecutionEngineRenderSide _javascriptExecutionEngine;
        private JavascriptToNativeDispatcherRenderSide _javascriptToNativeDispatcher;

        private readonly MessageDispatcher _messageDispatcher = new MessageDispatcher();


        protected override void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            WithErrorHandling(() =>
            {
                using (CefObjectTracker.StartTracking())
                {
                    if (_javascriptToNativeDispatcher != null)
                        _javascriptToNativeDispatcher.HandleContextReleased(context, frame.IsMain);
                    base.OnContextReleased(browser, frame, context);

                    var message = new Messages.JsContextReleased();
                    var cefMessage = message.ToCefProcessMessage();
                    frame.SendProcessMessage(CefProcessId.Browser, cefMessage);
                }
            }, frame);
        }

        protected override void OnUncaughtException(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace)
        {
            WithErrorHandling(() =>
            {
                using (CefObjectTracker.StartTracking())
                {
                    var frames = new Messages.JsStackFrame[stackTrace.FrameCount];
                    for (var i = 0; i < stackTrace.FrameCount; i++)
                    {
                        var stackFrame = stackTrace.GetFrame(i);
                        frames[i] = new Messages.JsStackFrame()
                        {
                            FunctionName = stackFrame.FunctionName,
                            ScriptNameOrSourceUrl = stackFrame.ScriptNameOrSourceUrl,
                            LineNumber = stackFrame.LineNumber,
                            Column = stackFrame.Column
                        };
                    }

                    var message = new Messages.JsUncaughtException()
                    {
                        Message = exception.Message,
                        StackFrames = frames
                    };

                    var cefMessage = message.ToCefProcessMessage();
                    frame.SendProcessMessage(CefProcessId.Browser, cefMessage);

                    base.OnUncaughtException(browser, frame, context, exception, stackTrace);
                }
            }, frame);
        }


        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CefFrame frame = null;
            var exception = (Exception)e.ExceptionObject;
            try
            {
                var _browser = browsers.FirstOrDefault().Value;
                frame = _browser?.FrameCount > 0 ? _browser?.GetMainFrame() : null;
            }
            catch
            {
                // ignore
            }
            HandleException(exception, frame);
        }

        private void WithErrorHandling(Action action, CefFrame frame)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                HandleException(e, frame);
            }
        }

        private void HandleException(Exception e, CefFrame frame)
        {
            if (frame != null)
            {
                try
                {
                    using (CefObjectTracker.StartTracking())
                    {
                        var exceptionMessage = new Messages.UnhandledException()
                        {
                            ExceptionType = e.GetType().FullName,
                            Message = e.Message,
                            StackTrace = e.StackTrace
                        };
                        var message = exceptionMessage.ToCefProcessMessage();
                        frame.SendProcessMessage(CefProcessId.Browser, message);
                    }
                    return;
                }
                catch
                {
                    // ignore, lets try an alternative method using the crash pipe
                }
            }
            //SendExceptionToParentProcess(e);
        }

        protected override void OnFocusedNodeChanged(CefBrowser browser, CefFrame frame, CefDomNode node)
        {
            if (node != null && node.IsEditable)
            {
                var msg = CefProcessMessage.Create("OnFocusedNodeChanged");
                var rect = node.GetElementBounds();
                msg.Arguments.SetInt(0, rect.X);
                msg.Arguments.SetInt(1, rect.Y);
                SendProcessMessage(browser, CefProcessId.Browser, msg);
            }
        }
    }
}
