﻿//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Xilium.CefGlue.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_dev_tools_message_observer_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void*, UIntPtr, int> _on_dev_tools_message;
        internal delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, int, int, void*, UIntPtr, void> _on_dev_tools_method_result;
        internal delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, cef_string_t*, void*, UIntPtr, void> _on_dev_tools_event;
        internal delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void> _on_dev_tools_agent_attached;
        internal delegate* unmanaged<cef_dev_tools_message_observer_t*, cef_browser_t*, void> _on_dev_tools_agent_detached;
        
        internal GCHandle _obj;
        
        [UnmanagedCallersOnly]
        public static void add_ref(cef_dev_tools_message_observer_t* self)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            obj.add_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int release(cef_dev_tools_message_observer_t* self)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            return obj.release(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_one_ref(cef_dev_tools_message_observer_t* self)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            return obj.has_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_at_least_one_ref(cef_dev_tools_message_observer_t* self)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            return obj.has_at_least_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int on_dev_tools_message(cef_dev_tools_message_observer_t* self, cef_browser_t* browser, void* message, UIntPtr message_size)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            return obj.on_dev_tools_message(self, browser, message, message_size);
        }
        
        [UnmanagedCallersOnly]
        public static void on_dev_tools_method_result(cef_dev_tools_message_observer_t* self, cef_browser_t* browser, int message_id, int success, void* result, UIntPtr result_size)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            obj.on_dev_tools_method_result(self, browser, message_id, success, result, result_size);
        }
        
        [UnmanagedCallersOnly]
        public static void on_dev_tools_event(cef_dev_tools_message_observer_t* self, cef_browser_t* browser, cef_string_t* method, void* @params, UIntPtr params_size)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            obj.on_dev_tools_event(self, browser, method, @params, params_size);
        }
        
        [UnmanagedCallersOnly]
        public static void on_dev_tools_agent_attached(cef_dev_tools_message_observer_t* self, cef_browser_t* browser)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            obj.on_dev_tools_agent_attached(self, browser);
        }
        
        [UnmanagedCallersOnly]
        public static void on_dev_tools_agent_detached(cef_dev_tools_message_observer_t* self, cef_browser_t* browser)
        {
            var obj = (CefDevToolsMessageObserver)self->_obj.Target;
            obj.on_dev_tools_agent_detached(self, browser);
        }
        
        internal static cef_dev_tools_message_observer_t* Alloc()
        {
            var ptr = (cef_dev_tools_message_observer_t*)NativeMemory.Alloc((UIntPtr)sizeof(cef_dev_tools_message_observer_t));
            *ptr = default(cef_dev_tools_message_observer_t);
            ptr->_base._size = (UIntPtr)sizeof(cef_dev_tools_message_observer_t);
            ptr->_base._add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)(delegate* unmanaged<cef_dev_tools_message_observer_t*, void>)&add_ref;
            ptr->_base._release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dev_tools_message_observer_t*, int>)&release;
            ptr->_base._has_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dev_tools_message_observer_t*, int>)&has_one_ref;
            ptr->_base._has_at_least_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dev_tools_message_observer_t*, int>)&has_at_least_one_ref;
            ptr->_on_dev_tools_message = &on_dev_tools_message;
            ptr->_on_dev_tools_method_result = &on_dev_tools_method_result;
            ptr->_on_dev_tools_event = &on_dev_tools_event;
            ptr->_on_dev_tools_agent_attached = &on_dev_tools_agent_attached;
            ptr->_on_dev_tools_agent_detached = &on_dev_tools_agent_detached;
            return ptr;
        }
        
        internal static void Free(cef_dev_tools_message_observer_t* ptr)
        {
            NativeMemory.Free((void*)ptr);
        }
        
    }
}
