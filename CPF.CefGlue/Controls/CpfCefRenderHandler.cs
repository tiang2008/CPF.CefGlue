using Microsoft.Win32.SafeHandles;
using System;
using CPF.Controls;

namespace CPF.CefGlue
{
    public class CpfCefRenderHandler : CefRenderHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }
        //private readonly ILogger _logger = new ILogger();
        //private readonly IUiHelper _uiHelper;

        //public CpfCefRenderHandler(WebBrowser owner)
        //{
        //    if (owner == null)
        //    {
        //        throw new ArgumentNullException("owner");
        //    }


        //    WebBrowser = owner;
        //    //_uiHelper = uiHelper;
        //}

        protected override bool GetRootScreenRect(CefBrowser browser, ref CefRectangle rect)
        {
            return WebBrowser.GetViewRect(ref rect);
        }

#if Net4
        protected override bool GetViewRect(CefBrowser browser, ref CefRectangle rect)
        {
            return WebBrowser.GetViewRect(ref rect);
        }
#else

        protected override CefAccessibilityHandler GetAccessibilityHandler()
        {
            return null;
        }

        protected override void GetViewRect(CefBrowser browser, out CefRectangle rect)
        {
            rect = new CefRectangle();
            WebBrowser.GetViewRect(ref rect);
        }

        protected override void OnAcceleratedPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr sharedHandle)
        {

        }

        protected override void OnImeCompositionRangeChanged(CefBrowser browser, CefRange selectedRange, CefRectangle[] characterBounds)
        {
            if (characterBounds.Length > 0)
            {
                var rect = characterBounds[0];
                WebBrowser.Invoke(() =>
                {
                    //Console.WriteLine(rect.X + "," + rect.Y);
                    WebBrowser.Root.ViewImpl.SetIMEPosition(WebBrowser.PointToView(new Drawing.Point(rect.X, rect.Y)));
                });
            }
        }

        protected override void OnTextSelectionChanged(CefBrowser browser, string selectedText, CefRange selectedRange)
        {
            base.OnTextSelectionChanged(browser, selectedText, selectedRange);
        }
#endif
        protected override bool GetScreenPoint(CefBrowser browser, int viewX, int viewY, ref int screenX, ref int screenY)
        {
            WebBrowser.GetScreenPoint(viewX, viewY, ref screenX, ref screenY);
            return true;
        }

        protected override bool GetScreenInfo(CefBrowser browser, CefScreenInfo screenInfo)
        {
            return false;
        }

        protected override void OnPopupShow(CefBrowser browser, bool show)
        {
            WebBrowser.OnPopupShow(show);
        }

        protected override void OnPopupSize(CefBrowser browser, CefRectangle rect)
        {
            WebBrowser.OnPopupSize(rect);
        }

        protected override void OnPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {
            //_logger.Debug("Type: {0} Buffer: {1:X8} Width: {2} Height: {3}", type, buffer, width, height);
            //foreach (var rect in dirtyRects)
            //{
            //    _logger.Debug("   DirtyRect: X={0} Y={1} W={2} H={3}", rect.X, rect.Y, rect.Width, rect.Height);
            //}

            if (type == CefPaintElementType.View)
            {
                WebBrowser.HandleViewPaint(browser, type, dirtyRects, buffer, width, height);
            }
            else if (type == CefPaintElementType.Popup)
            {
                WebBrowser.HandlePopupPaint(width, height, dirtyRects, buffer);
            }
        }

        protected override void OnScrollOffsetChanged(CefBrowser browser, double x, double y)
        {
        }

    }
}
