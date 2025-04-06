namespace CPF.CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    internal unsafe struct cef_key_event_t
    {
        public UIntPtr size;
        public CefKeyEventType type;
        public CefEventFlags modifiers;
        public int windows_key_code;
        public int native_key_code;
        public int is_system_key;
        public ushort character;
        public ushort unmodified_character;
        public int focus_on_editable_field;
        
        #region Alloc & Free
        
        private static int _sizeof;

        static cef_key_event_t()
        {
            _sizeof = Marshal.SizeOf(typeof(cef_key_event_t));
        }

        public static cef_key_event_t* Alloc()
        {
            cef_key_event_t* ptr = (cef_key_event_t*)Marshal.AllocHGlobal(_sizeof);
            *ptr = new cef_key_event_t();
            ptr->size = (UIntPtr)_sizeof;
            return ptr;
        }

        public static void Free(cef_key_event_t* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
        
        #endregion
    }
}
