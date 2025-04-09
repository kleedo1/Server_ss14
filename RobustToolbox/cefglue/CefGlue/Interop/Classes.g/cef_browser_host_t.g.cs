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
    internal unsafe struct cef_browser_host_t
    {
        internal cef_base_ref_counted_t _base;
        internal delegate* unmanaged<cef_browser_host_t*, cef_browser_t*> _get_browser;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _close_browser;
        internal delegate* unmanaged<cef_browser_host_t*, int> _try_close_browser;
        internal delegate* unmanaged<cef_browser_host_t*, int> _is_ready_to_be_closed;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _set_focus;
        internal delegate* unmanaged<cef_browser_host_t*, IntPtr> _get_window_handle;
        internal delegate* unmanaged<cef_browser_host_t*, IntPtr> _get_opener_window_handle;
        internal delegate* unmanaged<cef_browser_host_t*, int> _get_opener_identifier;
        internal delegate* unmanaged<cef_browser_host_t*, int> _has_view;
        internal delegate* unmanaged<cef_browser_host_t*, cef_client_t*> _get_client;
        internal delegate* unmanaged<cef_browser_host_t*, cef_request_context_t*> _get_request_context;
        internal delegate* unmanaged<cef_browser_host_t*, CefZoomCommand, int> _can_zoom;
        internal delegate* unmanaged<cef_browser_host_t*, CefZoomCommand, void> _zoom;
        internal delegate* unmanaged<cef_browser_host_t*, double> _get_default_zoom_level;
        internal delegate* unmanaged<cef_browser_host_t*, double> _get_zoom_level;
        internal delegate* unmanaged<cef_browser_host_t*, double, void> _set_zoom_level;
        internal delegate* unmanaged<cef_browser_host_t*, CefFileDialogMode, cef_string_t*, cef_string_t*, cef_string_list*, cef_run_file_dialog_callback_t*, void> _run_file_dialog;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> _start_download;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, int, uint, int, cef_download_image_callback_t*, void> _download_image;
        internal delegate* unmanaged<cef_browser_host_t*, void> _print;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, cef_pdf_print_settings_t*, cef_pdf_print_callback_t*, void> _print_to_pdf;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, int, int, int, void> _find;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _stop_finding;
        internal delegate* unmanaged<cef_browser_host_t*, cef_window_info_t*, cef_client_t*, cef_browser_settings_t*, cef_point_t*, void> _show_dev_tools;
        internal delegate* unmanaged<cef_browser_host_t*, void> _close_dev_tools;
        internal delegate* unmanaged<cef_browser_host_t*, int> _has_dev_tools;
        internal delegate* unmanaged<cef_browser_host_t*, void*, UIntPtr, int> _send_dev_tools_message;
        internal delegate* unmanaged<cef_browser_host_t*, int, cef_string_t*, cef_dictionary_value_t*, int> _execute_dev_tools_method;
        internal delegate* unmanaged<cef_browser_host_t*, cef_dev_tools_message_observer_t*, cef_registration_t*> _add_dev_tools_message_observer;
        internal delegate* unmanaged<cef_browser_host_t*, cef_navigation_entry_visitor_t*, int, void> _get_navigation_entries;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> _replace_misspelling;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, void> _add_word_to_dictionary;
        internal delegate* unmanaged<cef_browser_host_t*, int> _is_window_rendering_disabled;
        internal delegate* unmanaged<cef_browser_host_t*, void> _was_resized;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _was_hidden;
        internal delegate* unmanaged<cef_browser_host_t*, void> _notify_screen_info_changed;
        internal delegate* unmanaged<cef_browser_host_t*, CefPaintElementType, void> _invalidate;
        internal delegate* unmanaged<cef_browser_host_t*, void> _send_external_begin_frame;
        internal delegate* unmanaged<cef_browser_host_t*, cef_key_event_t*, void> _send_key_event;
        internal delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, CefMouseButtonType, int, int, void> _send_mouse_click_event;
        internal delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, int, void> _send_mouse_move_event;
        internal delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, int, int, void> _send_mouse_wheel_event;
        internal delegate* unmanaged<cef_browser_host_t*, cef_touch_event_t*, void> _send_touch_event;
        internal delegate* unmanaged<cef_browser_host_t*, void> _send_capture_lost_event;
        internal delegate* unmanaged<cef_browser_host_t*, void> _notify_move_or_resize_started;
        internal delegate* unmanaged<cef_browser_host_t*, int> _get_windowless_frame_rate;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _set_windowless_frame_rate;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, UIntPtr, cef_composition_underline_t*, cef_range_t*, cef_range_t*, void> _ime_set_composition;
        internal delegate* unmanaged<cef_browser_host_t*, cef_string_t*, cef_range_t*, int, void> _ime_commit_text;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _ime_finish_composing_text;
        internal delegate* unmanaged<cef_browser_host_t*, void> _ime_cancel_composition;
        internal delegate* unmanaged<cef_browser_host_t*, cef_drag_data_t*, cef_mouse_event_t*, CefDragOperationsMask, void> _drag_target_drag_enter;
        internal delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, CefDragOperationsMask, void> _drag_target_drag_over;
        internal delegate* unmanaged<cef_browser_host_t*, void> _drag_target_drag_leave;
        internal delegate* unmanaged<cef_browser_host_t*, cef_mouse_event_t*, void> _drag_target_drop;
        internal delegate* unmanaged<cef_browser_host_t*, int, int, CefDragOperationsMask, void> _drag_source_ended_at;
        internal delegate* unmanaged<cef_browser_host_t*, void> _drag_source_system_drag_ended;
        internal delegate* unmanaged<cef_browser_host_t*, cef_navigation_entry_t*> _get_visible_navigation_entry;
        internal delegate* unmanaged<cef_browser_host_t*, CefState, void> _set_accessibility_state;
        internal delegate* unmanaged<cef_browser_host_t*, int, cef_size_t*, cef_size_t*, void> _set_auto_resize_enabled;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _set_audio_muted;
        internal delegate* unmanaged<cef_browser_host_t*, int> _is_audio_muted;
        internal delegate* unmanaged<cef_browser_host_t*, int> _is_fullscreen;
        internal delegate* unmanaged<cef_browser_host_t*, int, void> _exit_fullscreen;
        internal delegate* unmanaged<cef_browser_host_t*, int, int> _can_execute_chrome_command;
        internal delegate* unmanaged<cef_browser_host_t*, int, CefWindowOpenDisposition, void> _execute_chrome_command;
        internal delegate* unmanaged<cef_browser_host_t*, int> _is_render_process_unresponsive;
        internal delegate* unmanaged<cef_browser_host_t*, CefRuntimeStyle> _get_runtime_style;
        
        // CreateBrowser
        [DllImport(libcef.DllName, EntryPoint = "cef_browser_host_create_browser", CallingConvention = libcef.CEF_CALL)]
        public static extern int create_browser(cef_window_info_t* windowInfo, cef_client_t* client, cef_string_t* url, cef_browser_settings_t* settings, cef_dictionary_value_t* extra_info, cef_request_context_t* request_context);
        
        // CreateBrowserSync
        [DllImport(libcef.DllName, EntryPoint = "cef_browser_host_create_browser_sync", CallingConvention = libcef.CEF_CALL)]
        public static extern cef_browser_t* create_browser_sync(cef_window_info_t* windowInfo, cef_client_t* client, cef_string_t* url, cef_browser_settings_t* settings, cef_dictionary_value_t* extra_info, cef_request_context_t* request_context);
        
        // GetBrowserByIdentifier
        [DllImport(libcef.DllName, EntryPoint = "cef_browser_host_get_browser_by_identifier", CallingConvention = libcef.CEF_CALL)]
        public static extern cef_browser_t* get_browser_by_identifier(int browser_id);
        
        // AddRef
        
        public static void add_ref(cef_browser_host_t* self)
        {
            self->_base._add_ref((cef_base_ref_counted_t*)self);
        }
        
        // Release
        
        public static int release(cef_browser_host_t* self)
        {
            return self->_base._release((cef_base_ref_counted_t*)self);
        }
        
        // HasOneRef
        
        public static int has_one_ref(cef_browser_host_t* self)
        {
            return self->_base._has_one_ref((cef_base_ref_counted_t*)self);
        }
        
        // HasAtLeastOneRef
        
        public static int has_at_least_one_ref(cef_browser_host_t* self)
        {
            return self->_base._has_at_least_one_ref((cef_base_ref_counted_t*)self);
        }
        
        // GetBrowser
        
        public static cef_browser_t* get_browser(cef_browser_host_t* self)
        {
            return self->_get_browser(self);
        }
        
        // CloseBrowser
        
        public static void close_browser(cef_browser_host_t* self, int force_close)
        {
            self->_close_browser(self, force_close);
        }
        
        // TryCloseBrowser
        
        public static int try_close_browser(cef_browser_host_t* self)
        {
            return self->_try_close_browser(self);
        }
        
        // IsReadyToBeClosed
        
        public static int is_ready_to_be_closed(cef_browser_host_t* self)
        {
            return self->_is_ready_to_be_closed(self);
        }
        
        // SetFocus
        
        public static void set_focus(cef_browser_host_t* self, int focus)
        {
            self->_set_focus(self, focus);
        }
        
        // GetWindowHandle
        
        public static IntPtr get_window_handle(cef_browser_host_t* self)
        {
            return self->_get_window_handle(self);
        }
        
        // GetOpenerWindowHandle
        
        public static IntPtr get_opener_window_handle(cef_browser_host_t* self)
        {
            return self->_get_opener_window_handle(self);
        }
        
        // GetOpenerIdentifier
        
        public static int get_opener_identifier(cef_browser_host_t* self)
        {
            return self->_get_opener_identifier(self);
        }
        
        // HasView
        
        public static int has_view(cef_browser_host_t* self)
        {
            return self->_has_view(self);
        }
        
        // GetClient
        
        public static cef_client_t* get_client(cef_browser_host_t* self)
        {
            return self->_get_client(self);
        }
        
        // GetRequestContext
        
        public static cef_request_context_t* get_request_context(cef_browser_host_t* self)
        {
            return self->_get_request_context(self);
        }
        
        // CanZoom
        
        public static int can_zoom(cef_browser_host_t* self, CefZoomCommand command)
        {
            return self->_can_zoom(self, command);
        }
        
        // Zoom
        
        public static void zoom(cef_browser_host_t* self, CefZoomCommand command)
        {
            self->_zoom(self, command);
        }
        
        // GetDefaultZoomLevel
        
        public static double get_default_zoom_level(cef_browser_host_t* self)
        {
            return self->_get_default_zoom_level(self);
        }
        
        // GetZoomLevel
        
        public static double get_zoom_level(cef_browser_host_t* self)
        {
            return self->_get_zoom_level(self);
        }
        
        // SetZoomLevel
        
        public static void set_zoom_level(cef_browser_host_t* self, double zoomLevel)
        {
            self->_set_zoom_level(self, zoomLevel);
        }
        
        // RunFileDialog
        
        public static void run_file_dialog(cef_browser_host_t* self, CefFileDialogMode mode, cef_string_t* title, cef_string_t* default_file_path, cef_string_list* accept_filters, cef_run_file_dialog_callback_t* callback)
        {
            self->_run_file_dialog(self, mode, title, default_file_path, accept_filters, callback);
        }
        
        // StartDownload
        
        public static void start_download(cef_browser_host_t* self, cef_string_t* url)
        {
            self->_start_download(self, url);
        }
        
        // DownloadImage
        
        public static void download_image(cef_browser_host_t* self, cef_string_t* image_url, int is_favicon, uint max_image_size, int bypass_cache, cef_download_image_callback_t* callback)
        {
            self->_download_image(self, image_url, is_favicon, max_image_size, bypass_cache, callback);
        }
        
        // Print
        
        public static void print(cef_browser_host_t* self)
        {
            self->_print(self);
        }
        
        // PrintToPDF
        
        public static void print_to_pdf(cef_browser_host_t* self, cef_string_t* path, cef_pdf_print_settings_t* settings, cef_pdf_print_callback_t* callback)
        {
            self->_print_to_pdf(self, path, settings, callback);
        }
        
        // Find
        
        public static void find(cef_browser_host_t* self, cef_string_t* searchText, int forward, int matchCase, int findNext)
        {
            self->_find(self, searchText, forward, matchCase, findNext);
        }
        
        // StopFinding
        
        public static void stop_finding(cef_browser_host_t* self, int clearSelection)
        {
            self->_stop_finding(self, clearSelection);
        }
        
        // ShowDevTools
        
        public static void show_dev_tools(cef_browser_host_t* self, cef_window_info_t* windowInfo, cef_client_t* client, cef_browser_settings_t* settings, cef_point_t* inspect_element_at)
        {
            self->_show_dev_tools(self, windowInfo, client, settings, inspect_element_at);
        }
        
        // CloseDevTools
        
        public static void close_dev_tools(cef_browser_host_t* self)
        {
            self->_close_dev_tools(self);
        }
        
        // HasDevTools
        
        public static int has_dev_tools(cef_browser_host_t* self)
        {
            return self->_has_dev_tools(self);
        }
        
        // SendDevToolsMessage
        
        public static int send_dev_tools_message(cef_browser_host_t* self, void* message, UIntPtr message_size)
        {
            return self->_send_dev_tools_message(self, message, message_size);
        }
        
        // ExecuteDevToolsMethod
        
        public static int execute_dev_tools_method(cef_browser_host_t* self, int message_id, cef_string_t* method, cef_dictionary_value_t* @params)
        {
            return self->_execute_dev_tools_method(self, message_id, method, @params);
        }
        
        // AddDevToolsMessageObserver
        
        public static cef_registration_t* add_dev_tools_message_observer(cef_browser_host_t* self, cef_dev_tools_message_observer_t* observer)
        {
            return self->_add_dev_tools_message_observer(self, observer);
        }
        
        // GetNavigationEntries
        
        public static void get_navigation_entries(cef_browser_host_t* self, cef_navigation_entry_visitor_t* visitor, int current_only)
        {
            self->_get_navigation_entries(self, visitor, current_only);
        }
        
        // ReplaceMisspelling
        
        public static void replace_misspelling(cef_browser_host_t* self, cef_string_t* word)
        {
            self->_replace_misspelling(self, word);
        }
        
        // AddWordToDictionary
        
        public static void add_word_to_dictionary(cef_browser_host_t* self, cef_string_t* word)
        {
            self->_add_word_to_dictionary(self, word);
        }
        
        // IsWindowRenderingDisabled
        
        public static int is_window_rendering_disabled(cef_browser_host_t* self)
        {
            return self->_is_window_rendering_disabled(self);
        }
        
        // WasResized
        
        public static void was_resized(cef_browser_host_t* self)
        {
            self->_was_resized(self);
        }
        
        // WasHidden
        
        public static void was_hidden(cef_browser_host_t* self, int hidden)
        {
            self->_was_hidden(self, hidden);
        }
        
        // NotifyScreenInfoChanged
        
        public static void notify_screen_info_changed(cef_browser_host_t* self)
        {
            self->_notify_screen_info_changed(self);
        }
        
        // Invalidate
        
        public static void invalidate(cef_browser_host_t* self, CefPaintElementType type)
        {
            self->_invalidate(self, type);
        }
        
        // SendExternalBeginFrame
        
        public static void send_external_begin_frame(cef_browser_host_t* self)
        {
            self->_send_external_begin_frame(self);
        }
        
        // SendKeyEvent
        
        public static void send_key_event(cef_browser_host_t* self, cef_key_event_t* @event)
        {
            self->_send_key_event(self, @event);
        }
        
        // SendMouseClickEvent
        
        public static void send_mouse_click_event(cef_browser_host_t* self, cef_mouse_event_t* @event, CefMouseButtonType type, int mouseUp, int clickCount)
        {
            self->_send_mouse_click_event(self, @event, type, mouseUp, clickCount);
        }
        
        // SendMouseMoveEvent
        
        public static void send_mouse_move_event(cef_browser_host_t* self, cef_mouse_event_t* @event, int mouseLeave)
        {
            self->_send_mouse_move_event(self, @event, mouseLeave);
        }
        
        // SendMouseWheelEvent
        
        public static void send_mouse_wheel_event(cef_browser_host_t* self, cef_mouse_event_t* @event, int deltaX, int deltaY)
        {
            self->_send_mouse_wheel_event(self, @event, deltaX, deltaY);
        }
        
        // SendTouchEvent
        
        public static void send_touch_event(cef_browser_host_t* self, cef_touch_event_t* @event)
        {
            self->_send_touch_event(self, @event);
        }
        
        // SendCaptureLostEvent
        
        public static void send_capture_lost_event(cef_browser_host_t* self)
        {
            self->_send_capture_lost_event(self);
        }
        
        // NotifyMoveOrResizeStarted
        
        public static void notify_move_or_resize_started(cef_browser_host_t* self)
        {
            self->_notify_move_or_resize_started(self);
        }
        
        // GetWindowlessFrameRate
        
        public static int get_windowless_frame_rate(cef_browser_host_t* self)
        {
            return self->_get_windowless_frame_rate(self);
        }
        
        // SetWindowlessFrameRate
        
        public static void set_windowless_frame_rate(cef_browser_host_t* self, int frame_rate)
        {
            self->_set_windowless_frame_rate(self, frame_rate);
        }
        
        // ImeSetComposition
        
        public static void ime_set_composition(cef_browser_host_t* self, cef_string_t* text, UIntPtr underlinesCount, cef_composition_underline_t* underlines, cef_range_t* replacement_range, cef_range_t* selection_range)
        {
            self->_ime_set_composition(self, text, underlinesCount, underlines, replacement_range, selection_range);
        }
        
        // ImeCommitText
        
        public static void ime_commit_text(cef_browser_host_t* self, cef_string_t* text, cef_range_t* replacement_range, int relative_cursor_pos)
        {
            self->_ime_commit_text(self, text, replacement_range, relative_cursor_pos);
        }
        
        // ImeFinishComposingText
        
        public static void ime_finish_composing_text(cef_browser_host_t* self, int keep_selection)
        {
            self->_ime_finish_composing_text(self, keep_selection);
        }
        
        // ImeCancelComposition
        
        public static void ime_cancel_composition(cef_browser_host_t* self)
        {
            self->_ime_cancel_composition(self);
        }
        
        // DragTargetDragEnter
        
        public static void drag_target_drag_enter(cef_browser_host_t* self, cef_drag_data_t* drag_data, cef_mouse_event_t* @event, CefDragOperationsMask allowed_ops)
        {
            self->_drag_target_drag_enter(self, drag_data, @event, allowed_ops);
        }
        
        // DragTargetDragOver
        
        public static void drag_target_drag_over(cef_browser_host_t* self, cef_mouse_event_t* @event, CefDragOperationsMask allowed_ops)
        {
            self->_drag_target_drag_over(self, @event, allowed_ops);
        }
        
        // DragTargetDragLeave
        
        public static void drag_target_drag_leave(cef_browser_host_t* self)
        {
            self->_drag_target_drag_leave(self);
        }
        
        // DragTargetDrop
        
        public static void drag_target_drop(cef_browser_host_t* self, cef_mouse_event_t* @event)
        {
            self->_drag_target_drop(self, @event);
        }
        
        // DragSourceEndedAt
        
        public static void drag_source_ended_at(cef_browser_host_t* self, int x, int y, CefDragOperationsMask op)
        {
            self->_drag_source_ended_at(self, x, y, op);
        }
        
        // DragSourceSystemDragEnded
        
        public static void drag_source_system_drag_ended(cef_browser_host_t* self)
        {
            self->_drag_source_system_drag_ended(self);
        }
        
        // GetVisibleNavigationEntry
        
        public static cef_navigation_entry_t* get_visible_navigation_entry(cef_browser_host_t* self)
        {
            return self->_get_visible_navigation_entry(self);
        }
        
        // SetAccessibilityState
        
        public static void set_accessibility_state(cef_browser_host_t* self, CefState accessibility_state)
        {
            self->_set_accessibility_state(self, accessibility_state);
        }
        
        // SetAutoResizeEnabled
        
        public static void set_auto_resize_enabled(cef_browser_host_t* self, int enabled, cef_size_t* min_size, cef_size_t* max_size)
        {
            self->_set_auto_resize_enabled(self, enabled, min_size, max_size);
        }
        
        // SetAudioMuted
        
        public static void set_audio_muted(cef_browser_host_t* self, int mute)
        {
            self->_set_audio_muted(self, mute);
        }
        
        // IsAudioMuted
        
        public static int is_audio_muted(cef_browser_host_t* self)
        {
            return self->_is_audio_muted(self);
        }
        
        // IsFullscreen
        
        public static int is_fullscreen(cef_browser_host_t* self)
        {
            return self->_is_fullscreen(self);
        }
        
        // ExitFullscreen
        
        public static void exit_fullscreen(cef_browser_host_t* self, int will_cause_resize)
        {
            self->_exit_fullscreen(self, will_cause_resize);
        }
        
        // CanExecuteChromeCommand
        
        public static int can_execute_chrome_command(cef_browser_host_t* self, int command_id)
        {
            return self->_can_execute_chrome_command(self, command_id);
        }
        
        // ExecuteChromeCommand
        
        public static void execute_chrome_command(cef_browser_host_t* self, int command_id, CefWindowOpenDisposition disposition)
        {
            self->_execute_chrome_command(self, command_id, disposition);
        }
        
        // IsRenderProcessUnresponsive
        
        public static int is_render_process_unresponsive(cef_browser_host_t* self)
        {
            return self->_is_render_process_unresponsive(self);
        }
        
        // GetRuntimeStyle
        
        public static CefRuntimeStyle get_runtime_style(cef_browser_host_t* self)
        {
            return self->_get_runtime_style(self);
        }
        
    }
}
