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
    internal unsafe struct cef_media_route_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _get_id;
        internal IntPtr _get_source;
        internal IntPtr _get_sink;
        internal IntPtr _send_route_message;
        internal IntPtr _terminate;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void add_ref_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int release_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_one_ref_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_at_least_one_ref_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_id_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_media_source_t* get_source_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_media_sink_t* get_sink_delegate(cef_media_route_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void send_route_message_delegate(cef_media_route_t* self, void* message, UIntPtr message_size);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void terminate_delegate(cef_media_route_t* self);
        
        // AddRef
        private static IntPtr _p0;
        private static add_ref_delegate _d0;
        
        public static void add_ref(cef_media_route_t* self)
        {
            add_ref_delegate d;
            var p = self->_base._add_ref;
            if (p == _p0) { d = _d0; }
            else
            {
                d = (add_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(add_ref_delegate));
                if (_p0 == IntPtr.Zero) { _d0 = d; _p0 = p; }
            }
            d(self);
        }
        
        // Release
        private static IntPtr _p1;
        private static release_delegate _d1;
        
        public static int release(cef_media_route_t* self)
        {
            release_delegate d;
            var p = self->_base._release;
            if (p == _p1) { d = _d1; }
            else
            {
                d = (release_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(release_delegate));
                if (_p1 == IntPtr.Zero) { _d1 = d; _p1 = p; }
            }
            return d(self);
        }
        
        // HasOneRef
        private static IntPtr _p2;
        private static has_one_ref_delegate _d2;
        
        public static int has_one_ref(cef_media_route_t* self)
        {
            has_one_ref_delegate d;
            var p = self->_base._has_one_ref;
            if (p == _p2) { d = _d2; }
            else
            {
                d = (has_one_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_one_ref_delegate));
                if (_p2 == IntPtr.Zero) { _d2 = d; _p2 = p; }
            }
            return d(self);
        }
        
        // HasAtLeastOneRef
        private static IntPtr _p3;
        private static has_at_least_one_ref_delegate _d3;
        
        public static int has_at_least_one_ref(cef_media_route_t* self)
        {
            has_at_least_one_ref_delegate d;
            var p = self->_base._has_at_least_one_ref;
            if (p == _p3) { d = _d3; }
            else
            {
                d = (has_at_least_one_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_at_least_one_ref_delegate));
                if (_p3 == IntPtr.Zero) { _d3 = d; _p3 = p; }
            }
            return d(self);
        }
        
        // GetId
        private static IntPtr _p4;
        private static get_id_delegate _d4;
        
        public static cef_string_userfree* get_id(cef_media_route_t* self)
        {
            get_id_delegate d;
            var p = self->_get_id;
            if (p == _p4) { d = _d4; }
            else
            {
                d = (get_id_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_id_delegate));
                if (_p4 == IntPtr.Zero) { _d4 = d; _p4 = p; }
            }
            return d(self);
        }
        
        // GetSource
        private static IntPtr _p5;
        private static get_source_delegate _d5;
        
        public static cef_media_source_t* get_source(cef_media_route_t* self)
        {
            get_source_delegate d;
            var p = self->_get_source;
            if (p == _p5) { d = _d5; }
            else
            {
                d = (get_source_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_source_delegate));
                if (_p5 == IntPtr.Zero) { _d5 = d; _p5 = p; }
            }
            return d(self);
        }
        
        // GetSink
        private static IntPtr _p6;
        private static get_sink_delegate _d6;
        
        public static cef_media_sink_t* get_sink(cef_media_route_t* self)
        {
            get_sink_delegate d;
            var p = self->_get_sink;
            if (p == _p6) { d = _d6; }
            else
            {
                d = (get_sink_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_sink_delegate));
                if (_p6 == IntPtr.Zero) { _d6 = d; _p6 = p; }
            }
            return d(self);
        }
        
        // SendRouteMessage
        private static IntPtr _p7;
        private static send_route_message_delegate _d7;
        
        public static void send_route_message(cef_media_route_t* self, void* message, UIntPtr message_size)
        {
            send_route_message_delegate d;
            var p = self->_send_route_message;
            if (p == _p7) { d = _d7; }
            else
            {
                d = (send_route_message_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(send_route_message_delegate));
                if (_p7 == IntPtr.Zero) { _d7 = d; _p7 = p; }
            }
            d(self, message, message_size);
        }
        
        // Terminate
        private static IntPtr _p8;
        private static terminate_delegate _d8;
        
        public static void terminate(cef_media_route_t* self)
        {
            terminate_delegate d;
            var p = self->_terminate;
            if (p == _p8) { d = _d8; }
            else
            {
                d = (terminate_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(terminate_delegate));
                if (_p8 == IntPtr.Zero) { _d8 = d; _p8 = p; }
            }
            d(self);
        }
        
    }
}
