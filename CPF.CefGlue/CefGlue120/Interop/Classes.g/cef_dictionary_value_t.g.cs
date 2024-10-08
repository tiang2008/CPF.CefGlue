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
    internal unsafe struct cef_dictionary_value_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _is_valid;
        internal IntPtr _is_owned;
        internal IntPtr _is_read_only;
        internal IntPtr _is_same;
        internal IntPtr _is_equal;
        internal IntPtr _copy;
        internal IntPtr _get_size;
        internal IntPtr _clear;
        internal IntPtr _has_key;
        internal IntPtr _get_keys;
        internal IntPtr _remove;
        internal IntPtr _get_type;
        internal IntPtr _get_value;
        internal IntPtr _get_bool;
        internal IntPtr _get_int;
        internal IntPtr _get_double;
        internal IntPtr _get_string;
        internal IntPtr _get_binary;
        internal IntPtr _get_dictionary;
        internal IntPtr _get_list;
        internal IntPtr _set_value;
        internal IntPtr _set_null;
        internal IntPtr _set_bool;
        internal IntPtr _set_int;
        internal IntPtr _set_double;
        internal IntPtr _set_string;
        internal IntPtr _set_binary;
        internal IntPtr _set_dictionary;
        internal IntPtr _set_list;
        
        // Create
        [DllImport(libcef.DllName, EntryPoint = "cef_dictionary_value_create", CallingConvention = libcef.CEF_CALL)]
        public static extern cef_dictionary_value_t* create();
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void add_ref_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int release_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_one_ref_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_at_least_one_ref_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_valid_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_owned_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_read_only_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_same_delegate(cef_dictionary_value_t* self, cef_dictionary_value_t* that);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_equal_delegate(cef_dictionary_value_t* self, cef_dictionary_value_t* that);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_dictionary_value_t* copy_delegate(cef_dictionary_value_t* self, int exclude_empty_children);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate UIntPtr get_size_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int clear_delegate(cef_dictionary_value_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_key_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_keys_delegate(cef_dictionary_value_t* self, cef_string_list* keys);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int remove_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefValueType get_type_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_value_t* get_value_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_bool_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_int_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate double get_double_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_string_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_binary_value_t* get_binary_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_dictionary_value_t* get_dictionary_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_list_value_t* get_list_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_value_delegate(cef_dictionary_value_t* self, cef_string_t* key, cef_value_t* value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_null_delegate(cef_dictionary_value_t* self, cef_string_t* key);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_bool_delegate(cef_dictionary_value_t* self, cef_string_t* key, int value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_int_delegate(cef_dictionary_value_t* self, cef_string_t* key, int value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_double_delegate(cef_dictionary_value_t* self, cef_string_t* key, double value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_string_delegate(cef_dictionary_value_t* self, cef_string_t* key, cef_string_t* value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_binary_delegate(cef_dictionary_value_t* self, cef_string_t* key, cef_binary_value_t* value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_dictionary_delegate(cef_dictionary_value_t* self, cef_string_t* key, cef_dictionary_value_t* value);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int set_list_delegate(cef_dictionary_value_t* self, cef_string_t* key, cef_list_value_t* value);
        
        // AddRef
        private static IntPtr _p0;
        private static add_ref_delegate _d0;
        
        public static void add_ref(cef_dictionary_value_t* self)
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
        
        public static int release(cef_dictionary_value_t* self)
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
        
        public static int has_one_ref(cef_dictionary_value_t* self)
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
        
        public static int has_at_least_one_ref(cef_dictionary_value_t* self)
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
        
        // IsValid
        private static IntPtr _p4;
        private static is_valid_delegate _d4;
        
        public static int is_valid(cef_dictionary_value_t* self)
        {
            is_valid_delegate d;
            var p = self->_is_valid;
            if (p == _p4) { d = _d4; }
            else
            {
                d = (is_valid_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_valid_delegate));
                if (_p4 == IntPtr.Zero) { _d4 = d; _p4 = p; }
            }
            return d(self);
        }
        
        // IsOwned
        private static IntPtr _p5;
        private static is_owned_delegate _d5;
        
        public static int is_owned(cef_dictionary_value_t* self)
        {
            is_owned_delegate d;
            var p = self->_is_owned;
            if (p == _p5) { d = _d5; }
            else
            {
                d = (is_owned_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_owned_delegate));
                if (_p5 == IntPtr.Zero) { _d5 = d; _p5 = p; }
            }
            return d(self);
        }
        
        // IsReadOnly
        private static IntPtr _p6;
        private static is_read_only_delegate _d6;
        
        public static int is_read_only(cef_dictionary_value_t* self)
        {
            is_read_only_delegate d;
            var p = self->_is_read_only;
            if (p == _p6) { d = _d6; }
            else
            {
                d = (is_read_only_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_read_only_delegate));
                if (_p6 == IntPtr.Zero) { _d6 = d; _p6 = p; }
            }
            return d(self);
        }
        
        // IsSame
        private static IntPtr _p7;
        private static is_same_delegate _d7;
        
        public static int is_same(cef_dictionary_value_t* self, cef_dictionary_value_t* that)
        {
            is_same_delegate d;
            var p = self->_is_same;
            if (p == _p7) { d = _d7; }
            else
            {
                d = (is_same_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_same_delegate));
                if (_p7 == IntPtr.Zero) { _d7 = d; _p7 = p; }
            }
            return d(self, that);
        }
        
        // IsEqual
        private static IntPtr _p8;
        private static is_equal_delegate _d8;
        
        public static int is_equal(cef_dictionary_value_t* self, cef_dictionary_value_t* that)
        {
            is_equal_delegate d;
            var p = self->_is_equal;
            if (p == _p8) { d = _d8; }
            else
            {
                d = (is_equal_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_equal_delegate));
                if (_p8 == IntPtr.Zero) { _d8 = d; _p8 = p; }
            }
            return d(self, that);
        }
        
        // Copy
        private static IntPtr _p9;
        private static copy_delegate _d9;
        
        public static cef_dictionary_value_t* copy(cef_dictionary_value_t* self, int exclude_empty_children)
        {
            copy_delegate d;
            var p = self->_copy;
            if (p == _p9) { d = _d9; }
            else
            {
                d = (copy_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(copy_delegate));
                if (_p9 == IntPtr.Zero) { _d9 = d; _p9 = p; }
            }
            return d(self, exclude_empty_children);
        }
        
        // GetSize
        private static IntPtr _pa;
        private static get_size_delegate _da;
        
        public static UIntPtr get_size(cef_dictionary_value_t* self)
        {
            get_size_delegate d;
            var p = self->_get_size;
            if (p == _pa) { d = _da; }
            else
            {
                d = (get_size_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_size_delegate));
                if (_pa == IntPtr.Zero) { _da = d; _pa = p; }
            }
            return d(self);
        }
        
        // Clear
        private static IntPtr _pb;
        private static clear_delegate _db;
        
        public static int clear(cef_dictionary_value_t* self)
        {
            clear_delegate d;
            var p = self->_clear;
            if (p == _pb) { d = _db; }
            else
            {
                d = (clear_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(clear_delegate));
                if (_pb == IntPtr.Zero) { _db = d; _pb = p; }
            }
            return d(self);
        }
        
        // HasKey
        private static IntPtr _pc;
        private static has_key_delegate _dc;
        
        public static int has_key(cef_dictionary_value_t* self, cef_string_t* key)
        {
            has_key_delegate d;
            var p = self->_has_key;
            if (p == _pc) { d = _dc; }
            else
            {
                d = (has_key_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_key_delegate));
                if (_pc == IntPtr.Zero) { _dc = d; _pc = p; }
            }
            return d(self, key);
        }
        
        // GetKeys
        private static IntPtr _pd;
        private static get_keys_delegate _dd;
        
        public static int get_keys(cef_dictionary_value_t* self, cef_string_list* keys)
        {
            get_keys_delegate d;
            var p = self->_get_keys;
            if (p == _pd) { d = _dd; }
            else
            {
                d = (get_keys_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_keys_delegate));
                if (_pd == IntPtr.Zero) { _dd = d; _pd = p; }
            }
            return d(self, keys);
        }
        
        // Remove
        private static IntPtr _pe;
        private static remove_delegate _de;
        
        public static int remove(cef_dictionary_value_t* self, cef_string_t* key)
        {
            remove_delegate d;
            var p = self->_remove;
            if (p == _pe) { d = _de; }
            else
            {
                d = (remove_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(remove_delegate));
                if (_pe == IntPtr.Zero) { _de = d; _pe = p; }
            }
            return d(self, key);
        }
        
        // GetType
        private static IntPtr _pf;
        private static get_type_delegate _df;
        
        public static CefValueType get_type(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_type_delegate d;
            var p = self->_get_type;
            if (p == _pf) { d = _df; }
            else
            {
                d = (get_type_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_type_delegate));
                if (_pf == IntPtr.Zero) { _df = d; _pf = p; }
            }
            return d(self, key);
        }
        
        // GetValue
        private static IntPtr _p10;
        private static get_value_delegate _d10;
        
        public static cef_value_t* get_value(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_value_delegate d;
            var p = self->_get_value;
            if (p == _p10) { d = _d10; }
            else
            {
                d = (get_value_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_value_delegate));
                if (_p10 == IntPtr.Zero) { _d10 = d; _p10 = p; }
            }
            return d(self, key);
        }
        
        // GetBool
        private static IntPtr _p11;
        private static get_bool_delegate _d11;
        
        public static int get_bool(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_bool_delegate d;
            var p = self->_get_bool;
            if (p == _p11) { d = _d11; }
            else
            {
                d = (get_bool_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_bool_delegate));
                if (_p11 == IntPtr.Zero) { _d11 = d; _p11 = p; }
            }
            return d(self, key);
        }
        
        // GetInt
        private static IntPtr _p12;
        private static get_int_delegate _d12;
        
        public static int get_int(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_int_delegate d;
            var p = self->_get_int;
            if (p == _p12) { d = _d12; }
            else
            {
                d = (get_int_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_int_delegate));
                if (_p12 == IntPtr.Zero) { _d12 = d; _p12 = p; }
            }
            return d(self, key);
        }
        
        // GetDouble
        private static IntPtr _p13;
        private static get_double_delegate _d13;
        
        public static double get_double(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_double_delegate d;
            var p = self->_get_double;
            if (p == _p13) { d = _d13; }
            else
            {
                d = (get_double_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_double_delegate));
                if (_p13 == IntPtr.Zero) { _d13 = d; _p13 = p; }
            }
            return d(self, key);
        }
        
        // GetString
        private static IntPtr _p14;
        private static get_string_delegate _d14;
        
        public static cef_string_userfree* get_string(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_string_delegate d;
            var p = self->_get_string;
            if (p == _p14) { d = _d14; }
            else
            {
                d = (get_string_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_string_delegate));
                if (_p14 == IntPtr.Zero) { _d14 = d; _p14 = p; }
            }
            return d(self, key);
        }
        
        // GetBinary
        private static IntPtr _p15;
        private static get_binary_delegate _d15;
        
        public static cef_binary_value_t* get_binary(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_binary_delegate d;
            var p = self->_get_binary;
            if (p == _p15) { d = _d15; }
            else
            {
                d = (get_binary_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_binary_delegate));
                if (_p15 == IntPtr.Zero) { _d15 = d; _p15 = p; }
            }
            return d(self, key);
        }
        
        // GetDictionary
        private static IntPtr _p16;
        private static get_dictionary_delegate _d16;
        
        public static cef_dictionary_value_t* get_dictionary(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_dictionary_delegate d;
            var p = self->_get_dictionary;
            if (p == _p16) { d = _d16; }
            else
            {
                d = (get_dictionary_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_dictionary_delegate));
                if (_p16 == IntPtr.Zero) { _d16 = d; _p16 = p; }
            }
            return d(self, key);
        }
        
        // GetList
        private static IntPtr _p17;
        private static get_list_delegate _d17;
        
        public static cef_list_value_t* get_list(cef_dictionary_value_t* self, cef_string_t* key)
        {
            get_list_delegate d;
            var p = self->_get_list;
            if (p == _p17) { d = _d17; }
            else
            {
                d = (get_list_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_list_delegate));
                if (_p17 == IntPtr.Zero) { _d17 = d; _p17 = p; }
            }
            return d(self, key);
        }
        
        // SetValue
        private static IntPtr _p18;
        private static set_value_delegate _d18;
        
        public static int set_value(cef_dictionary_value_t* self, cef_string_t* key, cef_value_t* value)
        {
            set_value_delegate d;
            var p = self->_set_value;
            if (p == _p18) { d = _d18; }
            else
            {
                d = (set_value_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_value_delegate));
                if (_p18 == IntPtr.Zero) { _d18 = d; _p18 = p; }
            }
            return d(self, key, value);
        }
        
        // SetNull
        private static IntPtr _p19;
        private static set_null_delegate _d19;
        
        public static int set_null(cef_dictionary_value_t* self, cef_string_t* key)
        {
            set_null_delegate d;
            var p = self->_set_null;
            if (p == _p19) { d = _d19; }
            else
            {
                d = (set_null_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_null_delegate));
                if (_p19 == IntPtr.Zero) { _d19 = d; _p19 = p; }
            }
            return d(self, key);
        }
        
        // SetBool
        private static IntPtr _p1a;
        private static set_bool_delegate _d1a;
        
        public static int set_bool(cef_dictionary_value_t* self, cef_string_t* key, int value)
        {
            set_bool_delegate d;
            var p = self->_set_bool;
            if (p == _p1a) { d = _d1a; }
            else
            {
                d = (set_bool_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_bool_delegate));
                if (_p1a == IntPtr.Zero) { _d1a = d; _p1a = p; }
            }
            return d(self, key, value);
        }
        
        // SetInt
        private static IntPtr _p1b;
        private static set_int_delegate _d1b;
        
        public static int set_int(cef_dictionary_value_t* self, cef_string_t* key, int value)
        {
            set_int_delegate d;
            var p = self->_set_int;
            if (p == _p1b) { d = _d1b; }
            else
            {
                d = (set_int_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_int_delegate));
                if (_p1b == IntPtr.Zero) { _d1b = d; _p1b = p; }
            }
            return d(self, key, value);
        }
        
        // SetDouble
        private static IntPtr _p1c;
        private static set_double_delegate _d1c;
        
        public static int set_double(cef_dictionary_value_t* self, cef_string_t* key, double value)
        {
            set_double_delegate d;
            var p = self->_set_double;
            if (p == _p1c) { d = _d1c; }
            else
            {
                d = (set_double_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_double_delegate));
                if (_p1c == IntPtr.Zero) { _d1c = d; _p1c = p; }
            }
            return d(self, key, value);
        }
        
        // SetString
        private static IntPtr _p1d;
        private static set_string_delegate _d1d;
        
        public static int set_string(cef_dictionary_value_t* self, cef_string_t* key, cef_string_t* value)
        {
            set_string_delegate d;
            var p = self->_set_string;
            if (p == _p1d) { d = _d1d; }
            else
            {
                d = (set_string_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_string_delegate));
                if (_p1d == IntPtr.Zero) { _d1d = d; _p1d = p; }
            }
            return d(self, key, value);
        }
        
        // SetBinary
        private static IntPtr _p1e;
        private static set_binary_delegate _d1e;
        
        public static int set_binary(cef_dictionary_value_t* self, cef_string_t* key, cef_binary_value_t* value)
        {
            set_binary_delegate d;
            var p = self->_set_binary;
            if (p == _p1e) { d = _d1e; }
            else
            {
                d = (set_binary_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_binary_delegate));
                if (_p1e == IntPtr.Zero) { _d1e = d; _p1e = p; }
            }
            return d(self, key, value);
        }
        
        // SetDictionary
        private static IntPtr _p1f;
        private static set_dictionary_delegate _d1f;
        
        public static int set_dictionary(cef_dictionary_value_t* self, cef_string_t* key, cef_dictionary_value_t* value)
        {
            set_dictionary_delegate d;
            var p = self->_set_dictionary;
            if (p == _p1f) { d = _d1f; }
            else
            {
                d = (set_dictionary_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_dictionary_delegate));
                if (_p1f == IntPtr.Zero) { _d1f = d; _p1f = p; }
            }
            return d(self, key, value);
        }
        
        // SetList
        private static IntPtr _p20;
        private static set_list_delegate _d20;
        
        public static int set_list(cef_dictionary_value_t* self, cef_string_t* key, cef_list_value_t* value)
        {
            set_list_delegate d;
            var p = self->_set_list;
            if (p == _p20) { d = _d20; }
            else
            {
                d = (set_list_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(set_list_delegate));
                if (_p20 == IntPtr.Zero) { _d20 = d; _p20 = p; }
            }
            return d(self, key, value);
        }
        
    }
}
