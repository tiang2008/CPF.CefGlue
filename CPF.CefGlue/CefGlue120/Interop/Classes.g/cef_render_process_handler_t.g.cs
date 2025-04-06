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
    internal unsafe struct cef_render_process_handler_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _on_web_kit_initialized;
        internal IntPtr _on_browser_created;
        internal IntPtr _on_browser_destroyed;
        internal IntPtr _get_load_handler;
        internal IntPtr _on_context_created;
        internal IntPtr _on_context_released;
        internal IntPtr _on_uncaught_exception;
        internal IntPtr _on_focused_node_changed;
        internal IntPtr _on_process_message_received;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void add_ref_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int release_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int has_one_ref_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int has_at_least_one_ref_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_web_kit_initialized_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_browser_created_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_dictionary_value_t* extra_info);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_browser_destroyed_delegate(cef_render_process_handler_t* self, cef_browser_t* browser);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate cef_load_handler_t* get_load_handler_delegate(cef_render_process_handler_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_context_created_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8_context_t* context);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_context_released_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8_context_t* context);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_uncaught_exception_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8_context_t* context, cef_v8_exception_t* exception, cef_v8_stack_trace_t* stackTrace);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate void on_focused_node_changed_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_domnode_t* node);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        internal delegate int on_process_message_received_delegate(cef_render_process_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, CefProcessId source_process, cef_process_message_t* message);
        
        private static int _sizeof;
        
        static cef_render_process_handler_t()
        {
            _sizeof = Marshal.SizeOf(typeof(cef_render_process_handler_t));
        }
        
        internal static cef_render_process_handler_t* Alloc()
        {
            var ptr = (cef_render_process_handler_t*)Marshal.AllocHGlobal(_sizeof);
            *ptr = new cef_render_process_handler_t();
            ptr->_base._size = (UIntPtr)_sizeof;
            return ptr;
        }
        
        internal static void Free(cef_render_process_handler_t* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
        
    }
}
