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
    public sealed unsafe partial class CefProcessMessage : IDisposable
    {
        internal static CefProcessMessage FromNative(cef_process_message_t* ptr)
        {
            return new CefProcessMessage(ptr);
        }
        
        internal static CefProcessMessage FromNativeOrNull(cef_process_message_t* ptr)
        {
            if (ptr == null) return null;
            return new CefProcessMessage(ptr);
        }
        
        private cef_process_message_t* _self;
        private int _disposed = 0;
        
        private CefProcessMessage(cef_process_message_t* ptr)
        {
            if (ptr == null) throw new ArgumentNullException("ptr");
            _self = ptr;
            CefObjectTracker.Track(this);
        }
        
        ~CefProcessMessage()
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
            cef_process_message_t.add_ref(_self);
        }
        
        internal bool Release()
        {
            return cef_process_message_t.release(_self) != 0;
        }
        
        internal bool HasOneRef
        {
            get { return cef_process_message_t.has_one_ref(_self) != 0; }
        }
        
        internal bool HasAtLeastOneRef
        {
            get { return cef_process_message_t.has_at_least_one_ref(_self) != 0; }
        }
        
        internal cef_process_message_t* ToNative()
        {
            AddRef();
            return _self;
        }
    }
}
