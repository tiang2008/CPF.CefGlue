//https://gitlab.com/xiliumhq/chromiumembedded/cefglue
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPF.CefGlue
{
    public class CpfCefApp : CefApp
    {
        public static CpfCefApp App { get;private set; }
        public CpfCefApp()
        {
            App = this;
        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            if (CefRuntime.Platform == CefRuntimePlatform.Linux)
            {
                commandLine.AppendSwitch("disable-gpu", "1");
                commandLine.AppendSwitch("no-zygote");
                commandLine.AppendSwitch("disable-gpu");
            }
            //if (string.IsNullOrEmpty(processType))
            //{
            //    commandLine.AppendSwitch("disable-gpu", "1");
            //    commandLine.AppendSwitch("disable-gpu-compositing", "1");
            //    //commandLine.AppendSwitch("enable-begin-frame-scheduling", "1");
            //    commandLine.AppendSwitch("disable-smooth-scrolling", "1");
            //    commandLine.AppendSwitch("disable-gpu-sandbox", "1");
            //    commandLine.AppendSwitch("disable-gpu-vsync", "1");
            //    //commandLine.AppendSwitch("disable-site-isolation-trials");
            //    //commandLine.AppendSwitch("disable-direct-write", "1");
            //    commandLine.AppendSwitch("disable-software-rasterizer", "1");
            //    //commandLine.AppendSwitch("disable-features=VizDisplayCompositor");

            //commandLine.AppendSwitch("force-device-scale-factor", "1");

            //    commandLine.AppendSwitch("default-encoding", "utf-8");
            //    commandLine.AppendSwitch("allow-file-access-from-files");
            //    commandLine.AppendSwitch("allow-universal-access-from-files");
            //    commandLine.AppendSwitch("disable-web-security");
            //    commandLine.AppendSwitch("ignore-certificate-errors");
            //}


            commandLine.AppendSwitch("enable-devtools-experiments");
            commandLine.AppendSwitch("ignore-certificate-errors");
            commandLine.AppendSwitch("enable-begin-frame-scheduling");
            commandLine.AppendSwitch("enable-media-stream");
            commandLine.AppendSwitch("enable-blink-features", "CSSPseudoHas");
        }
        public CpfCefRenderProcessHandler RenderProcessHandler { get; set; }
        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            if (RenderProcessHandler == null)
            {
                RenderProcessHandler = new CpfCefRenderProcessHandler();
            }
            return RenderProcessHandler;
        }
        public CpfCefBrowserProcessHandler BrowserProcessHandler { get; set; }
        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            if (BrowserProcessHandler == null)
            {
                BrowserProcessHandler = new CpfCefBrowserProcessHandler();
            }
            return BrowserProcessHandler;
        }
        public CefResourceBundleHandler ResourceBundleHandler { get; set; }
        protected override CefResourceBundleHandler GetResourceBundleHandler()
        {
            return ResourceBundleHandler;
        }
        public Action<CefSchemeRegistrar> RegisterCustomSchemes { get; set; }
        protected override void OnRegisterCustomSchemes(CefSchemeRegistrar registrar)
        {
            //registrar.AddCustomScheme("res", CefSchemeOptions.Standard);
            RegisterCustomSchemes?.Invoke(registrar);
        }
    }
}
