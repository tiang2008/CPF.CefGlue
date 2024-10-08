//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using CPF.CefGlue.Interop;
    
    // Role: PROXY
    public sealed unsafe partial class CefRequestContext : CefPreferenceManager
    {
        internal static CefRequestContext FromNative(cef_request_context_t* ptr)
        {
            return new CefRequestContext(ptr);
        }
        
        internal static CefRequestContext FromNativeOrNull(cef_request_context_t* ptr)
        {
            if (ptr == null) return null;
            return new CefRequestContext(ptr);
        }
        
        private CefRequestContext(cef_request_context_t* ptr)
            : base((cef_preference_manager_t*)ptr) {}
        
        internal new cef_request_context_t* ToNative()
        {
            return (cef_request_context_t*)_self;
        }
    }
}
