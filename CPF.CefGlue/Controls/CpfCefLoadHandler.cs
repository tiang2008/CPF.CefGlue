using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPF.Controls;

namespace CPF.CefGlue
{
    public class CpfCefLoadHandler : CefLoadHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }

        //public CpfCefLoadHandler(WebBrowser owner)
        //{
        //    this.WebBrowser = owner;
        //}

        protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        {
            this.WebBrowser.OnLoadingStateChange(isLoading, canGoBack, canGoForward);
        }

        protected override void OnLoadError(CefBrowser browser, CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
        {
            this.WebBrowser.OnLoadError(frame, errorCode, errorText, failedUrl);
        }

#if Net4
        protected override void OnLoadStart(CefBrowser browser, CefFrame frame)
        {
            this.WebBrowser.OnLoadStart(frame);
        }
#else
        protected override void OnLoadStart(CefBrowser browser, CefFrame frame, CefTransitionType transitionType)
        {
            this.WebBrowser.OnLoadStart(frame);
        }
#endif

        protected override void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
        {
            this.WebBrowser.OnLoadEnd(frame, httpStatusCode);
        }

    }
}
