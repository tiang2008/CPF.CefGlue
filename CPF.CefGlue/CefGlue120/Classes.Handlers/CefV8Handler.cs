using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

/// <summary>
///     Interface that should be implemented to handle V8 function calls. The methods
///     of this class will always be called on the render process main thread.
/// </summary>
public abstract unsafe partial class CefV8Handler
{
    private static readonly CefV8Value[] emtpyArgs = new CefV8Value[0];

    private int execute(cef_v8_handler_t* self, cef_string_t* name, cef_v8_value_t* @object, UIntPtr argumentsCount,
        cef_v8_value_t** arguments, cef_v8_value_t** retval, cef_string_t* exception)
    {
        CheckSelf(self);

        var m_name = cef_string_t.ToString(name);
        var m_obj = CefV8Value.FromNative(@object);
        var argc = (int) argumentsCount;
        CefV8Value[] m_arguments;
        if (argc == 0)
        {
            m_arguments = emtpyArgs;
        }
        else
        {
            m_arguments = new CefV8Value[argc];
            for (var i = 0; i < argc; i++) m_arguments[i] = CefV8Value.FromNative(arguments[i]);
        }

        var handled = Execute(m_name, m_obj, m_arguments, out var m_returnValue, out var m_exception);

        if (handled)
        {
            if (m_exception != null)
                cef_string_t.Copy(m_exception, exception);
            else if (m_returnValue != null) *retval = m_returnValue.ToNative();
        }

        return handled ? 1 : 0;
    }

    /// <summary>
    ///     Handle execution of the function identified by |name|. |object| is the
    ///     receiver ('this' object) of the function. |arguments| is the list of
    ///     arguments passed to the function. If execution succeeds set |retval| to the
    ///     function return value. If execution fails set |exception| to the exception
    ///     that will be thrown. Return true if execution was handled.
    /// </summary>
    protected abstract bool Execute(string? name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value? returnValue,
        out string? exception);
}
