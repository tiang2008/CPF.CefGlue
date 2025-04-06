﻿namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using CPF.CefGlue.Interop;

    /// <summary>
    /// Class used to implement browser process callbacks. The methods of this class
    /// will be called on the browser process main thread unless otherwise
    /// indicated.
    /// </summary>
    public abstract unsafe partial class CefBrowserProcessHandler
    {
        private void on_register_custom_preferences(cef_browser_process_handler_t* self, CefPreferencesType type, cef_preference_registrar_t* registrar)
        {
            CheckSelf(self);

            var mRegistrar = CefPreferenceRegistrar.FromNative(registrar);

            try
            {
                OnRegisterCustomPreferences(type, mRegistrar);
            }
            finally
            {
                mRegistrar.ReleaseObject();
            }
        }

        /// <summary>
        /// Provides an opportunity to register custom preferences prior to
        /// global and request context initialization.
        /// If |type| is CEF_PREFERENCES_TYPE_GLOBAL the registered preferences can be
        /// accessed via CefPreferenceManager::GetGlobalPreferences after
        /// OnContextInitialized is called. Global preferences are registered a single
        /// time at application startup. See related cef_settings_t.cache_path and
        /// cef_settings_t.persist_user_preferences configuration.
        /// If |type| is CEF_PREFERENCES_TYPE_REQUEST_CONTEXT the preferences can be
        /// accessed via the CefRequestContext after
        /// CefRequestContextHandler::OnRequestContextInitialized is called. Request
        /// context preferences are registered each time a new CefRequestContext is
        /// created. It is intended but not required that all request contexts have
        /// the same registered preferences. See related
        /// cef_request_context_settings_t.cache_path and
        /// cef_request_context_settings_t.persist_user_preferences configuration.
        /// Do not keep a reference to the |registrar| object. This method is called
        /// on the browser process UI thread.
        /// </summary>
        protected virtual void OnRegisterCustomPreferences(CefPreferencesType type, CefPreferenceRegistrar registrar)
        { }

        private void on_context_initialized(cef_browser_process_handler_t* self)
        {
            CheckSelf(self);

            OnContextInitialized();
        }

        /// <summary>
        /// Called on the browser process UI thread immediately after the CEF context
        /// has been initialized.
        /// </summary>
        protected virtual void OnContextInitialized()
        {
        }


        private void on_before_child_process_launch(cef_browser_process_handler_t* self, cef_command_line_t* command_line)
        {
            CheckSelf(self);

            using (var m_commandLine = CefCommandLine.FromNative(command_line)) 
            {
                OnBeforeChildProcessLaunch(m_commandLine);
            }
        }

        /// <summary>
        /// Called before a child process is launched. Will be called on the browser
        /// process UI thread when launching a render process and on the browser
        /// process IO thread when launching a GPU process. Provides an opportunity to
        /// modify the child process command line. Do not keep a reference to
        /// |command_line| outside of this method.
        /// </summary>
        protected virtual void OnBeforeChildProcessLaunch(CefCommandLine commandLine)
        {
        }


        private int on_already_running_app_relaunch(cef_browser_process_handler_t* self, cef_command_line_t* command_line, cef_string_t* current_directory)
        {
            CheckSelf(self);
            
            var m_commandLine = CefCommandLine.FromNative(command_line);
            var m_current_directory = cef_string_t.ToString(current_directory);

            bool result = OnAlreadyRunningAppRelaunch(ref m_commandLine, ref m_current_directory);

            *command_line = *m_commandLine.ToNative();
            cef_string_t.Copy(m_current_directory, current_directory);

            return result ? 1 : 0;
        }

        protected virtual bool OnAlreadyRunningAppRelaunch(ref CefCommandLine commandLine, ref string currentDirectory)
        {
            return false;
        }


        private void on_schedule_message_pump_work(cef_browser_process_handler_t* self, long delay_ms)
        {
            CheckSelf(self);
            OnScheduleMessagePumpWork(delay_ms);
        }

        /// <summary>
        /// Called from any thread when work has been scheduled for the browser
        /// process main (UI) thread. This callback is used in combination with
        /// cef_settings_t.external_message_pump and CefDoMessageLoopWork() in cases
        /// where the CEF message loop must be integrated into an existing application
        /// message loop (see additional comments and warnings on
        /// CefDoMessageLoopWork). This callback should schedule a
        /// CefDoMessageLoopWork() call to happen on the main (UI) thread. |delay_ms|
        /// is the requested delay in milliseconds. If |delay_ms| is &lt;= 0 then the
        /// call should happen reasonably soon. If |delay_ms| is &gt; 0 then the call
        /// should be scheduled to happen after the specified delay and any currently
        /// pending scheduled call should be cancelled.
        /// </summary>
        protected virtual void OnScheduleMessagePumpWork(long delayMs) { }


        private cef_client_t* get_default_client(cef_browser_process_handler_t* self)
        {
            CheckSelf(self);

            var m_client = GetDefaultClient();

            return m_client != null ? m_client.ToNative() : null;
        }

        /// <summary>
        /// Return the default client for use with a newly created browser window. If
        /// null is returned the browser will be unmanaged (no callbacks will be
        /// executed for that browser) and application shutdown will be blocked until
        /// the browser window is closed manually. This method is currently only used
        /// with the chrome runtime.
        /// </summary>
        protected virtual CefClient? GetDefaultClient() => null;

        private
            cef_request_context_handler_t* get_default_request_context_handler(cef_browser_process_handler_t* self)
        {
            CheckSelf(self);

            CefRequestContextHandler? m_request_context_handler = GetDefaultRequestContextHandler();

            return m_request_context_handler != null ? m_request_context_handler.ToNative() : null;
        }

        /// <summary>
        /// Return the default handler for use with a new user or incognito profile
        /// (CefRequestContext object). If null is returned the CefRequestContext will
        /// be unmanaged (no callbacks will be executed for that CefRequestContext).
        /// This method is currently only used with the Chrome runtime when creating
        /// new browser windows via Chrome UI.
        /// </summary>
        protected virtual CefRequestContextHandler? GetDefaultRequestContextHandler() => null;
    }
}
