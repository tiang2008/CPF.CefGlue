﻿using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPF.CefGlue
{
    public class CpfCefKeyboardHandler: CefKeyboardHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }
    }
}
