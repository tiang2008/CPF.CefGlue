using CPF.CefGlue;
using CPF.Platform;
using System;
using System.Net;

namespace TransLinkApp.Core
{
    public class MyWebBrowser : WebBrowser
    {
        public MyWebBrowser()
        {
#if DEBUG
            this.ShowConsoleMessage = true;
#else
                this.ShowConsoleMessage=false;
#endif
        }


        public string ID
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 创建浏览器内核的时候，用来设置浏览器内核的参数以及设置处理方法
        /// </summary>
        /// <param name="settings"></param>
        protected override CpfCefClient OnCreateWebBrowser(CefBrowserSettings settings)
        {
            string appDataPath = Application.StartupPath;
            var contextSettings = new CefRequestContextSettings
            {
                CachePath = appDataPath + "\\LocalStorage\\" + ID+"\\",  // 缓存路径，保证会话隔离
                PersistSessionCookies = true,         // 持久化 Session Cookies
                AcceptLanguageList = "zh-CN,en-US",
                CookieableSchemesExcludeDefaults = false,
                CookieableSchemesList = "http,https,ws,wss",
            };

            // 创建独立的 RequestContext
            var requestContext = CefRequestContext.CreateContext(contextSettings, null);
            //requestContext.LoadExtension("", cefManifest, null);
            //string error = string.Empty;
            //var cefvalue = CefValue.Create();
            //cefvalue.SetString("zh-CN");
            //requestContext.SetPreference("intl.accept_languages", cefvalue, out error);

            CefCookie cookie = new CefCookie
            {
                Name = $"cookie{ID}",
                Value = $"cookievalue{ID}",
                Path = "/" + ID + "/",
                Expires = new CefBaseTime(DateTime.Now.AddDays(1).Ticks),
            };
            var cookieManager = requestContext.GetCookieManager(null);
            //这样设置的cookie不是全局的，只有当前browser才能访问
            cookieManager.SetCookie("dxl.cn", cookie,null);
            cookieManager.FlushStore(null);

            cookie.Expires = CefBaseTime.Now();

            var client = new CpfCefClient(this)
            {
                RenderHandler = new CpfCefRenderHandler(),
                DisplayHandler = new CpfCefDisplayHandler(),
                JSDialogHandler = new CpfCefJSDialogHandler(),
                LifeSpanHandler = new CpfCefLifeSpanHandler(),
                ContextMenuHandler = new CpfCefContextMenuHandler(),
                RequestContext = requestContext,
                LoadHandler = new CpfCefLoadHandler()
            };

            return client;
        }
    }

}
