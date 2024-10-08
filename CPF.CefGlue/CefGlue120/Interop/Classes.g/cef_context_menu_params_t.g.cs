//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace CPF.CefGlue.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_context_menu_params_t
    {
        internal cef_base_ref_counted_t _base;
        internal IntPtr _get_xcoord;
        internal IntPtr _get_ycoord;
        internal IntPtr _get_type_flags;
        internal IntPtr _get_link_url;
        internal IntPtr _get_unfiltered_link_url;
        internal IntPtr _get_source_url;
        internal IntPtr _has_image_contents;
        internal IntPtr _get_title_text;
        internal IntPtr _get_page_url;
        internal IntPtr _get_frame_url;
        internal IntPtr _get_frame_charset;
        internal IntPtr _get_media_type;
        internal IntPtr _get_media_state_flags;
        internal IntPtr _get_selection_text;
        internal IntPtr _get_misspelled_word;
        internal IntPtr _get_dictionary_suggestions;
        internal IntPtr _is_editable;
        internal IntPtr _is_spell_check_enabled;
        internal IntPtr _get_edit_state_flags;
        internal IntPtr _is_custom_menu;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate void add_ref_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int release_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_one_ref_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_at_least_one_ref_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_xcoord_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_ycoord_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefContextMenuTypeFlags get_type_flags_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_link_url_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_unfiltered_link_url_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_source_url_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_image_contents_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_title_text_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_page_url_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_frame_url_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_frame_charset_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefContextMenuMediaType get_media_type_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefContextMenuMediaStateFlags get_media_state_flags_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_selection_text_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_misspelled_word_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_dictionary_suggestions_delegate(cef_context_menu_params_t* self, cef_string_list* suggestions);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_editable_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_spell_check_enabled_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefContextMenuEditStateFlags get_edit_state_flags_delegate(cef_context_menu_params_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int is_custom_menu_delegate(cef_context_menu_params_t* self);
        
        // AddRef
        private static IntPtr _p0;
        private static add_ref_delegate _d0;
        
        public static void add_ref(cef_context_menu_params_t* self)
        {
            add_ref_delegate d;
            var p = self->_base._add_ref;
            if (p == _p0) { d = _d0; }
            else
            {
                d = (add_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(add_ref_delegate));
                if (_p0 == IntPtr.Zero) { _d0 = d; _p0 = p; }
            }
            d(self);
        }
        
        // Release
        private static IntPtr _p1;
        private static release_delegate _d1;
        
        public static int release(cef_context_menu_params_t* self)
        {
            release_delegate d;
            var p = self->_base._release;
            if (p == _p1) { d = _d1; }
            else
            {
                d = (release_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(release_delegate));
                if (_p1 == IntPtr.Zero) { _d1 = d; _p1 = p; }
            }
            return d(self);
        }
        
        // HasOneRef
        private static IntPtr _p2;
        private static has_one_ref_delegate _d2;
        
        public static int has_one_ref(cef_context_menu_params_t* self)
        {
            has_one_ref_delegate d;
            var p = self->_base._has_one_ref;
            if (p == _p2) { d = _d2; }
            else
            {
                d = (has_one_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_one_ref_delegate));
                if (_p2 == IntPtr.Zero) { _d2 = d; _p2 = p; }
            }
            return d(self);
        }
        
        // HasAtLeastOneRef
        private static IntPtr _p3;
        private static has_at_least_one_ref_delegate _d3;
        
        public static int has_at_least_one_ref(cef_context_menu_params_t* self)
        {
            has_at_least_one_ref_delegate d;
            var p = self->_base._has_at_least_one_ref;
            if (p == _p3) { d = _d3; }
            else
            {
                d = (has_at_least_one_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_at_least_one_ref_delegate));
                if (_p3 == IntPtr.Zero) { _d3 = d; _p3 = p; }
            }
            return d(self);
        }
        
        // GetXCoord
        private static IntPtr _p4;
        private static get_xcoord_delegate _d4;
        
        public static int get_xcoord(cef_context_menu_params_t* self)
        {
            get_xcoord_delegate d;
            var p = self->_get_xcoord;
            if (p == _p4) { d = _d4; }
            else
            {
                d = (get_xcoord_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_xcoord_delegate));
                if (_p4 == IntPtr.Zero) { _d4 = d; _p4 = p; }
            }
            return d(self);
        }
        
        // GetYCoord
        private static IntPtr _p5;
        private static get_ycoord_delegate _d5;
        
        public static int get_ycoord(cef_context_menu_params_t* self)
        {
            get_ycoord_delegate d;
            var p = self->_get_ycoord;
            if (p == _p5) { d = _d5; }
            else
            {
                d = (get_ycoord_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_ycoord_delegate));
                if (_p5 == IntPtr.Zero) { _d5 = d; _p5 = p; }
            }
            return d(self);
        }
        
        // GetTypeFlags
        private static IntPtr _p6;
        private static get_type_flags_delegate _d6;
        
        public static CefContextMenuTypeFlags get_type_flags(cef_context_menu_params_t* self)
        {
            get_type_flags_delegate d;
            var p = self->_get_type_flags;
            if (p == _p6) { d = _d6; }
            else
            {
                d = (get_type_flags_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_type_flags_delegate));
                if (_p6 == IntPtr.Zero) { _d6 = d; _p6 = p; }
            }
            return d(self);
        }
        
        // GetLinkUrl
        private static IntPtr _p7;
        private static get_link_url_delegate _d7;
        
        public static cef_string_userfree* get_link_url(cef_context_menu_params_t* self)
        {
            get_link_url_delegate d;
            var p = self->_get_link_url;
            if (p == _p7) { d = _d7; }
            else
            {
                d = (get_link_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_link_url_delegate));
                if (_p7 == IntPtr.Zero) { _d7 = d; _p7 = p; }
            }
            return d(self);
        }
        
        // GetUnfilteredLinkUrl
        private static IntPtr _p8;
        private static get_unfiltered_link_url_delegate _d8;
        
        public static cef_string_userfree* get_unfiltered_link_url(cef_context_menu_params_t* self)
        {
            get_unfiltered_link_url_delegate d;
            var p = self->_get_unfiltered_link_url;
            if (p == _p8) { d = _d8; }
            else
            {
                d = (get_unfiltered_link_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_unfiltered_link_url_delegate));
                if (_p8 == IntPtr.Zero) { _d8 = d; _p8 = p; }
            }
            return d(self);
        }
        
        // GetSourceUrl
        private static IntPtr _p9;
        private static get_source_url_delegate _d9;
        
        public static cef_string_userfree* get_source_url(cef_context_menu_params_t* self)
        {
            get_source_url_delegate d;
            var p = self->_get_source_url;
            if (p == _p9) { d = _d9; }
            else
            {
                d = (get_source_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_source_url_delegate));
                if (_p9 == IntPtr.Zero) { _d9 = d; _p9 = p; }
            }
            return d(self);
        }
        
        // HasImageContents
        private static IntPtr _pa;
        private static has_image_contents_delegate _da;
        
        public static int has_image_contents(cef_context_menu_params_t* self)
        {
            has_image_contents_delegate d;
            var p = self->_has_image_contents;
            if (p == _pa) { d = _da; }
            else
            {
                d = (has_image_contents_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_image_contents_delegate));
                if (_pa == IntPtr.Zero) { _da = d; _pa = p; }
            }
            return d(self);
        }
        
        // GetTitleText
        private static IntPtr _pb;
        private static get_title_text_delegate _db;
        
        public static cef_string_userfree* get_title_text(cef_context_menu_params_t* self)
        {
            get_title_text_delegate d;
            var p = self->_get_title_text;
            if (p == _pb) { d = _db; }
            else
            {
                d = (get_title_text_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_title_text_delegate));
                if (_pb == IntPtr.Zero) { _db = d; _pb = p; }
            }
            return d(self);
        }
        
        // GetPageUrl
        private static IntPtr _pc;
        private static get_page_url_delegate _dc;
        
        public static cef_string_userfree* get_page_url(cef_context_menu_params_t* self)
        {
            get_page_url_delegate d;
            var p = self->_get_page_url;
            if (p == _pc) { d = _dc; }
            else
            {
                d = (get_page_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_page_url_delegate));
                if (_pc == IntPtr.Zero) { _dc = d; _pc = p; }
            }
            return d(self);
        }
        
        // GetFrameUrl
        private static IntPtr _pd;
        private static get_frame_url_delegate _dd;
        
        public static cef_string_userfree* get_frame_url(cef_context_menu_params_t* self)
        {
            get_frame_url_delegate d;
            var p = self->_get_frame_url;
            if (p == _pd) { d = _dd; }
            else
            {
                d = (get_frame_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_frame_url_delegate));
                if (_pd == IntPtr.Zero) { _dd = d; _pd = p; }
            }
            return d(self);
        }
        
        // GetFrameCharset
        private static IntPtr _pe;
        private static get_frame_charset_delegate _de;
        
        public static cef_string_userfree* get_frame_charset(cef_context_menu_params_t* self)
        {
            get_frame_charset_delegate d;
            var p = self->_get_frame_charset;
            if (p == _pe) { d = _de; }
            else
            {
                d = (get_frame_charset_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_frame_charset_delegate));
                if (_pe == IntPtr.Zero) { _de = d; _pe = p; }
            }
            return d(self);
        }
        
        // GetMediaType
        private static IntPtr _pf;
        private static get_media_type_delegate _df;
        
        public static CefContextMenuMediaType get_media_type(cef_context_menu_params_t* self)
        {
            get_media_type_delegate d;
            var p = self->_get_media_type;
            if (p == _pf) { d = _df; }
            else
            {
                d = (get_media_type_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_media_type_delegate));
                if (_pf == IntPtr.Zero) { _df = d; _pf = p; }
            }
            return d(self);
        }
        
        // GetMediaStateFlags
        private static IntPtr _p10;
        private static get_media_state_flags_delegate _d10;
        
        public static CefContextMenuMediaStateFlags get_media_state_flags(cef_context_menu_params_t* self)
        {
            get_media_state_flags_delegate d;
            var p = self->_get_media_state_flags;
            if (p == _p10) { d = _d10; }
            else
            {
                d = (get_media_state_flags_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_media_state_flags_delegate));
                if (_p10 == IntPtr.Zero) { _d10 = d; _p10 = p; }
            }
            return d(self);
        }
        
        // GetSelectionText
        private static IntPtr _p11;
        private static get_selection_text_delegate _d11;
        
        public static cef_string_userfree* get_selection_text(cef_context_menu_params_t* self)
        {
            get_selection_text_delegate d;
            var p = self->_get_selection_text;
            if (p == _p11) { d = _d11; }
            else
            {
                d = (get_selection_text_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_text_delegate));
                if (_p11 == IntPtr.Zero) { _d11 = d; _p11 = p; }
            }
            return d(self);
        }
        
        // GetMisspelledWord
        private static IntPtr _p12;
        private static get_misspelled_word_delegate _d12;
        
        public static cef_string_userfree* get_misspelled_word(cef_context_menu_params_t* self)
        {
            get_misspelled_word_delegate d;
            var p = self->_get_misspelled_word;
            if (p == _p12) { d = _d12; }
            else
            {
                d = (get_misspelled_word_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_misspelled_word_delegate));
                if (_p12 == IntPtr.Zero) { _d12 = d; _p12 = p; }
            }
            return d(self);
        }
        
        // GetDictionarySuggestions
        private static IntPtr _p13;
        private static get_dictionary_suggestions_delegate _d13;
        
        public static int get_dictionary_suggestions(cef_context_menu_params_t* self, cef_string_list* suggestions)
        {
            get_dictionary_suggestions_delegate d;
            var p = self->_get_dictionary_suggestions;
            if (p == _p13) { d = _d13; }
            else
            {
                d = (get_dictionary_suggestions_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_dictionary_suggestions_delegate));
                if (_p13 == IntPtr.Zero) { _d13 = d; _p13 = p; }
            }
            return d(self, suggestions);
        }
        
        // IsEditable
        private static IntPtr _p14;
        private static is_editable_delegate _d14;
        
        public static int is_editable(cef_context_menu_params_t* self)
        {
            is_editable_delegate d;
            var p = self->_is_editable;
            if (p == _p14) { d = _d14; }
            else
            {
                d = (is_editable_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_editable_delegate));
                if (_p14 == IntPtr.Zero) { _d14 = d; _p14 = p; }
            }
            return d(self);
        }
        
        // IsSpellCheckEnabled
        private static IntPtr _p15;
        private static is_spell_check_enabled_delegate _d15;
        
        public static int is_spell_check_enabled(cef_context_menu_params_t* self)
        {
            is_spell_check_enabled_delegate d;
            var p = self->_is_spell_check_enabled;
            if (p == _p15) { d = _d15; }
            else
            {
                d = (is_spell_check_enabled_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_spell_check_enabled_delegate));
                if (_p15 == IntPtr.Zero) { _d15 = d; _p15 = p; }
            }
            return d(self);
        }
        
        // GetEditStateFlags
        private static IntPtr _p16;
        private static get_edit_state_flags_delegate _d16;
        
        public static CefContextMenuEditStateFlags get_edit_state_flags(cef_context_menu_params_t* self)
        {
            get_edit_state_flags_delegate d;
            var p = self->_get_edit_state_flags;
            if (p == _p16) { d = _d16; }
            else
            {
                d = (get_edit_state_flags_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_edit_state_flags_delegate));
                if (_p16 == IntPtr.Zero) { _d16 = d; _p16 = p; }
            }
            return d(self);
        }
        
        // IsCustomMenu
        private static IntPtr _p17;
        private static is_custom_menu_delegate _d17;
        
        public static int is_custom_menu(cef_context_menu_params_t* self)
        {
            is_custom_menu_delegate d;
            var p = self->_is_custom_menu;
            if (p == _p17) { d = _d17; }
            else
            {
                d = (is_custom_menu_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(is_custom_menu_delegate));
                if (_p17 == IntPtr.Zero) { _d17 = d; _p17 = p; }
            }
            return d(self);
        }
        
    }
}
