//------------------------------------------------------------------------------
// <auto-generated>
//      This file is auto-generated!
//      DO NOT MODIFY!
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace CPF.CefGlue.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_resource_request_handler_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _get_cookie_access_filter;
        internal IntPtr _on_before_resource_load;
        internal IntPtr _get_resource_handler;
        internal IntPtr _on_resource_redirect;
        internal IntPtr _on_resource_response;
        internal IntPtr _get_resource_response_filter;
        internal IntPtr _on_resource_load_complete;
        internal IntPtr _on_protocol_execution;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void add_ref_delegate(cef_resource_request_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int release_delegate(cef_resource_request_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int has_one_ref_delegate(cef_resource_request_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int has_at_least_one_ref_delegate(cef_resource_request_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate cef_cookie_access_filter_t* get_cookie_access_filter_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate CefReturnValue on_before_resource_load_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_callback_t* callback);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate cef_resource_handler_t* get_resource_handler_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_resource_redirect_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, cef_string_t* new_url);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int on_resource_response_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate cef_response_filter_t* get_resource_response_filter_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_resource_load_complete_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_response_t* response, CefUrlRequestStatus status, long received_content_length);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_protocol_execution_delegate(cef_resource_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, int* allow_os_execution);
        
        private static int _sizeof;
        
        static cef_resource_request_handler_t()
        {
            _sizeof = Marshal.SizeOf(typeof(cef_resource_request_handler_t));
        }
        
        internal static cef_resource_request_handler_t* Alloc()
        {
            var ptr = (cef_resource_request_handler_t*)Marshal.AllocHGlobal(_sizeof);
            *ptr = new cef_resource_request_handler_t();
            ptr->_base._size = (UIntPtr)_sizeof;
            return ptr;
        }
        
        internal static void Free(cef_resource_request_handler_t* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
        
    }
}
