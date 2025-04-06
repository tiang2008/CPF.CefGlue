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
    
    // Role: HANDLER
    #nullable enable
    public abstract unsafe partial class CefPermissionHandler
    {
        private static Dictionary<IntPtr, CefPermissionHandler> _roots = new Dictionary<IntPtr, CefPermissionHandler>();
        
        private int _refct;
        private cef_permission_handler_t* _self;
        
        protected object SyncRoot { get { return this; } }
        
        private cef_permission_handler_t.add_ref_delegate _ds0;
        private cef_permission_handler_t.release_delegate _ds1;
        private cef_permission_handler_t.has_one_ref_delegate _ds2;
        private cef_permission_handler_t.has_at_least_one_ref_delegate _ds3;
        private cef_permission_handler_t.on_request_media_access_permission_delegate _ds4;
        private cef_permission_handler_t.on_show_permission_prompt_delegate _ds5;
        private cef_permission_handler_t.on_dismiss_permission_prompt_delegate _ds6;
        
        protected CefPermissionHandler()
        {
            _self = cef_permission_handler_t.Alloc();
        
            _ds0 = new cef_permission_handler_t.add_ref_delegate(add_ref);
            _self->_base._add_ref = Marshal.GetFunctionPointerForDelegate(_ds0);
            _ds1 = new cef_permission_handler_t.release_delegate(release);
            _self->_base._release = Marshal.GetFunctionPointerForDelegate(_ds1);
            _ds2 = new cef_permission_handler_t.has_one_ref_delegate(has_one_ref);
            _self->_base._has_one_ref = Marshal.GetFunctionPointerForDelegate(_ds2);
            _ds3 = new cef_permission_handler_t.has_at_least_one_ref_delegate(has_at_least_one_ref);
            _self->_base._has_at_least_one_ref = Marshal.GetFunctionPointerForDelegate(_ds3);
            _ds4 = new cef_permission_handler_t.on_request_media_access_permission_delegate(on_request_media_access_permission);
            _self->_on_request_media_access_permission = Marshal.GetFunctionPointerForDelegate(_ds4);
            _ds5 = new cef_permission_handler_t.on_show_permission_prompt_delegate(on_show_permission_prompt);
            _self->_on_show_permission_prompt = Marshal.GetFunctionPointerForDelegate(_ds5);
            _ds6 = new cef_permission_handler_t.on_dismiss_permission_prompt_delegate(on_dismiss_permission_prompt);
            _self->_on_dismiss_permission_prompt = Marshal.GetFunctionPointerForDelegate(_ds6);
        }
        
        ~CefPermissionHandler()
        {
            Dispose(false);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_self != null)
            {
                cef_permission_handler_t.Free(_self);
                _self = null;
            }
        }
        
        private void add_ref(cef_permission_handler_t* self)
        {
            lock (SyncRoot)
            {
                var result = ++_refct;
                if (result == 1)
                {
                    lock (_roots) { _roots.Add((IntPtr)_self, this); }
                }
            }
        }
        
        private int release(cef_permission_handler_t* self)
        {
            lock (SyncRoot)
            {
                var result = --_refct;
                if (result == 0)
                {
                    lock (_roots) { _roots.Remove((IntPtr)_self); }
                    return 1;
                }
                return 0;
            }
        }
        
        private int has_one_ref(cef_permission_handler_t* self)
        {
            lock (SyncRoot) { return _refct == 1 ? 1 : 0; }
        }
        
        private int has_at_least_one_ref(cef_permission_handler_t* self)
        {
            lock (SyncRoot) { return _refct != 0 ? 1 : 0; }
        }
        
        internal cef_permission_handler_t* ToNative()
        {
            add_ref(_self);
            return _self;
        }
        
        [Conditional("DEBUG")]
        private void CheckSelf(cef_permission_handler_t* self)
        {
            if (_self != self) throw ExceptionBuilder.InvalidSelfReference();
        }
        
    }
}
