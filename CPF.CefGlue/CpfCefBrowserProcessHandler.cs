using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using CPF.Controls;
using CPF.Reflection;
using System.Reflection;

namespace CPF.CefGlue
{
    public class CpfCefBrowserProcessHandler : CefBrowserProcessHandler
    {
        internal ConcurrentDictionary<string, BinaryWriter> clients = new ConcurrentDictionary<string, BinaryWriter>();
        internal ConcurrentDictionary<string, NamedPipeServerStream> servers = new ConcurrentDictionary<string, NamedPipeServerStream>();

        //protected override void OnRenderProcessThreadCreated(CefListValue extraInfo)
        //{
        //    string name = Guid.NewGuid().ToString();
        //    extraInfo.SetString(0, name);
        //    NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", name + "Render", PipeDirection.Out);
        //    clients.TryAdd(name, new BinaryWriter(pipeClient, Encoding.Unicode));
        //    ThreadPool.QueueUserWorkItem(a => pipeClient.Connect());

        //    new Thread(n =>
        //    {
        //        var serverStream = new NamedPipeServerStream((string)n + "Browser", PipeDirection.In, 1);
        //        servers.TryAdd((string)n, serverStream);
        //        serverStream.WaitForConnection();

        //        using (BinaryReader br = new BinaryReader(serverStream, Encoding.Unicode))
        //        {
        //            List<(ValueType type, Type ctype, object value)> values = new List<(ValueType, Type, object)>();
        //            string callback = null;
        //            while (true)
        //            {
        //                ValueType type;
        //                try
        //                {
        //                    type = (ValueType)br.ReadByte();
        //                }
        //                catch (Exception)
        //                {
        //                    clients.TryRemove((string)n, out _);
        //                    servers.TryRemove((string)n, out _);
        //                    return;
        //                }
        //                switch (type)
        //                {
        //                    case ValueType.Undefined:
        //                        values.Add((type, typeof(string), null));
        //                        break;
        //                    case ValueType.Null:
        //                        values.Add((type, typeof(string), null));
        //                        break;
        //                    case ValueType.Bool:
        //                        values.Add((type, typeof(bool), br.ReadBoolean()));
        //                        break;
        //                    case ValueType.Date:
        //                        values.Add((type, typeof(DateTime), new DateTime(br.ReadInt64(), DateTimeKind.Utc)));
        //                        break;
        //                    case ValueType.Double:
        //                        values.Add((type, typeof(double), br.ReadDouble()));
        //                        break;
        //                    case ValueType.Int:
        //                        values.Add((type, typeof(int), br.ReadInt32()));
        //                        break;
        //                    case ValueType.String:
        //                        values.Add((type, typeof(string), br.ReadStringEx()));
        //                        break;
        //                    case ValueType.CallBack:
        //                        callback = br.ReadStringEx();
        //                        break;
        //                    case ValueType.End:
        //                        var rType = ValueType.Undefined;
        //                        object r = null;
        //                        if (WebBrowser.browsers.TryGetValue((int)values[0].value, out var browser))
        //                        {
        //                            object global = null;
        //                            browser.Invoke(() =>
        //                            {
        //                                global = browser.GlobalObject;
        //                            });
        //                            var fn = (string)values[1].value;
        //                            if (global != null && !string.IsNullOrWhiteSpace(fn))
        //                            {
        //                                var method = global.GetType().GetMethod(fn);
        //                                if (method != null)
        //                                {
        //                                    var jsf = method.GetCustomAttributes(typeof(JSFunction), true);
        //                                    if (jsf != null && jsf.Length > 0)
        //                                    {
        //                                        var ps = method.GetParameters();
        //                                        if (ps.Length != values.Count - 2)
        //                                        {
        //                                            throw new Exception("方法" + fn + "参数数量不对");
        //                                        }
        //                                        var list = new List<object>();
        //                                        for (int i = 0; i < ps.Length; i++)
        //                                        {
        //                                            var item = ps[i];
        //                                            var pt = item.ParameterType;
        //                                            if (pt != typeof(bool) && pt != typeof(int) && pt != typeof(string) && pt != typeof(DateTime) && pt != typeof(double))
        //                                            {
        //                                                throw new Exception("方法" + fn + "参数类型不支持" + pt + "，必须是int bool string double DateTime这几种类型");
        //                                            }
        //                                            if (values[i + 2].ctype != pt)
        //                                            {
        //                                                throw new Exception("方法" + fn + "参数类型不对" + pt);
        //                                            }
        //                                            list.Add(values[i + 2].value);
        //                                        }
        //                                        var pt1 = method.ReturnType;
        //                                        if (pt1 == typeof(bool))
        //                                        {
        //                                            rType = ValueType.Bool;
        //                                        }
        //                                        else if (pt1 == typeof(int))
        //                                        {
        //                                            rType = ValueType.Int;
        //                                        }
        //                                        //else if (pt1 == typeof(uint))
        //                                        //{
        //                                        //    rType = ValueType.UInt;
        //                                        //}
        //                                        else if (pt1 == typeof(string))
        //                                        {
        //                                            rType = ValueType.String;
        //                                        }
        //                                        else if (pt1 == typeof(DateTime))
        //                                        {
        //                                            rType = ValueType.Date;
        //                                        }
        //                                        else if (pt1 == typeof(double))
        //                                        {
        //                                            rType = ValueType.Double;
        //                                        }
        //                                        else if (pt1 != typeof(void))
        //                                        {
        //                                            throw new Exception("方法" + fn + "返回值类型不支持" + pt1);
        //                                        }
        //                                        if (callback == null)
        //                                        {
        //                                            browser.Invoke(() =>
        //                                            {
        //                                                r = method.FastInvoke(global, list.ToArray());
        //                                            });
        //                                        }
        //                                        else
        //                                        {
        //                                            new Thread(a =>
        //                                            {
        //                                                var item = (ValueTuple<string, ValueType, object, List<object>, MethodInfo, string>)a;
        //                                                var cr = item.Item5.FastInvoke(item.Item3, item.Item4.ToArray());

