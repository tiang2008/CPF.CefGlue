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
    public sealed unsafe partial class CefV8Value : IDisposable
    {
        internal static CefV8Value FromNative(cef_v8value_t* ptr)
        {
            return new CefV8Value(ptr);
        }
        
        internal static CefV8Value FromNativeOrNull(cef_v8value_t* ptr)
        {
            if (ptr == null) return null;
            return new CefV8Value(ptr);
        }
        
        private cef_v8value_t* _self;
        private int _disposed = 0;
        
        private CefV8Value(cef_v8value_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
            CefObjectTracker.Track(this);
        }
        
        ~CefV8Value()
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
            cef_v8value_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_v8value_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_v8value_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_v8value_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_v8value_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
