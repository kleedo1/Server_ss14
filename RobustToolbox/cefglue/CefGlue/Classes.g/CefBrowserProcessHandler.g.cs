﻿//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Xilium.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Xilium.CefGlue.Interop;
    
    // Role: HANDLER
    public abstract unsafe partial class CefBrowserProcessHandler
    {
        private int _refct;
        private cef_browser_process_handler_t* _self;
        
        protected object SyncRoot { get { return this; } }
        
        protected CefBrowserProcessHandler()
        {
            _self = cef_browser_process_handler_t.Alloc();
        }
        
        ~CefBrowserProcessHandler()
        {
            Dispose(false);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_self != null)
            {
                cef_browser_process_handler_t.Free(_self);
                _self = null;
            }
        }
        
        internal void add_ref(cef_browser_process_handler_t* self)
        {
            lock (SyncRoot)
            {
                var result = ++_refct;
                if (result == 1)
                {
                    _self->_obj = GCHandle.Alloc(this);
                }
            }
        }
        
        internal int release(cef_browser_process_handler_t* self)
        {
            lock (SyncRoot)
            {
                var result = --_refct;
                if (result == 0)
                {
                    _self->_obj.Free();
                    return 1;
                }
                return 0;
            }
        }
        
        internal int has_one_ref(cef_browser_process_handler_t* self)
        {
            lock (SyncRoot) { return _refct == 1 ? 1 : 0; }
        }
        
        internal int has_at_least_one_ref(cef_browser_process_handler_t* self)
        {
            lock (SyncRoot) { return _refct != 0 ? 1 : 0; }
        }
        
        internal cef_browser_process_handler_t* ToNative()
        {
            add_ref(_self);
            return _self;
        }
        
        [Conditional("DEBUG")]
        private void CheckSelf(cef_browser_process_handler_t* self)
        {
            if (_self != self) throw ExceptionBuilder.InvalidSelfReference();
        }
        
    }
}
