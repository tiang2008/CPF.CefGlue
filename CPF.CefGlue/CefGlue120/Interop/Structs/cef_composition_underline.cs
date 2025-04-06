//
// This file manually written from cef/include/internal/cef_types.h.
//
namespace CPF.CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    internal unsafe struct cef_composition_underline_t
    {
        public UIntPtr size;
        public cef_range_t range;
        public uint color;
        public uint background_color;
        public int thick;
        public CefCompositionUnderlineStyle style;
        
        #region Alloc & Free
        private static int _sizeof;

        static cef_composition_underline_t()
        {
            _sizeof = Marshal.SizeOf(typeof(cef_composition_underline_t));
        }

        public static cef_composition_underline_t* Alloc()
        {
            var ptr = (cef_composition_underline_t*)Marshal.AllocHGlobal(_sizeof);
            *ptr = new cef_composition_underline_t();
            ptr->size = (UIntPtr)_sizeof;
            return ptr;
        }

        public static void Free(cef_composition_underline_t* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
        #endregion
    }
}
