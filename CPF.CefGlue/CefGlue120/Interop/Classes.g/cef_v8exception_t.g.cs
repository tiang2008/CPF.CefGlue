//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace CPF.CefGlue.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_v8exception_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _get_message;
        internal IntPtr _get_source_line;
        internal IntPtr _get_script_resource_name;
        internal IntPtr _get_line_number;
        internal IntPtr _get_start_position;
        internal IntPtr _get_end_position;
        internal IntPtr _get_start_column;
        internal IntPtr _get_end_column;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void add_ref_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int release_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_one_ref_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_at_least_one_ref_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_message_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_source_line_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_script_resource_name_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_line_number_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_start_position_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_end_position_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_start_column_delegate(cef_v8exception_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_end_column_delegate(cef_v8exception_t* self);
        
        // AddRef
        private static IntPtr _p0;
        private static add_ref_delegate _d0;
        
        public static void add_ref(cef_v8exception_t* self)
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
        
        public static int release(cef_v8exception_t* self)
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
        
        public static int has_one_ref(cef_v8exception_t* self)
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
        
        public static int has_at_least_one_ref(cef_v8exception_t* self)
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
        
        // GetMessage
        private static IntPtr _p4;
        private static get_message_delegate _d4;
        
        public static cef_string_userfree* get_message(cef_v8exception_t* self)
        {
            get_message_delegate d;
            var p = self->_get_message;
            if (p == _p4) { d = _d4; }
            else
            {
                d = (get_message_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_message_delegate));
                if (_p4 == IntPtr.Zero) { _d4 = d; _p4 = p; }
            }
            return d(self);
        }
        
        // GetSourceLine
        private static IntPtr _p5;
        private static get_source_line_delegate _d5;
        
        public static cef_string_userfree* get_source_line(cef_v8exception_t* self)
        {
            get_source_line_delegate d;
            var p = self->_get_source_line;
            if (p == _p5) { d = _d5; }
            else
            {
                d = (get_source_line_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_source_line_delegate));
                if (_p5 == IntPtr.Zero) { _d5 = d; _p5 = p; }
            }
            return d(self);
        }
        
        // GetScriptResourceName
        private static IntPtr _p6;
        private static get_script_resource_name_delegate _d6;
        
        public static cef_string_userfree* get_script_resource_name(cef_v8exception_t* self)
        {
            get_script_resource_name_delegate d;
            var p = self->_get_script_resource_name;
            if (p == _p6) { d = _d6; }
            else
            {
                d = (get_script_resource_name_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_script_resource_name_delegate));
                if (_p6 == IntPtr.Zero) { _d6 = d; _p6 = p; }
            }
            return d(self);
        }
        
        // GetLineNumber
        private static IntPtr _p7;
        private static get_line_number_delegate _d7;
        
        public static int get_line_number(cef_v8exception_t* self)
        {
            get_line_number_delegate d;
            var p = self->_get_line_number;
            if (p == _p7) { d = _d7; }
            else
            {
                d = (get_line_number_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_line_number_delegate));
                if (_p7 == IntPtr.Zero) { _d7 = d; _p7 = p; }
            }
            return d(self);
        }
        
        // GetStartPosition
        private static IntPtr _p8;
        private static get_start_position_delegate _d8;
        
        public static int get_start_position(cef_v8exception_t* self)
        {
            get_start_position_delegate d;
            var p = self->_get_start_position;
            if (p == _p8) { d = _d8; }
            else
            {
                d = (get_start_position_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_start_position_delegate));
                if (_p8 == IntPtr.Zero) { _d8 = d; _p8 = p; }
            }
            return d(self);
        }
        
        // GetEndPosition
        private static IntPtr _p9;
        private static get_end_position_delegate _d9;
        
        public static int get_end_position(cef_v8exception_t* self)
        {
            get_end_position_delegate d;
            var p = self->_get_end_position;
            if (p == _p9) { d = _d9; }
            else
            {
                d = (get_end_position_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_end_position_delegate));
                if (_p9 == IntPtr.Zero) { _d9 = d; _p9 = p; }
            }
            return d(self);
        }
        
        // GetStartColumn
        private static IntPtr _pa;
        private static get_start_column_delegate _da;
        
        public static int get_start_column(cef_v8exception_t* self)
        {
            get_start_column_delegate d;
            var p = self->_get_start_column;
            if (p == _pa) { d = _da; }
            else
            {
                d = (get_start_column_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_start_column_delegate));
                if (_pa == IntPtr.Zero) { _da = d; _pa = p; }
            }
            return d(self);
        }
        
        // GetEndColumn
        private static IntPtr _pb;
        private static get_end_column_delegate _db;
        
        public static int get_end_column(cef_v8exception_t* self)
        {
            get_end_column_delegate d;
            var p = self->_get_end_column;
            if (p == _pb) { d = _db; }
            else
            {
                d = (get_end_column_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_end_column_delegate));
                if (_pb == IntPtr.Zero) { _db = d; _pb = p; }
            }
            return d(self);
        }
        
    }
}
