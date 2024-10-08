using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPF.CefGlue
{
    public abstract class CpfCefDragHandler : CefDragHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }
    }
}
