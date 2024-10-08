using CPF.CefGlue;
using System;

namespace CPF.CefGlue.JSExtenstions
{
    internal class V8FunctionHandler : CefV8Handler
    {
        private readonly string _objectName;
        private readonly Func<Messages.NativeObjectCallRequest, PromiseHolder> _functionCallHandler;
        public V8FunctionHandler(string objectName, Func<Messages.NativeObjectCallRequest, PromiseHolder> functionCallHandler)
        {
            _objectName = objectName;
            _functionCallHandler = functionCallHandler;
        }

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            try
            {
                var cefArgs = CefListValue.Create();
                // create a copy of the args to pass to the browser process
                for (var i = 0; i < arguments.Length; i++)
                {
                    V8ValueSerialization.SerializeV8ObjectToCefValue(arguments[i], new CefListWrapper(cefArgs, i));
                }

                var message = new Messages.NativeObjectCallRequest()
                {
                    ObjectName = _objectName,
                    MemberName = name,
                    ArgumentsIn = cefArgs
                };

                var callResult = _functionCallHandler(message);

                if (callResult != null)
                {
                    returnValue = callResult.Promise;
                    exception = null;
                }
                else
                {
                    returnValue = null;
                    exception = "Failed to create promise";
                }
            }
            catch (Exception ex)
            {
                returnValue = null;
                exception = ex.Message;
            }

            return true;
        }
    }
}
