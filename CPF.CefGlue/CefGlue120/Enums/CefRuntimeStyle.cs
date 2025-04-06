namespace CPF.CefGlue;

public enum CefRuntimeStyle
{
    ///
    /// Use the default runtime style. The default style will match the
    /// CefSettings.chrome_runtime value in most cases. See above documentation
    /// for exceptions.
    ///
    CEF_RUNTIME_STYLE_DEFAULT,

    ///
    /// Use the Chrome runtime style. Only supported with the Chrome runtime.
    ///
    CEF_RUNTIME_STYLE_CHROME,

    ///
    /// Use the Alloy runtime style. Supported with both the Alloy and Chrome
    /// runtime.
    ///
    CEF_RUNTIME_STYLE_ALLOY,
}