using CPF.CefGlue;
using CPF.CefGlue.JSExtenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CPF.CefGlue.JSExtenstions
{
    internal class NativeObject
    {
        private readonly object _target;
        private readonly IDictionary<string, NativeMethod> _methods;
        private readonly object _methodHandlerTarget;
        private readonly NativeMethod _methodHandler;

        public NativeObject(string name, object target, MethodCallHandler methodHandler = null)
        {
            Name = name;
            _target = target;
            _methods = GetObjectMembers(target);

            if (methodHandler != null)
            {
                _methodHandler = new NativeMethod(methodHandler.Method);
                _methodHandlerTarget = methodHandler.Target;
            }
        }

        public string Name { get; }

        public IEnumerable<string> MethodsNames => _methods.Keys;
       
        public void ExecuteMethod(string methodName, object[] args, Action<object, Exception> handleResult)
        {
            if (_methods == null)
            {
                handleResult(default, new Exception($"Object does not have a method."));
                return;
            }
            if (!_methods.TryGetValue(methodName ?? "", out var method))
            {
                handleResult(default, new Exception($"Object does not have a {methodName} method."));
                return;
            }

            if (_methodHandler == null)
            {
                method.Execute(_target, args, handleResult);
                return;
            }

            var innerMethod = method.MakeDelegate(_target, args);
            _methodHandler.Execute(_methodHandlerTarget, new[] { innerMethod }, (result, exception) =>
            {
                if (result is Task task)
                {
                    task.ContinueWith(t =>
                    {
                        var taskResult = GenericTaskAwaiter.GetResultFrom(t);
                        handleResult(taskResult.Result, taskResult.Exception);
                    });
                    return;
                }

                handleResult(result, exception);
            });
        }
        private static IDictionary<string, NativeMethod> GetObjectMembers(object obj)
        {
            var methods = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                       //.Where(p => p.GetCustomAttributes(typeof(JSFunction), inherit: false).Length > 0)
                                       .Where(m => !m.IsSpecialName);
            //return methods.ToDictionary(m => ToJavascriptMemberName(m.Name), m => new NativeMethod(m));
            return methods.ToDictionary(m => m.Name, m => new NativeMethod(m));
        }

        private static string ToJavascriptMemberName(string name) =>
            name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);
    }
}
