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
    public sealed unsafe partial class CefDownloadItemCallback : IDisposable
    {
        internal static CefDownloadItemCallback FromNative(cef_download_item_callback_t* ptr)
        {
            return new CefDownloadItemCallback(ptr);
        }
        
        internal static CefDownloadItemCallback FromNativeOrNull(cef_download_item_callback_t* ptr)
        {
            if (ptr == null) return null;
            return new CefDownloadItemCallback(ptr);
        }
        
        private cef_download_item_callback_t* _self;
        
        private CefDownloadItemCallback(cef_download_item_callback_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
        }
        
        ~CefDownloadItemCallback()
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
            cef_download_item_callback_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_download_item_callback_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_download_item_callback_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_download_item_callback_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_download_item_callback_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
