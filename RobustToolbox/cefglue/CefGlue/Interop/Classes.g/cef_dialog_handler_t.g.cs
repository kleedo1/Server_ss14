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
    internal unsafe struct cef_dialog_handler_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_dialog_handler_t*, cef_browser_t*, CefFileDialogMode, cef_string_t*, cef_string_t*, cef_string_list*, cef_string_list*, cef_string_list*, cef_file_dialog_callback_t*, int> _on_file_dialog;
        
        internal GCHandle _obj;
        
        [UnmanagedCallersOnly]
        public static void add_ref(cef_dialog_handler_t* self)
        {
            var obj = (CefDialogHandler)self->_obj.Target;
            obj.add_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int release(cef_dialog_handler_t* self)
        {
            var obj = (CefDialogHandler)self->_obj.Target;
            return obj.release(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_one_ref(cef_dialog_handler_t* self)
        {
            var obj = (CefDialogHandler)self->_obj.Target;
            return obj.has_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int has_at_least_one_ref(cef_dialog_handler_t* self)
        {
            var obj = (CefDialogHandler)self->_obj.Target;
            return obj.has_at_least_one_ref(self);
        }
        
        [UnmanagedCallersOnly]
        public static int on_file_dialog(cef_dialog_handler_t* self, cef_browser_t* browser, CefFileDialogMode mode, cef_string_t* title, cef_string_t* default_file_path, cef_string_list* accept_filters, cef_string_list* accept_extensions, cef_string_list* accept_descriptions, cef_file_dialog_callback_t* callback)
        {
            var obj = (CefDialogHandler)self->_obj.Target;
            return obj.on_file_dialog(self, browser, mode, title, default_file_path, accept_filters, accept_extensions, accept_descriptions, callback);
        }
        
        internal static cef_dialog_handler_t* Alloc()
        {
            var ptr = (cef_dialog_handler_t*)NativeMemory.Alloc((UIntPtr)sizeof(cef_dialog_handler_t));
            *ptr = default(cef_dialog_handler_t);
            ptr->_base._size = (UIntPtr)sizeof(cef_dialog_handler_t);
            ptr->_base._add_ref = (delegate* unmanaged<cef_base_ref_counted_t*, void>)(delegate* unmanaged<cef_dialog_handler_t*, void>)&add_ref;
            ptr->_base._release = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dialog_handler_t*, int>)&release;
            ptr->_base._has_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dialog_handler_t*, int>)&has_one_ref;
            ptr->_base._has_at_least_one_ref = (delegate* unmanaged<cef_base_ref_counted_t*, int>)(delegate* unmanaged<cef_dialog_handler_t*, int>)&has_at_least_one_ref;
            ptr->_on_file_dialog = &on_file_dialog;
            return ptr;
        }
        
        internal static void Free(cef_dialog_handler_t* ptr)
        {
            NativeMemory.Free((void*)ptr);
        }
        
    }
}
