//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using CPF.CefGlue.Interop;
    
    // Role: PROXY
    public sealed unsafe partial class CefUrlRequest : IDisposable
    {
        internal static CefUrlRequest FromNative(cef_urlrequest_t* ptr)
        {
            return new CefUrlRequest(ptr);
        }
        
        internal static CefUrlRequest FromNativeOrNull(cef_urlrequest_t* ptr)
        {
            if (ptr == null) return null;
            return new CefUrlRequest(ptr);
        }
        
        private cef_urlrequest_t* _self;
        private int _disposed = 0;
        
        private CefUrlRequest(cef_urlrequest_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
            CefObjectTracker.Track(this);
        }
        
        ~CefUrlRequest()
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
            {
                Release();
                _self = null;
            }
        }
        
        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 0)
            {
                Release();
                _self = null;
            }
            CefObjectTracker.Untrack(this);
            GC.SuppressFinalize(this);
        }
        
        internal void AddRef()
        {
            cef_urlrequest_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_urlrequest_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_urlrequest_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_urlrequest_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_urlrequest_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
