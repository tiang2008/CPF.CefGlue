﻿using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

/// <summary>
///     Callback for asynchronous continuation of CefResourceHandler::Read().
/// </summary>
public sealed unsafe partial class CefResourceReadCallback
{
    /// <summary>
    ///     Callback for asynchronous continuation of Read(). If |bytes_read| == 0
    ///     the response will be considered complete. If |bytes_read| > 0 then Read()
    ///     will be called again until the request is complete (based on either the
    ///     result or the expected content length). If |bytes_read| &lt; 0 then the
    ///     request will fail and the |bytes_read| value will be treated as the error
    ///     code.
    /// </summary>
    public void Continue(int bytesRead)
    {
        cef_resource_read_callback_t.cont(_self, bytesRead);
    }
}