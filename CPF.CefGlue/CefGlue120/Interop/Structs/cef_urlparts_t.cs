﻿//
// This file manually written from cef/include/internal/cef_types.h.
//
namespace CPF.CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    internal unsafe struct cef_urlparts_t
    {
        public UIntPtr size;
        public cef_string_t spec;
        public cef_string_t scheme;
        public cef_string_t username;
        public cef_string_t password;
        public cef_string_t host;
        public cef_string_t port;
        public cef_string_t origin;
        public cef_string_t path;
        public cef_string_t query;
        public cef_string_t fragment;

        internal static void Clear(cef_urlparts_t* self)
        {
            libcef.string_clear(&self->spec);
            libcef.string_clear(&self->scheme);
            libcef.string_clear(&self->username);
            libcef.string_clear(&self->password);
            libcef.string_clear(&self->host);
            libcef.string_clear(&self->port);
            libcef.string_clear(&self->origin);
            libcef.string_clear(&self->path);
            libcef.string_clear(&self->query);
            libcef.string_clear(&self->fragment);
        }
        
        #region Alloc & Free
        private static int _sizeof;

        static cef_urlparts_t()
        {
            _sizeof = Marshal.SizeOf(typeof(cef_urlparts_t));
        }

        public static cef_urlparts_t* Alloc()
        {
            var ptr = (cef_urlparts_t*)Marshal.AllocHGlobal(_sizeof);
            *ptr = new cef_urlparts_t();
            ptr->size = (UIntPtr)_sizeof;
            return ptr;
        }

        public static void Free(cef_urlparts_t* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
        #endregion
    }
}
