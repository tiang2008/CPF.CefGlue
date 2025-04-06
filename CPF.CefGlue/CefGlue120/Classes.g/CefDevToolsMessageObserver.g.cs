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
    public abstract unsafe partial class CefDevToolsMessageObserver
    {
        private static Dictionary<IntPtr, CefDevToolsMessageObserver> _roots = new Dictionary<IntPtr, CefDevToolsMessageObserver>();
        
        private int _refct;
        private cef_dev_tools_message_observer_t* _self;
        
        protected object SyncRoot { get { return this; } }
        
        private cef_dev_tools_message_observer_t.add_ref_delegate _ds0;
        private cef_dev_tools_message_observer_t.release_delegate _ds1;
        private cef_dev_tools_message_observer_t.has_one_ref_delegate _ds2;
        private cef_dev_tools_message_observer_t.has_at_least_one_ref_delegate _ds3;
        private cef_dev_tools_message_observer_t.on_dev_tools_message_delegate _ds4;
        private cef_dev_tools_message_observer_t.on_dev_tools_method_result_delegate _ds5;
        private cef_dev_tools_message_observer_t.on_dev_tools_event_delegate _ds6;
        private cef_dev_tools_message_observer_t.on_dev_tools_agent_attached_delegate _ds7;
        private cef_dev_tools_message_observer_t.on_dev_tools_agent_detached_delegate _ds8;
        
        protected CefDevToolsMessageObserver()
        {
            _self = cef_dev_tools_message_observer_t.Alloc();
        
            _ds0 = new cef_dev_tools_message_observer_t.add_ref_delegate(add_ref);
            _self->_base._add_ref = Marshal.GetFunctionPointerForDelegate(_ds0);
            _ds1 = new cef_dev_tools_message_observer_t.release_delegate(release);
            _self->_base._release = Marshal.GetFunctionPointerForDelegate(_ds1);
            _ds2 = new cef_dev_tools_message_observer_t.has_one_ref_delegate(has_one_ref);
            _self->_base._has_one_ref = Marshal.GetFunctionPointerForDelegate(_ds2);
            _ds3 = new cef_dev_tools_message_observer_t.has_at_least_one_ref_delegate(has_at_least_one_ref);
            _self->_base._has_at_least_one_ref = Marshal.GetFunctionPointerForDelegate(_ds3);
            _ds4 = new cef_dev_tools_message_observer_t.on_dev_tools_message_delegate(on_dev_tools_message);
            _self->_on_dev_tools_message = Marshal.GetFunctionPointerForDelegate(_ds4);
            _ds5 = new cef_dev_tools_message_observer_t.on_dev_tools_method_result_delegate(on_dev_tools_method_result);
            _self->_on_dev_tools_method_result = Marshal.GetFunctionPointerForDelegate(_ds5);
            _ds6 = new cef_dev_tools_message_observer_t.on_dev_tools_event_delegate(on_dev_tools_event);
            _self->_on_dev_tools_event = Marshal.GetFunctionPointerForDelegate(_ds6);
            _ds7 = new cef_dev_tools_message_observer_t.on_dev_tools_agent_attached_delegate(on_dev_tools_agent_attached);
            _self->_on_dev_tools_agent_attached = Marshal.GetFunctionPointerForDelegate(_ds7);
            _ds8 = new cef_dev_tools_message_observer_t.on_dev_tools_agent_detached_delegate(on_dev_tools_agent_detached);
            _self->_on_dev_tools_agent_detached = Marshal.GetFunctionPointerForDelegate(_ds8);
        }
        
        ~CefDevToolsMessageObserver()
        {
            Dispose(false);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_self != null)
            {
                cef_dev_tools_message_observer_t.Free(_self);
                _self = null;
            }
        }
        
        private void add_ref(cef_dev_tools_message_observer_t* self)
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
        
        private int release(cef_dev_tools_message_observer_t* self)
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
        
        private int has_one_ref(cef_dev_tools_message_observer_t* self)
        {
            lock (SyncRoot) { return _refct == 1 ? 1 : 0; }
        }
        
        private int has_at_least_one_ref(cef_dev_tools_message_observer_t* self)
        {
            lock (SyncRoot) { return _refct != 0 ? 1 : 0; }
        }
        
        internal cef_dev_tools_message_observer_t* ToNative()
        {
            add_ref(_self);
            return _self;
        }
        
        [Conditional("DEBUG")]
        private void CheckSelf(cef_dev_tools_message_observer_t* self)
        {
            if (_self != self) throw ExceptionBuilder.InvalidSelfReference();
        }
        
    }
}
