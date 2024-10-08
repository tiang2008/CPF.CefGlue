using System;
using CPF.Controls;

namespace CPF.CefGlue
{
    public class CpfCefLifeSpanHandler : CefLifeSpanHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            WebBrowser.HandleAfterCreated(browser);
        }
        protected override bool DoClose(CefBrowser browser)
        {
            //Console.WriteLine("关闭DoClose" + browser.Identifier);
            return false;
        }
        protected override void OnBeforeClose(CefBrowser browser)
        {
            //Console.WriteLine("关闭OnBeforeClose" + browser.Identifier);
        }
    }
}
