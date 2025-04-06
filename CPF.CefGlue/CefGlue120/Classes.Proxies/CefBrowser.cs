using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

/// <summary>
///     Class used to represent a browser. When used in the browser process the
///     methods of this class may be called on any thread unless otherwise indicated
///     in the comments. When used in the render process the methods of this class
///     may only be called on the main thread.
/// </summary>
public sealed unsafe partial class CefBrowser
{
    /// <summary>
    ///     True if this object is currently valid. This will return false after
    ///     CefLifeSpanHandler::OnBeforeClose is called.
    /// </summary>
    public bool IsValid => cef_browser_t.is_valid(_self) != 0;

    /// <summary>
    ///     Returns true if the browser can navigate backwards.
    /// </summary>
    public bool CanGoBack => cef_browser_t.can_go_back(_self) != 0;

    /// <summary>
    ///     Returns true if the browser can navigate forwards.
    /// </summary>
    public bool CanGoForward => cef_browser_t.can_go_forward(_self) != 0;

    /// <summary>
    ///     Returns true if the browser is currently loading.
    /// </summary>
    public bool IsLoading => cef_browser_t.is_loading(_self) != 0;

    /// <summary>
    ///     Returns the globally unique identifier for this browser. This value is also
    ///     used as the tabId for extension APIs.
    /// </summary>
    public int Identifier => cef_browser_t.get_identifier(_self);

    /// <summary>
    ///     Returns true if the browser is a popup.
    /// </summary>
    public bool IsPopup => cef_browser_t.is_popup(_self) != 0;

    /// <summary>
    ///     Returns true if a document has been loaded in the browser.
    /// </summary>
    public bool HasDocument => cef_browser_t.has_document(_self) != 0;

    /// <summary>
    ///     Returns the number of frames that currently exist.
    /// </summary>
    public int FrameCount => (int) cef_browser_t.get_frame_count(_self);

    /// <summary>
    ///     Returns the browser host object. This method can only be called in the
    ///     browser process.
    /// </summary>
    public CefBrowserHost GetHost()
    {
        return CefBrowserHost.FromNative(
            cef_browser_t.get_host(_self));
    }

    /// <summary>
    ///     Navigate backwards.
    /// </summary>
    public void GoBack()
    {
        cef_browser_t.go_back(_self);
    }

    /// <summary>
    ///     Navigate forwards.
    /// </summary>
    public void GoForward()
    {
        cef_browser_t.go_forward(_self);
    }

    /// <summary>
    ///     Reload the current page.
    /// </summary>
    public void Reload()
    {
        cef_browser_t.reload(_self);
    }

    /// <summary>
    ///     Reload the current page ignoring any cached data.
    /// </summary>
    public void ReloadIgnoreCache()
    {
        cef_browser_t.reload_ignore_cache(_self);
    }

    /// <summary>
    ///     Stop loading the page.
    /// </summary>
    public void StopLoad()
    {
        cef_browser_t.stop_load(_self);
    }

    /// <summary>
    ///     Returns true if this object is pointing to the same handle as |that|
    ///     object.
    /// </summary>
    public bool IsSame(CefBrowser? that)
    {
        if (that == null)
            return false;

        return cef_browser_t.is_same(_self, that.ToNative()) != 0;
    }

    /// <summary>
    ///     Returns the main (top-level) frame for the browser. In the browser process
    ///     this will return a valid object until after
    ///     CefLifeSpanHandler::OnBeforeClose is called. In the renderer process this
    ///     will return NULL if the main frame is hosted in a different renderer
    ///     process (e.g. for cross-origin sub-frames). The main frame object will
    ///     change during cross-origin navigation or re-navigation after renderer
    ///     process termination (due to crashes, etc).
    /// </summary>
    public CefFrame? GetMainFrame()
    {
        return CefFrame.FromNativeOrNull(cef_browser_t.get_main_frame(_self));
    }

    /// <summary>
    ///     Returns the focused frame for the browser window.
    /// </summary>
    public CefFrame? GetFocusedFrame()
    {
        return CefFrame.FromNativeOrNull(cef_browser_t.get_focused_frame(_self));
    }

    /// <summary>
    ///     Returns the frame with the specified identifier, or NULL if not found.
    /// </summary>
    public CefFrame? GetFrameByIdentifier(string identifier)
    {
        fixed (char* identifier_str = identifier)
        {
            cef_string_t m_identifier = new(identifier_str, identifier.Length);
            
            return CefFrame.FromNativeOrNull(cef_browser_t.get_frame_by_identifier(_self, &m_identifier));
        }
    }

    /// <summary>
    ///     Returns the frame with the specified name, or NULL if not found.
    /// </summary>
    public CefFrame? GetFrameByName(string name)
    {
        fixed (char* name_str = name)
        {
            var n_name = new cef_string_t(name_str, name.Length);

            return CefFrame.FromNativeOrNull(cef_browser_t.get_frame_by_name(_self, &n_name));
        }
    }

    /// <summary>
    ///     Returns the identifiers of all existing frames.
    /// </summary>
    public string[] GetFrameIdentifiers()
    {
        var frameCount = FrameCount;
        //var identifiers = new long[frameCount * 2];
        var n_count = (UIntPtr) frameCount;
        
        var identifiers = cef_string_list.From(new string[frameCount * 2]);
        cef_browser_t.get_frame_identifiers(_self, identifiers);
        
        if ((int) n_count < 0) throw new InvalidOperationException("Invalid number of frames.");

        string[] identifiersManaged = cef_string_list.ToArray(identifiers);

        return identifiersManaged;
    }

    /// <summary>
    ///     Returns the names of all existing frames.
    /// </summary>
    public string[] GetFrameNames()
    {
        var list = libcef.string_list_alloc();
        cef_browser_t.get_frame_names(_self, list);
        var result = cef_string_list.ToArray(list);
        libcef.string_list_free(list);
        return result;
    }
}
