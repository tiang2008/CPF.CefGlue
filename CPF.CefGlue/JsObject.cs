using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using CPF.Controls;

namespace CPF.CefGlue
{
    /// <summary>
    /// 用于C#注册到JS，让JS可以调用C#
    /// </summary>
    internal class JsObject : CefV8Handler
    {
        public JsObject()
        {
            //NamedPipeServerStream
        }
        public ManualResetEvent ManualResetEvent = new ManualResetEvent(false);
        public object Result;
        internal ValueType ResultType;
        internal CpfCefRenderProcessHandler RenderProcessHandler;
        internal ConcurrentDictionary<string, CefV8Context> v8Context = new ConcurrentDictionary<string, CefV8Context>();

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            try
            {
                //returnValue = CefV8Value.CreateNull();
                //returnValue = CefV8Value.CreateString(name);
                returnValue = CefV8Value.CreateUndefined();
                if (name == "$CallCSharp")
                {
                    //returnValue = CefV8Value.CreateString(arguments.Length.ToString());
                    if (arguments != null && arguments.Length == 2)
                    {
                        //if (arguments.Length == 1 && obj.IsFunction)
                        //{
                        //    //CefV8Value[] args = new CefV8Value[1];
                        //    //args[0] = CefV8Value.CreateString("test测试");
                        //    //returnValue = arguments[0].ExecuteFunction(null, args);
                        //    returnValue = arguments[0];
                        //}
                        CefV8Context currentContext = CefV8Context.GetCurrentContext();
                        var frame = currentContext.GetFrame();
                        CefBrowser browser = currentContext.GetBrowser();
                        var funName = arguments[0].GetStringValue();
                        RenderProcessHandler.clientStream.Write((byte)ValueType.Int);
                        RenderProcessHandler.clientStream.Write(browser.Identifier);
                        RenderProcessHandler.clientStream.Write((byte)ValueType.String);
                        RenderProcessHandler.clientStream.WriteStringEx(funName);
                        var len = arguments[1].GetValue("length").GetIntValue();
                        for (int i = 0; i < len; i++)
                        {
                            var value = arguments[1].GetValue(i);
                            if (value.IsBool)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.Bool);
                                RenderProcessHandler.clientStream.Write(value.GetBoolValue());
                                //cefProcessMessage.Arguments.SetBool(i + 1, value.GetBoolValue());
                            }
                            else if (value.IsDate)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.Date);
                                RenderProcessHandler.clientStream.Write(value.GetDateValue().Ticks);
                                //cefProcessMessage.Arguments.SetInt(i + 1, value.GetIntValue());
                            }
                            else if (value.IsInt)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.Int);
                                RenderProcessHandler.clientStream.Write(value.GetIntValue());
                                //cefProcessMessage.Arguments.SetInt(i + 1, value.GetIntValue());
                            }
                            //else if (value.IsUInt)
                            //{
                            //    RenderProcessHandler.clientStream.Write((byte)ValueType.UInt);
                            //    RenderProcessHandler.clientStream.Write(value.GetIntValue());
                            //    //cefProcessMessage.Arguments.SetInt(i + 1, value.GetIntValue());
                            //}
                            else if (value.IsDouble)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.Double);
                                RenderProcessHandler.clientStream.Write(value.GetDoubleValue());
                                //cefProcessMessage.Arguments.SetDouble(i + 1, value.GetDoubleValue());
                            }
                            else if (value.IsNull || value.IsUndefined)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.Null);
                                //cefProcessMessage.Arguments.SetNull(i + 1);
                            }
                            else if (value.IsString)
                            {
                                RenderProcessHandler.clientStream.Write((byte)ValueType.String);
                                var str = value.GetStringValue();
                                if (str == null)
                                {
                                    str = "";
                                }
                                RenderProcessHandler.clientStream.WriteStringEx(str);
                                //cefProcessMessage.Arguments.SetString(i + 1, value.GetStringValue());
                            }
                            //else if (value.IsFunction)
                            //{

                            //}
                        }
                        if (len > 0 && arguments[1].GetValue(len - 1).IsFunction)
                        {
                            var cn = "$callBack" + Guid.NewGuid().ToString().Replace('-', '_');
                            v8Context.TryAdd(cn, frame.V8Context);
                            frame.V8Context.GetGlobal().SetValue(cn, arguments[1].GetValue(len - 1), CefV8PropertyAttribute.None);
                            RenderProcessHandler.clientStream.Write((byte)ValueType.CallBack);
                            RenderProcessHandler.clientStream.WriteStringEx(cn);
                        }

                        RenderProcessHandler.clientStream.Write((byte)ValueType.End);

                        ManualResetEvent.WaitOne();
                        ManualResetEvent.Reset();
                        returnValue = ConvertValue(Result, ResultType);
                    }
                }
                exception = null;
                return true;
            }
            catch (Exception e)
            {
                returnValue = null;
                exception = e.Message;
                return false;
            }
        }

        public static CefV8Value ConvertValue(object value, ValueType valueType)
        {
            var returnValue = CefV8Value.CreateUndefined();
            switch (valueType)
            {
                case ValueType.Null:
                    returnValue = CefV8Value.CreateNull();
                    break;
                case ValueType.Bool:
                    returnValue = CefV8Value.CreateBool((bool)value);
                    break;
                case ValueType.Date:
                    returnValue = CefV8Value.CreateDate((CefBaseTime)value);
                    break;
                case ValueType.Double:
                    returnValue = CefV8Value.CreateDouble((double)value);
                    break;
                case ValueType.Int:
                    returnValue = CefV8Value.CreateInt((int)value);
                    break;
                case ValueType.String:
                    returnValue = CefV8Value.CreateString((string)value);
                    break;
                    //case ValueType.UInt:
                    //    returnValue = CefV8Value.CreateUInt((uint)Result);
                    //    break;
            }

            return returnValue;
        }

        //        public void SendProcessMessage(CefBrowser _browser, CefProcessId targetProcess, CefProcessMessage message)
        //        {
        //#if Net4
        //            _browser.SendProcessMessage(targetProcess, message);
        //#else
        //            _browser.GetMainFrame().SendProcessMessage(targetProcess, message);
        //#endif
        //        }
    }

    enum ValueType : byte
    {
        Undefined,
        Null,
        Bool,
        Date,
        Double,
        Int,
        String,
        //UInt,
        /// <summary>
        /// 数据结束
        /// </summary>
        End,
        /// <summary>
        /// 异步回调的方法名
        /// </summary>
        CallBack,
        //RemoteCall,
        ///// <summary>
        ///// 异常消息
        ///// </summary>
        //Error,
    }
}