        //                                                if (clients.TryGetValue(item.Item6, out var bw))
        //                                                {
        //                                                    bw.Write((byte)ValueType.CallBack);
        //                                                    bw.Write(item.Item1);
        //                                                    SetResult(item.Item2, cr, bw);
        //                                                }
        //                                            })
        //                                            { Name = "JS异步调用C#", IsBackground = true }.Start((callback, rType, global, list, method, (string)n));
        //                                        }
        //                                        if (rType == ValueType.String && r == null)
        //                                        {
        //                                            rType = ValueType.Null;
        //                                        }
        //                                    }
        //                                }

        //                            }
        //                        }
        //                        if (clients.TryGetValue((string)n, out var binaryWriter))
        //                        {
        //                            if (callback != null)
        //                            {
        //                                SetResult(ValueType.Undefined, null, binaryWriter);
        //                            }
        //                            else
        //                            {
        //                                SetResult(rType, r, binaryWriter);
        //                            }
        //                        }
        //                        values.Clear();
        //                        callback = null;
        //                        break;
        //                }
        //            }
        //        }
        //    })
        //    { IsBackground = true, Name = "JS调用C#" }.Start(name);
        //}

        //private static void SetResult(ValueType rType, object r, BinaryWriter binaryWriter)
        //{
        //    binaryWriter.Write((byte)rType);
        //    if (rType != ValueType.Null && rType != ValueType.Undefined)
        //    {
        //        switch (rType)
        //        {
        //            case ValueType.Bool:
        //                binaryWriter.Write((bool)r);
        //                break;
        //            case ValueType.Date:
        //                binaryWriter.Write(((DateTime)r).Ticks);
        //                break;
        //            case ValueType.Double:
        //                binaryWriter.Write((double)r);
        //                break;
        //            case ValueType.Int:
        //                binaryWriter.Write((int)r);
        //                break;
        //            case ValueType.String:
        //                binaryWriter.WriteStringEx((string)r);
        //                break;
        //                //case ValueType.UInt:
        //                //    binaryWriter.Write((uint)r);
        //                //    break;
        //        }
        //    }
        //}

        protected override void OnBeforeChildProcessLaunch(CefCommandLine commandLine)
        {
            commandLine.AppendSwitch("default-encoding", "utf-8");
            commandLine.AppendSwitch("allow-file-access-from-files");
            commandLine.AppendSwitch("allow-universal-access-from-files");
            commandLine.AppendSwitch("disable-web-security");
            commandLine.AppendSwitch("ignore-certificate-errors");

            if (CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.OSX)
            {
                if (!commandLine.HasSwitch("cpf"))
                {

                    //var path = new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath;
                    // ^^ not correct path, points too src/TestApp.macOS/bin/Debug/TestApp.Mac.app/Contents/Monobundle/TestApp.Mac.exe
                    //Console.WriteLine(Path.Combine(CPF.Platform.Application.StartupPath, "ConsoleApp1"));
                    //explicit path to see if it solved issue
                    commandLine.SetProgram(Path.Combine(CPF.Platform.Application.StartupPath, Assembly.GetEntryAssembly().FullName.Split(',')[0].Trim()));

                    commandLine.AppendSwitch("cpf", "w");
                }
            }
            //Console.WriteLine(commandLine.ToString());
        }

#if !Net4
        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            //CPF.Threading.DispatcherTimer timer = new CPF.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(15), IsEnabled = true };
            //timer.Tick += delegate
            //{
            //    CefRuntime.DoMessageLoopWork();
            //};
            base.OnScheduleMessagePumpWork(delayMs);
        }
#endif
    }
}
