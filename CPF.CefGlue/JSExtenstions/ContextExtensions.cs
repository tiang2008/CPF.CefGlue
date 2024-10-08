using CPF.CefGlue;
using System;

namespace CPF.CefGlue
{
    internal static class ContextExtensions
    {
        public static ContextWrapper EnterOrFail(this CefV8Context context, bool shallDispose = true)
        {
            if (!context.Enter())
            {
                throw new InvalidOperationException("Could not enter context");
            }
            return new ContextWrapper(context, shallDispose);
        }
    }
}
