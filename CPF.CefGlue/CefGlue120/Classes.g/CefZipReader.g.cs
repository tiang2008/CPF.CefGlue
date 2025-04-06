//------------------------------------------------------------------------------
// <auto-generated>
//      This file is auto-generated!
//      DO NOT MODIFY!
// </auto-generated>
//------------------------------------------------------------------------------
namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using CPF.CefGlue.Interop;
    
    // Role: PROXY
    #nullable enable
    public sealed unsafe partial class CefZipReader : IDisposable
    {
        internal static CefZipReader FromNative(cef_zip_reader_t* ptr)
        {
            return new CefZipReader(ptr);
        }
        
        internal static CefZipReader FromNativeOrNull(cef_zip_reader_t* ptr)
        {
            if (ptr == null) return null;
            return new CefZipReader(ptr);
        }
        
        private cef_zip_reader_t* _self;
        
        private CefZipReader(cef_zip_reader_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
        }
        
        ~CefZipReader()
        {
            if (_self != null)
            {
                Release();
                _self = null;
            }
        }
        
        public void Dispose()
        {
            if (_self != null)
            {
                Release();
                _self = null;
            }
            GC.SuppressFinalize(this);
        }
        
        internal void AddRef()
        {
            cef_zip_reader_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_zip_reader_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_zip_reader_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_zip_reader_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_zip_reader_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
