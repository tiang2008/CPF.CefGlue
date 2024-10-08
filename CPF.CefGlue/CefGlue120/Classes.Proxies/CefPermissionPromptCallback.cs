﻿namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using CPF.CefGlue.Interop;

    /// <summary>
    /// Callback interface used for asynchronous continuation of permission prompts.
    /// </summary>
    public sealed unsafe partial class CefPermissionPromptCallback
    {
        /// <summary>
        /// Complete the permissions request with the specified |result|.
        /// </summary>
        public void Continue(CefPermissionRequestResult result)
        {
            cef_permission_prompt_callback_t.cont(_self, result);
        }
    }
}
