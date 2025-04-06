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
    public sealed unsafe partial class CefResponse : IDisposable
    {
        internal static CefResponse FromNative(cef_response_t* ptr)
        {
            return new CefResponse(ptr);
        }
        
        internal static CefResponse FromNativeOrNull(cef_response_t* ptr)
        {
            if (ptr == null) return null;
            return new CefResponse(ptr);
        }
        
        private cef_response_t* _self;
        
        private CefResponse(cef_response_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
        }
        
        ~CefResponse()
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
            cef_response_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_response_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_response_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_response_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_response_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
