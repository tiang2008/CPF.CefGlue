﻿using CPF.CefGlue.Interop;

#nullable enable
namespace CPF.CefGlue;

/// <summary>
///     Callback interface used for asynchronous continuation of authentication
///     requests.
/// </summary>
public sealed unsafe partial class CefAuthCallback
{
    /// <summary>
    ///     Continue the authentication request.
    /// </summary>
    public void Continue(string? username, string? password)
    {
        fixed (char* username_str = username)
        fixed (char* password_str = password)
        {
            var n_username = new cef_string_t(username_str, username?.Length ?? 0);
            var n_password = new cef_string_t(password_str, password?.Length ?? 0);

            cef_auth_callback_t.cont(_self, &n_username, &n_password);
        }
    }

    /// <summary>
    ///     Cancel the authentication request.
    /// </summary>
    public void Cancel()
    {
        cef_auth_callback_t.cancel(_self);
    }
}