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
    public sealed unsafe partial class CefPrintSettings : IDisposable
    {
        internal static CefPrintSettings FromNative(cef_print_settings_t* ptr)
        {
            return new CefPrintSettings(ptr);
        }
        
        internal static CefPrintSettings FromNativeOrNull(cef_print_settings_t* ptr)
        {
            if (ptr == null) return null;
            return new CefPrintSettings(ptr);
        }
        
        private cef_print_settings_t* _self;
        
        private CefPrintSettings(cef_print_settings_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
        }
        
        ~CefPrintSettings()
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
            cef_print_settings_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_print_settings_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_print_settings_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_print_settings_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_print_settings_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
