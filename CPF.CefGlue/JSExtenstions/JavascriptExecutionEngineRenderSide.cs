using CPF.CefGlue;
using System;

namespace CPF.CefGlue.JSExtenstions
{
    internal class JavascriptExecutionEngineRenderSide
    {
        public JavascriptExecutionEngineRenderSide(MessageDispatcher dispatcher)
        {
            dispatcher.RegisterMessageHandler(Messages.JsEvaluationRequest.Name, HandleScriptEvaluation);
        }

        private void HandleScriptEvaluation(MessageReceivedEventArgs args)
        {
            var frame = args.Frame;

            using (var context = frame.V8Context.EnterOrFail())
            {
                var message = Messages.JsEvaluationRequest.FromCefMessage(args.Message);
                // send script to browser
                CefV8Value value; CefV8Exception exception;
                var success = context.V8Context.TryEval(message.Script, message.Url, message.Line, out value, out exception);

                var response = new Messages.JsEvaluationResult()
                {
                    TaskId = message.TaskId,
                    Success = success,
                    Exception = success ? null : exception.Message,
                    Result = new CefValueHolder()
                };

                if (value != null)
                {
                    V8ValueSerialization.SerializeV8ObjectToCefValue(value, response.Result);
                }

                var cefResponseMessage = response.ToCefProcessMessage();
                frame.SendProcessMessage(CefProcessId.Browser, cefResponseMessage);
            }
        }
    }
}
