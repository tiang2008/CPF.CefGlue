using System;
using CPF.Controls;
using CPF;
using CPF.Input;
using CPF.Drawing;
using CPF.CefGlue;
using CPF.Threading;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Collections.Generic;
using CPF.Reflection;
using System.Reflection;
using CPF.CefGlue.JSExtenstions;

namespace CPF.CefGlue
{
    /// <summary>
    /// 绑定JS的方法需要在方法上加[JSFunction] 方法所在的对象是设置给 WebBrowser的CommandContext 属性
    /// </summary>
    public class WebBrowser : UIElement, IEditor
    {
        private static readonly Keys[] HandledKeys =
        {
            Keys.Tab, Keys.Home, Keys.End, Keys.Left, Keys.Right, Keys.Up, Keys.Down
        };
        internal static ConcurrentDictionary<int, WebBrowser> browsers = new ConcurrentDictionary<int, WebBrowser>();

        int Identifier;
        //private bool _disposed;
        private bool _created;

        //private Image _browserPageImage;
        private Bitmap _browserPageBitmap;

        private int _browserWidth;
        private int _browserHeight;
        private bool _browserSizeChanged;

        private CefBrowser _browser;
        private CefBrowserHost _browserHost;
        private CpfCefClient _cefClient;

        private Popup _popup;
        //private Image _popupImage;
        private Bitmap _popupImageBitmap;

        private Popup _tooltip;
        private DispatcherTimer _tooltipTimer;

        //Dispatcher _mainUiDispatcher;

        private readonly ILogger _logger = new ILogger();
        ///// <summary>
        ///// 用来JS绑定C#方法的对象
        ///// </summary>
        //[Description("用来JS绑定C#方法的对象")]
        //public object GlobalObject
        //{
        //    get { return GetValue(); }
        //    set { SetValue(value); }
        //}

        public WebBrowser()
        {
            if (!CefRuntimeLoader.IsLoaded)
            {
                CefRuntimeLoader.Load();
            }

            _popup = CreatePopup();

            _tooltip = new Popup();
            //_tooltip.StaysOpen = true;
            _tooltip.Visibility = Visibility.Collapsed;
            _tooltip.Placement = PlacementMode.Mouse;
            _tooltip.PlacementTarget = this;
            _tooltip.CanActivate = false;
            _tooltip.MarginLeft = 15;
            _tooltip.MarginTop = 15;
            _tooltip.Background = null;
            _tooltip.Children.Add(new ContentControl
            {
                PresenterFor = _tooltip,
                Name = "content",
                Margin = new ThicknessField(1),
                BorderFill = "#aaa",
                BorderStroke = "1",
                Background = "#fff",
                UseLayoutRounding = true,
            });
            //_tooltip.Closed += TooltipOnClosed;

            _tooltipTimer = new DispatcherTimer();
            _tooltipTimer.Interval = TimeSpan.FromSeconds(0.5);

            //KeyboardNavigation.SetAcceptsReturn(this, true);

            //_mainUiDispatcher = Dispatcher.MainThread;

            if (CefRuntime.Platform == CefRuntimePlatform.MacOS && !CefRuntimeLoader.IsLoaded)
            {
                CefRuntimeLoader.Load(new CPFBrowserProcessHandler());
            }

        }

        private readonly NativeObjectRegistry _objectRegistry = new NativeObjectRegistry();
        private NativeObjectMethodDispatcher _objectMethodDispatcher;

        /// <summary>
        /// 注册对象到JS里
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="name"></param>
        /// <param name="methodHandler"></param>
        public void RegisterJavascriptObject(object targetObject, string name, MethodCallHandler methodHandler = null)
        {
            _objectRegistry.Register(targetObject, name, methodHandler);
        }
        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="name"></param>
        public void UnregisterJavascriptObject(string name)
        {
            _objectRegistry.Unregister(name);
        }
        /// <summary>
        /// 是否已经注册
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsJavascriptObjectRegistered(string name)
        {
            return _objectRegistry.Get(name) != null;
        }

        #region Disposable

        protected override void Dispose(bool disposing)
        {
            if (_tooltipTimer != null)
            {
                _tooltipTimer.Dispose();
                _tooltipTimer = null;
            }

            if (_popup != null)
            {
                _popup.Dispose();
                _popup = null;
            }

            //if (_browserPageImage != null)
            //{
            //    _browserPageImage.Dispose();
            //    _browserPageImage = null;
            //}
            if (_popupImageBitmap != null)
            {
                _popupImageBitmap.Dispose();
                _popupImageBitmap = null;
            }
            if (_browserPageBitmap != null)
            {
                _browserPageBitmap.Dispose();
                _browserPageBitmap = null;
            }

            // 					if (this.browserPageD3dImage != null)
            // 						this.browserPageD3dImage = null;

            // TODO: What's the right way of disposing the browser instance?
            //new Thread(() =>
            //{
            if (_browserHost != null)
            {
                _browserHost.CloseBrowser();
                //_browserHost.Dispose();
                _browserHost = null;
            }
            if (_browser != null)
            {
                browsers.TryRemove(Identifier, out _);
                _browser.Dispose();
                _browser = null;
            }
            //})
            //{ Name = "关闭浏览器", IsBackground = true }.Start();

            base.Dispose(disposing);
        }
        #endregion

        //public event LoadStartEventHandler LoadStart;
        public event EventHandler<LoadStartEventArgs> LoadStart
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event EventHandler<LoadEndEventArgs> LoadEnd
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event EventHandler<LoadingStateChangeEventArgs> LoadingStateChange
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event EventHandler<LoadErrorEventArgs> LoadError
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        public event EventHandler Created
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }
        /// <summary>
        /// 创建浏览器内核的时候，用来设置浏览器内核的参数以及设置处理方法
        /// </summary>
        /// <param name="settings"></param>
        protected virtual CpfCefClient OnCreateWebBrowser(CefBrowserSettings settings)
        {
            var client = new CpfCefClient(this);
            client.RenderHandler = new CpfCefRenderHandler();
            client.DisplayHandler = new CpfCefDisplayHandler();
            client.JSDialogHandler = new CpfCefJSDialogHandler();
            client.LifeSpanHandler = new CpfCefLifeSpanHandler();
            client.ContextMenuHandler = new CpfCefContextMenuHandler();
            client.LoadHandler = new CpfCefLoadHandler();
            return client;
        }

        [PropertyChanged(nameof(Url))]
        void OnUrlChanged(object newValue, object oldValue, CPF.PropertyMetadataAttribute attribute)
        {
            if (!isNav)
            {
                if (newValue == null)
                {
                    newValue = "";
                }
                var url = newValue.ToString().TrimStart();

                if (_browser != null)
                    _browser.GetMainFrame().LoadUrl(url);
            }
        }
        bool isNav = false;

        public event EventHandler<AddressChangeEventArgs> AddressChange
        {
            add { AddHandler(value); }
            remove { RemoveHandler(value); }
        }

        internal virtual void OnAddressChange(CefBrowser browser, AddressChangeEventArgs args)
        {
            if (_browserHost != null && Root != null)
            {
                Invoke(() =>
                {
                    if (Root.RenderScaling != 1)
                    {
                        //_browserHost.SetZoomLevel(Root.RenderScaling);
                        _browserHost.SetZoomLevel(Math.Log(Root.RenderScaling, 1.2));
                    }
                    if (this._browser.Identifier == browser.Identifier)
                    {
                        isNav = true;
                        Url = args.Url;
                        isNav = false;
                    }
                });
            }
            RaiseEvent(args, nameof(AddressChange));
        }

        //public event EventHandler<TitleChangeEventArgs> TitleChange
        //{
        //    add { AddHandler(value); }
        //    remove { RemoveHandler(value); }
        //}

        internal virtual void OnTitleChange(CefBrowser browser, string args)
        {
            if (_browser != null && this._browser.Identifier == browser.Identifier && !IsDisposed)
            {
                Title = args;
            }
            //RaiseEvent(args, nameof(TitleChange));
        }

        [Browsable(false), NotCpfProperty]
        public CefBrowser Browser
        {
            get { return _browser; }
        }

        [Browsable(false), NotCpfProperty]
        public CefClient CefClient
        {
            get { return _cefClient; }
        }

        [Browsable(false), NotCpfProperty]
        public CefBrowserHost BrowserHost
        {
            get { return _browserHost; }
        }

        internal void OnLoadStart(CefFrame frame)
        {
            Invoke(() =>
            {
                var g = CommandContext;
                if (g != null)
                {
                    var ms = g.GetType().GetMethods();
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in ms)
                    {
                        var cas = item.GetCustomAttributes(typeof(JSFunction), true);
                        if (cas != null && cas.Length == 1)
                        {
                            sb.Append($"function {item.Name}(){{return $csharpCaller.$CallCSharp('{item.Name}',arguments); }}");
                        }
                    }
                    frame.ExecuteJavaScript(sb.ToString(), "", 0);
                }
            });
            //frame.ExecuteJavaScript("window.Test123=123", "", 0);
            RaiseEvent(new LoadStartEventArgs(frame), nameof(LoadStart));
        }

        internal void OnLoadEnd(CefFrame frame, int httpStatusCode)
        {
            RaiseEvent(new LoadEndEventArgs(frame, httpStatusCode), nameof(LoadEnd));
        }
        internal void OnLoadingStateChange(bool isLoading, bool canGoBack, bool canGoForward)
        {
            RaiseEvent(new LoadingStateChangeEventArgs(isLoading, canGoBack, canGoForward), nameof(LoadingStateChange));
        }
        internal void OnLoadError(CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
        {
            RaiseEvent(new LoadErrorEventArgs(frame, errorCode, errorText, failedUrl), nameof(LoadError));
        }

        internal void OnFocusedNodeChanged(int x, int y)
        {
            Invoke(() =>
            {
                Root.ViewImpl.SetIMEPosition(PointToView(new Point(x, y)));
            });
        }

        /// <summary>
        /// 读取或者设置当前URL
        /// </summary>
        [PropertyMetadata("about:blank")]
        [Description("读取或者设置当前URL")]
        public string Url
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 网页标题
        /// </summary>
        [PropertyMetadata("")]
        [Description("网页标题")]
        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 控制台输出JS控制台消息
        /// </summary>
        [PropertyMetadata(true)]
        [Description("控制台输出JS控制台消息")]
        public bool ShowConsoleMessage
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        /// <summary>
        /// 是否启用输入法，主要描述的是中文这类输入法
        /// </summary>
        [NotCpfProperty]
        public bool IsInputMethodEnabled { get; set; } = true;
        [NotCpfProperty]
        bool IEditor.IsReadOnly => false;

        ///// <summary>
        ///// 是否允许背景透明
        ///// </summary>
        //[PropertyMetadata(false), Description("是否允许背景透明")]
        //private bool AllowsTransparency
        //{
        //    get { return GetValue<bool>(); }
        //    set { SetValue(value); }
        //}

        /// <summary>
        /// 在主框架中执行JS，只能返回这些类型 null，string，int，double，DateTime，bool
        /// </summary>
        /// <param name="code"></param>
        public Task<object> ExecuteJavaScript(string code)
        {
            if (_browser != null)
                return this._browser.GetMainFrame().ExecuteJavaScript(code);
            return null;
        }
        /// <summary>
        /// 打开开发者工具
        /// </summary>
        public void ShowDev()
        {
            if (_browser == null)
            {
                throw new Exception("需要等浏览器初始化完成");
            }
            //创建 CefWindowInfo 对象
            var wi = CefWindowInfo.Create();
            //开发者工具以独立窗口弹出 窗口名 DevTools
            wi.SetAsPopup(IntPtr.Zero, "DevTools");
            wi.SetAsWindowless(IntPtr.Zero, false);
            _browserHost.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings { }, new CefPoint());
        }

        //        void SendProcessMessage(CefProcessId targetProcess, CefProcessMessage message)
        //        {
        ////#if Net4
        //            _browser.SendProcessMessage(targetProcess,message);
        ////#else
        ////            _browser.GetMainFrame().SendProcessMessage(targetProcess, message);
        ////#endif
        //        }

        private class DevToolsWebClient : CefClient
        { }
        /// <summary>
        /// 关闭开发者工具
        /// </summary>
        public void CloseDev()
        {
            if (_browserHost == null)
            {
                return;
            }
            _browserHost.CloseDevTools();
        }

        protected override Size ArrangeOverride(in Size arrangeBounds)
        {
            var size = base.ArrangeOverride(arrangeBounds);

            if (!DesignMode)
            {
                var newWidth = (int)(size.Width * Root.RenderScaling);
                var newHeight = (int)(size.Height * Root.RenderScaling);

                //_logger.Debug("BrowserResize. Old H{0}xW{1}; New H{2}xW{3}.", _browserHeight, _browserWidth, newHeight, newWidth);

                if (newWidth > 0 && newHeight > 0)
                {
                    if (!_created)
                    {
                        AttachEventHandlers(this); // TODO: ?

                        // Create the bitmap that holds the rendered website bitmap
                        _browserWidth = newWidth;
                        _browserHeight = newHeight;
                        _browserSizeChanged = true;

                        var windowInfo = CefWindowInfo.Create();
                        windowInfo.SetAsWindowless(IntPtr.Zero, false);

                        //windowInfo.SetAsWindowless((IntPtr)Root.ViewImpl.GetPropretyValue("Handle"), true);

                        var settings = new CefBrowserSettings();
                        _cefClient = OnCreateWebBrowser(settings);
                        _cefClient.RenderHandler?.SetWebBrowser(this);
                        _cefClient.DisplayHandler?.SetWebBrowser(this);
                        _cefClient.LifeSpanHandler?.SetWebBrowser(this);
                        _cefClient.LoadHandler?.SetWebBrowser(this);
                        _cefClient.JSDialogHandler?.SetWebBrowser(this);
                        _cefClient.ContextMenuHandler?.SetWebBrowser(this);
                        _cefClient.DialogHandler?.SetWebBrowser(this);
                        _cefClient.DownloadHandler?.SetWebBrowser(this);
                        _cefClient.DragHandler?.SetWebBrowser(this);
                        _cefClient.FindHandler?.SetWebBrowser(this);
                        _cefClient.FocusHandler?.SetWebBrowser(this);
                        _cefClient.KeyboardHandler?.SetWebBrowser(this);
                        _cefClient.RequestHandler?.SetWebBrowser(this);

                        //CefDictionaryValue dictionaryValue = CefDictionaryValue.Create();
                        //OnRenderProcessThreadCreated(dictionaryValue);
                        // This is the first time the window is being rendered, so create it.
                        CefBrowserHost.CreateBrowser(windowInfo, _cefClient, settings, !string.IsNullOrEmpty(Url) ? Url : "about:blank", null, _cefClient.RequestContext);

                        _created = true;
                        //}
                    }
                    else
                    {
                        // Only update the bitmap if the size has changed
                        if (_browserPageBitmap == null || (_browserPageBitmap.Width != newWidth || _browserPageBitmap.Height != newHeight))
                        {
                            _browserWidth = newWidth;
                            _browserHeight = newHeight;
                            _browserSizeChanged = true;

                            // If the window has already been created, just resize it
                            if (_browserHost != null)
                            {
                                //_logger.Trace("CefBrowserHost::WasResized to {0}x{1}.", newWidth, newHeight);
                                if (Root.RenderScaling != 1)
                                {
                                    //_browserHost.SetZoomLevel(Root.RenderScaling);
                                    _browserHost.SetZoomLevel(Math.Log(Root.RenderScaling, 1.2));
                                }
                                _browserHost.WasResized();
                            }
                        }
                    }
                }
            }

            return size;
        }



        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (_browserPageBitmap != null)
            {
                dc.DrawImage(_browserPageBitmap, new Rect(0, 0, ActualSize.Width, ActualSize.Height), new Rect(0, 0, _browserPageBitmap.Width, _browserPageBitmap.Height));
            }
        }

        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            base.OnGotFocus(e);
            if (e.Handled)
            {
                return;
            }
            try
            {
                if (_browserHost != null)
                {
                    if (IsInputMethodEnabled)
                    {
                        Root.ViewImpl.SetIMEEnable(true);
                    }
                    _browserHost.SetFocus(true);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in GotFocus()", ex);
            }
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (e.Handled)
            {
                return;
            }
            try
            {
                if (_browserHost != null)
                {
                    _browserHost.ImeFinishComposingText(false);
                    _browserHost.ImeCancelComposition();
                    _browserHost.SetFocus(false);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in LostFocus()", ex);
            }
        }
        protected override void OnMouseLeave(MouseEventArgs arg)
        {
            base.OnMouseLeave(arg);
            try
            {
                if (_browserHost != null)
                {
                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = 0,
                        Y = 0
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);

                    _browserHost.SendMouseMoveEvent(mouseEvent, true);
                    //_logger.Debug("Browser_MouseLeave");
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseLeave()", ex);
            }
        }
        CefEventFlags eventFlags;
        Point mousePosition;
        protected override void OnMouseMove(MouseEventArgs arg)
        {
            base.OnMouseMove(arg);
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)(cursorPos.X * Root.RenderScaling),
                        Y = (int)(cursorPos.Y * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);
                    eventFlags = mouseEvent.Modifiers;
                    mousePosition = cursorPos;

                    _browserHost.SendMouseMoveEvent(mouseEvent, false);

                    //_logger.Debug(string.Format("Browser_MouseMove: ({0},{1})", mouseEvent.X, mouseEvent.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseMove()", ex);
            }
        }
        protected override void OnMouseDown(MouseButtonEventArgs arg)
        {
            base.OnMouseDown(arg);
            try
            {
                if (_browserHost != null)
                {
                    if (!IsKeyboardFocusWithin)
                    {
                        Focus();
                    }
                    CaptureMouse();

                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)(cursorPos.X * Root.RenderScaling),
                        Y = (int)(cursorPos.Y * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);

                    if (arg.MouseButton == MouseButton.Left)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, false, 1);
                    else if (arg.MouseButton == MouseButton.Middle)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, false, 1);
                    else if (arg.MouseButton == MouseButton.Right)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, false, 1);

                    //_logger.Debug(string.Format("Browser_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseDown()", ex);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs arg)
        {
            base.OnMouseUp(arg);
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)(cursorPos.X * Root.RenderScaling),
                        Y = (int)(cursorPos.Y * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);

                    if (arg.MouseButton == MouseButton.Left)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 1);
                    else if (arg.MouseButton == MouseButton.Middle)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Middle, true, 1);
                    else if (arg.MouseButton == MouseButton.Right)
                        _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Right, true, 1);

                    //_logger.Debug(string.Format("Browser_MouseUp: ({0},{1})", cursorPos.X, cursorPos.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseUp()", ex);
            }
            ReleaseMouseCapture();
        }

        protected override void OnDoubleClick(RoutedEventArgs e)
        {
            base.OnDoubleClick(e);
            try
            {
                if (_browserHost != null && mousePosition.X >= 0 && mousePosition.Y >= 0 && mousePosition.X < ActualSize.Width && mousePosition.Y < ActualSize.Height)
                {
                    var cursorPos = mousePosition;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)(cursorPos.X * Root.RenderScaling),
                        Y = (int)(cursorPos.Y * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = eventFlags;

                    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, false, 2);
                    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 2);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseDown()", ex);
            }
        }
        protected override void OnMouseWheel(MouseWheelEventArgs arg)
        {
            base.OnMouseWheel(arg);
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)(cursorPos.X * Root.RenderScaling),
                        Y = (int)(cursorPos.Y * Root.RenderScaling)
                    };

                    _browserHost.SendMouseWheelEvent(mouseEvent, (int)arg.Delta.X, (int)arg.Delta.Y);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in MouseWheel()", ex);
            }
        }
        protected override void OnTextInput(TextInputEventArgs arg)
        {
            base.OnTextInput(arg);
            if (arg.Handled)
            {
                return;
            }
            //Console.WriteLine("cef输入法输出：" + arg.Text);
            if (_browserHost != null)
            {
                //_logger.Debug("TextInput: text {0}", arg.Text);
                foreach (var c in arg.Text)
                {
                    CefKeyEvent keyEvent = new CefKeyEvent()
                    {
                        EventType = CefKeyEventType.Char,
                        WindowsKeyCode = (int)c,
                        Character = c,
                        UnmodifiedCharacter = c
                    };

                    keyEvent.Modifiers = GetKeyboardModifiers();

                    _browserHost.SendKeyEvent(keyEvent);
                }
                _browserHost.ImeCommitText(arg.Text, new CefRange(int.MaxValue, 0), 0);

                //用来触发光标定位回调
                _browserHost.ImeSetComposition(" ", 1, new CefCompositionUnderline { }, new CefRange(int.MaxValue, 0), new CefRange(0, 1));
                _browserHost.ImeSetComposition("", 0, new CefCompositionUnderline { }, new CefRange(int.MaxValue, 0), new CefRange(0, 0));
#if !Net4
                //_browserHost.ImeSetComposition
                //var p = this.PointToView(point);
                //Root?.ViewImpl.SetIMEPosition(p);
#endif
            }

            arg.Handled = true;
        }

        protected override void OnOverrideMetadata(OverrideMetadata overridePropertys)
        {
            base.OnOverrideMetadata(overridePropertys);
            overridePropertys.Override(nameof(Focusable), new PropertyMetadataAttribute(true));
        }

        protected override void OnKeyDown(KeyEventArgs arg)
        {
            //Console.WriteLine("keyDown:" + arg.Key);
            base.OnKeyDown(arg);
            if (arg.Handled)
            {
                return;
            }
            try
            {
                if (_browserHost != null)
                {
                    char Characters = default;
                    char CharactersIgnoringModifiers = default;
                    if (CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.OSX)
                    {
                        var e = CPF.Platform.Application.GetRuntimePlatform().GetPropretyValue("CurrentKeyEvent");
                        string cs;
                        if (e != null && !string.IsNullOrEmpty(cs = e.GetPropretyValue("Characters") as string))
                        {
                            Characters = cs[0];
                        }
                        string cim;
                        if (e != null && !string.IsNullOrEmpty(cim = e.GetPropretyValue("CharactersIgnoringModifiers") as string))
                        {
                            CharactersIgnoringModifiers = cim[0];
                        }
                    }
                    CefKeyEvent keyEvent = new CefKeyEvent()
                    {
                        EventType = CefKeyEventType.RawKeyDown,
                        //WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key == Keys.System ? arg.SystemKey : arg.Key),
                        // WindowsKeyCode = arg.KeyCode, // KeyInterop.VirtualKeyFromKey(arg.Key),
                        NativeKeyCode = CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.Linux ? KeyInterop.VirtualKeyFromKey(arg.Key) : arg.KeyCode,// KeyInterop.VirtualKeyFromKey(arg.Key),
                        IsSystemKey = arg.Key == Keys.System,
                        Character = Characters,
                        UnmodifiedCharacter = CharactersIgnoringModifiers
                    };
                    if (CPF.Platform.Application.OperatingSystem != CPF.Platform.OperatingSystemType.OSX)
                    {
                        keyEvent.WindowsKeyCode = CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.Linux ? KeyInterop.VirtualKeyFromKey(arg.Key) : arg.KeyCode;
                    }

                    keyEvent.Modifiers = GetKeyboardModifiers();
                    //Console.WriteLine(keyEvent.Modifiers);

                    _browserHost.SendKeyEvent(keyEvent);

                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in KeyDown()", ex);
            }
            arg.Handled = HandledKeys.Contains(arg.Key);
        }

        protected override void OnKeyUp(KeyEventArgs arg)
        {
            //Console.WriteLine("keyUp:" + arg.Key);
            base.OnKeyUp(arg);
            if (arg.Handled)
            {
                return;
            }
            try
            {
                if (_browserHost != null)
                {
                    char Characters = (char)0;
                    char CharactersIgnoringModifiers = (char)0;
                    if (CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.OSX)
                    {
                        var e = CPF.Platform.Application.GetRuntimePlatform().GetPropretyValue("CurrentKeyEvent");
                        string cs;
                        if (e != null && !string.IsNullOrEmpty(cs = e.GetPropretyValue("Characters") as string))
                        {
                            Characters = cs[0];
                        }
                        string cim;
                        if (e != null && !string.IsNullOrEmpty(cim = e.GetPropretyValue("CharactersIgnoringModifiers") as string))
                        {
                            CharactersIgnoringModifiers = cim[0];
                        }
                    }
                    CefKeyEvent keyEvent = new CefKeyEvent()
                    {
                        EventType = CefKeyEventType.KeyUp,
                        //WindowsKeyCode = KeyInterop.VirtualKeyFromKey(arg.Key == Keys.System ? arg.SystemKey : arg.Key),
                        //WindowsKeyCode = arg.KeyCode, // KeyInterop.VirtualKeyFromKey(arg.Key),
                        NativeKeyCode = CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.Linux ? KeyInterop.VirtualKeyFromKey(arg.Key) : arg.KeyCode, //KeyInterop.VirtualKeyFromKey(arg.Key),
                        IsSystemKey = arg.Key == Keys.System,
                        Character = Characters,
                        UnmodifiedCharacter = CharactersIgnoringModifiers
                    };
                    if (CPF.Platform.Application.OperatingSystem != CPF.Platform.OperatingSystemType.OSX)
                    {
                        keyEvent.WindowsKeyCode = CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.Linux ? KeyInterop.VirtualKeyFromKey(arg.Key) : arg.KeyCode;
                    }
                    keyEvent.Modifiers = GetKeyboardModifiers();


                    var key = arg.Key;
                    if ((key >= Keys.A && key <= Keys.Z))
                    {
                        var hotkey = CPF.Platform.Application.GetRuntimePlatform().Hotkey(new KeyGesture(arg.Key, arg.Modifiers));
                        if (hotkey == PlatformHotkey.None)
                        {
                            //用来触发光标定位回调
                            _browserHost.ImeSetComposition(" ", 1, new CefCompositionUnderline { }, new CefRange(int.MaxValue, 0), new CefRange(0, 1));
                            _browserHost.ImeSetComposition("", 0, new CefCompositionUnderline { }, new CefRange(int.MaxValue, 0), new CefRange(0, 0));
                        }
                    }
                    else
                    {
                        //_browserHost.ImeCommitText("", new CefRange(int.MaxValue, 0), 0);
                        _browserHost.ImeFinishComposingText(false);
                        //_browserHost.ImeCancelComposition();
                    }

                    _browserHost.SendKeyEvent(keyEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in KeyUp()", ex);
            }

            arg.Handled = true;
        }
        private void AttachEventHandlers(WebBrowser browser)
        {
            browser._popup.MouseMove += _popup_MouseMove;
            browser._popup.MouseDown += _popup_MouseDown;
            browser._popup.MouseWheel += _popup_MouseWheel;
        }

        private void _popup_MouseWheel(object sender, MouseWheelEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;
                    var delta = arg.Delta;
                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)((cursorPos.X + _popup.MarginLeft.Value) * Root.RenderScaling),
                        Y = (int)((cursorPos.Y + _popup.MarginTop.Value) * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);
                    _browserHost.SendMouseWheelEvent(mouseEvent, (int)delta.X, (int)delta.Y);

                    //_logger.Debug(string.Format("MouseWheel: ({0},{1})", cursorPos.X, cursorPos.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in Popup.MouseWheel()", ex);
            }
        }

        private void _popup_MouseDown(object sender, MouseButtonEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)((cursorPos.X + _popup.MarginLeft.Value) * Root.RenderScaling),
                        Y = (int)((cursorPos.Y + _popup.MarginTop.Value) * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);

                    _browserHost.SendMouseClickEvent(mouseEvent, CefMouseButtonType.Left, true, 1);

                    //_logger.Debug(string.Format("Popup_MouseDown: ({0},{1})", cursorPos.X, cursorPos.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in Popup.MouseDown()", ex);
            }
        }

        private void _popup_MouseMove(object sender, MouseEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    Point cursorPos = arg.Location;

                    CefMouseEvent mouseEvent = new CefMouseEvent()
                    {
                        X = (int)((cursorPos.X + _popup.MarginLeft.Value) * Root.RenderScaling),
                        Y = (int)((cursorPos.Y + _popup.MarginTop.Value) * Root.RenderScaling)
                    };

                    mouseEvent.Modifiers = GetMouseModifiers(arg);

                    _browserHost.SendMouseMoveEvent(mouseEvent, false);

                    //_logger.Debug(string.Format("Popup_MouseMove: ({0},{1})", cursorPos.X, cursorPos.Y));
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in Popup.MouseMove()", ex);
            }
        }

        protected virtual void OnCreated(EventArgs args)
        {
            RaiseEvent(args, nameof(Created));
        }

        #region Handlers

        internal void HandleAfterCreated(CefBrowser browser)
        {
            int width = 0, height = 0;

            bool hasAlreadyBeenInitialized = false;

            Invoke(new Action(delegate
            {
                if (_browser != null)
                {
                    hasAlreadyBeenInitialized = true;
                }
                else
                {
                    _browser = browser;
                    Identifier = browser.Identifier;
                    browsers.TryAdd(browser.Identifier, this);
                    _browserHost = _browser.GetHost();
                    //_browserHost.SetZoomLevel(Root.RenderScaling);
                    // _browserHost.SetFocus(IsFocused);

                    width = (int)_browserWidth;
                    height = (int)_browserHeight;

                    var dispatcher = _cefClient.Dispatcher;
                    if (dispatcher != null)
                    {
                        _objectRegistry.SetBrowser(this.Browser);
                        _objectMethodDispatcher = new NativeObjectMethodDispatcher(dispatcher, _objectRegistry);
                    }
                    OnCreated(EventArgs.Empty);
                }
            }));

            // Make sure we don't initialize ourselves more than once. That seems to break things.
            if (hasAlreadyBeenInitialized)
                return;

            if (width > 0 && height > 0)
                _browserHost.WasResized();

            // 			mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            // 			{
            // 				if (!string.IsNullOrEmpty(this.initialUrl))
            // 				{
            // 					NavigateTo(this.initialUrl);
            // 					this.initialUrl = string.Empty;
            // 				}
            // 			}));
        }

        internal bool GetViewRect(ref CefRectangle rect)
        {
            bool rectProvided = false;
            CefRectangle browserRect = new CefRectangle();

            // TODO: simplify this
            //_mainUiDispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            //{
            try
            {
                // The simulated screen and view rectangle are the same. This is necessary
                // for popup menus to be located and sized inside the view.
                browserRect.X = browserRect.Y = 0;
                browserRect.Width = (int)_browserWidth;
                browserRect.Height = (int)_browserHeight;

                rectProvided = true;
            }
            catch (Exception ex)
            {
                _logger.ErrorException("WebBrowser: Caught exception in GetViewRect()", ex);
                rectProvided = false;
            }
            //}));

            if (rectProvided)
            {
                rect = browserRect;
            }

            //_logger.Debug("GetViewRect result provided:{0} Rect: X{1} Y{2} H{3} W{4}", rectProvided, browserRect.X, browserRect.Y, browserRect.Height, browserRect.Width);

            return rectProvided;
        }

        internal void GetScreenPoint(int viewX, int viewY, ref int screenX, ref int screenY)
        {
            Point ptScreen = new Point();

            Invoke(new Action(delegate
            {
                try
                {
                    Point ptView = new Point(viewX, viewY);
                    ptScreen = PointToScreen(ptView);
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WebBrowser: Caught exception in GetScreenPoint()", ex);
                }
            }));

            screenX = (int)ptScreen.X;
            screenY = (int)ptScreen.Y;
        }

        internal void HandleViewPaint(CefBrowser browser, CefPaintElementType type, CefRectangle[] dirtyRects, IntPtr buffer, int width, int height)
        {
            // When browser size changed - we just skip frame updating.
            // This is dirty precheck to do not do Invoke whenever is possible.
            if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;

            if (IsDisposing || IsDisposed || Root == null)
            {
                return;
            }
            Invoke(new Action(delegate
            {
                // Actual browser size changed check.
                if (_browserSizeChanged && (width != _browserWidth || height != _browserHeight)) return;
                Invalidate();
                try
                {
                    if (_browserSizeChanged)
                    {
                        //_browserPageBitmap = new WriteableBitmap((int)_browserWidth, (int)_browserHeight, 96, 96, AllowsTransparency ? PixelFormats.Bgra32 : PixelFormats.Bgr32, null);
                        //_browserPageImage.Source = _browserPageBitmap;
                        _browserPageBitmap = new Bitmap((int)_browserWidth, (int)_browserHeight);
                        using (DrawingContext dc = DrawingContext.FromBitmap(_browserPageBitmap))
                        {
                            dc.Clear(Color.Transparent);
                        }
                        _browserSizeChanged = false;
                    }

                    if (_browserPageBitmap != null)
                    {
                        DoRenderBrowser(_browserPageBitmap, width, height, dirtyRects, buffer);
                    }

                }
                catch (Exception ex)
                {
                    _logger.ErrorException("WebBrowser: Caught exception in HandleViewPaint()", ex);
                }
            }));
        }

        internal unsafe void HandlePopupPaint(int width, int height, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            if (width == 0 || height == 0)
            {
                return;
            }

            Invoke(() =>
            {
                _popup.Invalidate();
                //int stride = width * 4;
                //int sourceBufferSize = stride * height;

                //_logger.Debug("RenderPopup() Bitmap H{0}xW{1}, Browser H{2}xW{3}", _popupImageBitmap.Height, _popupImageBitmap.Width, width, height);
                using (Bitmap bmp = new Bitmap(width, height, 4 * width, PixelFormat.Bgra, sourceBuffer))
                {
                    using (var dc = DrawingContext.FromBitmap(_popupImageBitmap))
                    {
                        //var data = (byte*)sourceBuffer;
                        //using (var lk = _popupImageBitmap.Lock())
                        {
                            foreach (CefRectangle dirtyRect in dirtyRects)
                            {
                                //_logger.Debug(
                                //    string.Format(
                                //        "Dirty rect [{0},{1},{2},{3}]",
                                //        dirtyRect.X,
                                //        dirtyRect.Y,
                                //        dirtyRect.Width,
                                //        dirtyRect.Height));

                                if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                                {
                                    continue;
                                }

                                int adjustedWidth = dirtyRect.Width;

                                int adjustedHeight = dirtyRect.Height;

                                //Int32Rect sourceRect = new Int32Rect(dirtyRect.X, dirtyRect.Y, adjustedWidth, adjustedHeight);

                                //_popupImageBitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, dirtyRect.X, dirtyRect.Y);
                                //int offset = (width - dirtyRect.Width) * 4;
                                //var p = data + dirtyRect.Y * width * 4 + dirtyRect.X * 4;
                                //for (int y = 0; y < dirtyRect.Height; y++)
                                //{
                                //    for (int x = 0; x < dirtyRect.Width; x++)
                                //    {
                                //        lk.SetPixel(x + dirtyRect.X, y + dirtyRect.Y, p[3], p[2], p[1], p[0]);
                                //        p += 4;
                                //    } // x
                                //    p += offset;
                                //} // y
                                //lk.WritePixels(new PixelRect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height), data, PixelFormat.Bgra);

                                var rect = new Rect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
                                dc.PushClip(rect);
                                //if (AllowsTransparency)
                                //{
                                dc.Clear(Color.Transparent);
                                //}
                                dc.DrawImage(bmp, rect, rect);
                                dc.PopClip();
                            }
                        }
                    }
                }
            });
        }

        private unsafe void DoRenderBrowser(Bitmap bitmap, int browserWidth, int browserHeight, CefRectangle[] dirtyRects, IntPtr sourceBuffer)
        {
            //int stride = browserWidth * 4;
            //int sourceBufferSize = stride * browserHeight;

            //_logger.Debug("DoRenderBrowser() Bitmap H{0}xW{1}, Browser H{2}xW{3}", bitmap.Height, bitmap.Width, browserHeight, browserWidth);

            if (browserWidth == 0 || browserHeight == 0)
            {
                return;
            }
            using (Bitmap bmp = new Bitmap(browserWidth, browserHeight, 4 * browserWidth, PixelFormat.Bgra, sourceBuffer))
            {
                using (var dc = DrawingContext.FromBitmap(bitmap))
                {
                    //var data = (byte*)sourceBuffer;
                    //using (var lk = bitmap.Lock())
                    {
                        //Debug.WriteLine(dirtyRects.Length);
                        foreach (CefRectangle dirtyRect in dirtyRects)
                        {
                            //_logger.Debug(string.Format("Dirty rect [{0},{1},{2},{3}]", dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height));

                            if (dirtyRect.Width == 0 || dirtyRect.Height == 0)
                            {
                                continue;
                            }

                            // If the window has been resized, make sure we never try to render too much
                            //int adjustedWidth = (int)dirtyRect.Width;
                            //if (dirtyRect.X + dirtyRect.Width > (int) bitmap.Width)
                            //{
                            //    adjustedWidth = (int) bitmap.Width - (int) dirtyRect.X;
                            //}

                            //int adjustedHeight = (int)dirtyRect.Height;
                            //if (dirtyRect.Y + dirtyRect.Height > (int) bitmap.Height)
                            //{
                            //    adjustedHeight = (int) bitmap.Height - (int) dirtyRect.Y;
                            //}

                            // Update the dirty region
                            //Rect sourceRect = new Rect(dirtyRect.X, dirtyRect.Y, adjustedWidth, adjustedHeight);
                            //bitmap.WritePixels(sourceRect, sourceBuffer, sourceBufferSize, stride, (int)dirtyRect.X, (int)dirtyRect.Y);

                            //int offset = (browserWidth - dirtyRect.Width) * 4;
                            //var p = data + dirtyRect.Y * browserWidth * 4 + dirtyRect.X * 4;
                            //for (int y = 0; y < dirtyRect.Height; y++)
                            //{
                            //    for (int x = 0; x < dirtyRect.Width; x++)
                            //    {
                            //        lk.SetPixel(x + dirtyRect.X, y + dirtyRect.Y, p[3], p[2], p[1], p[0]);
                            //        p += 4;
                            //    } // x
                            //    p += offset;
                            //} // y


                            //lk.WritePixels(new PixelRect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height), data, PixelFormat.Bgra);
                            var rect = new Rect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
                            dc.PushClip(rect);
                            //if (AllowsTransparency)
                            //{
                            dc.Clear(Color.Transparent);
                            //}
                            dc.DrawImage(bmp, rect, rect);
                            dc.PopClip();
                        }
                    }
                }
            }
        }

        internal void OnPopupShow(bool show)
        {
            if (_popup == null)
            {
                return;
            }

            Invoke(() =>
            {
                if (show)
                {
                    _popup.Show();
                }
                else
                {
                    _popup.Hide();
                }
            });
        }

        internal void OnPopupSize(CefRectangle rect)
        {
            Invoke(() =>
            {
                if (_popupImageBitmap != null)
                {
                    _popupImageBitmap.Dispose();
                }
                _popupImageBitmap = new Bitmap(rect.Width, rect.Height);
                using (DrawingContext dc = DrawingContext.FromBitmap(_popupImageBitmap))
                {
                    dc.Clear(Color.Transparent);
                }
                _popup.FindPresenterByName<Picture>("content").Source = _popupImageBitmap;
                _popup.Width = rect.Width / Root.RenderScaling;
                _popup.Height = rect.Height / Root.RenderScaling;
                _popup.MarginLeft = rect.X / Root.RenderScaling;
                _popup.MarginTop = rect.Y / Root.RenderScaling;
                //_popup.HorizontalOffset = rect.X;
                //_popup.VerticalOffset = rect.Y;
            });
        }

        internal bool OnTooltip(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _tooltipTimer.Stop();
                UpdateTooltip(null);
            }
            else
            {
                _tooltipTimer.Tick += (sender, args) => UpdateTooltip(text);
                _tooltipTimer.Start();
            }

            return true;
        }

        protected override void OnTouchMove(TouchEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    Point location = arg.Position.Position;
                    CefTouchEvent cefTouchEvent = new CefTouchEvent();
                    cefTouchEvent.X = location.X * Root.RenderScaling;
                    cefTouchEvent.Y = location.Y * Root.RenderScaling;
                    cefTouchEvent.PointerType = CefPointerType.Touch;
                    cefTouchEvent.Type = CefTouchEventType.Moved;
                    _browserHost.SendTouchEvent(cefTouchEvent);
                }
            }
            catch (Exception e)
            {
                _logger.ErrorException("WebBrowser: Caught exception in TouchMove()", e);
            }
        }

        protected override void OnTouchDown(TouchEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    CaptureMouse();
                    Point location = arg.Position.Position;
                    CefTouchEvent cefTouchEvent = new CefTouchEvent();
                    cefTouchEvent.X = location.X * Root.RenderScaling;
                    cefTouchEvent.Y = location.Y * Root.RenderScaling;
                    cefTouchEvent.PointerType = CefPointerType.Touch;
                    cefTouchEvent.Type = CefTouchEventType.Pressed;
                    _browserHost.SendTouchEvent(cefTouchEvent);
                }
            }
            catch (Exception e)
            {
                _logger.ErrorException("WebBrowser: Caught exception in TouchDown()", e);
            }
        }

        protected override void OnTouchUp(TouchEventArgs arg)
        {
            try
            {
                if (_browserHost != null)
                {
                    Point location = arg.Position.Position;
                    CefTouchEvent cefTouchEvent = new CefTouchEvent();
                    cefTouchEvent.X = location.X * Root.RenderScaling;
                    cefTouchEvent.Y = location.Y * Root.RenderScaling;
                    cefTouchEvent.PointerType = CefPointerType.Touch;
                    cefTouchEvent.Type = CefTouchEventType.Released;
                    _browserHost.SendTouchEvent(cefTouchEvent);
                }
            }
            catch (Exception e)
            {
                _logger.ErrorException("WebBrowser: Caught exception in TouchUp()", e);
            }
            ReleaseMouseCapture();
        }

        #endregion

        #region Utils

        private CefEventFlags GetMouseModifiers(MouseEventArgs mouseEventArgs)
        {
            CefEventFlags modifiers = new CefEventFlags();

            if (mouseEventArgs.LeftButton == MouseButtonState.Pressed)
                modifiers |= CefEventFlags.LeftMouseButton;

            if (mouseEventArgs.MiddleButton == MouseButtonState.Pressed)
                modifiers |= CefEventFlags.MiddleMouseButton;

            if (mouseEventArgs.RightButton == MouseButtonState.Pressed)
                modifiers |= CefEventFlags.RightMouseButton;

            return modifiers;
        }

        private CefEventFlags GetKeyboardModifiers()
        {
            CefEventFlags modifiers = new CefEventFlags();
            var modi = Root.InputManager.KeyboardDevice.Modifiers;
            if (modi.HasFlag(InputModifiers.Alt))
                modifiers |= CefEventFlags.AltDown;
            if (modi.HasFlag(InputModifiers.Control))
                modifiers |= CefEventFlags.ControlDown;
            if (modi.HasFlag(InputModifiers.Shift))
                modifiers |= CefEventFlags.ShiftDown;
            if (modi.HasFlag(InputModifiers.Command))
                modifiers |= CefEventFlags.CommandDown;

            return modifiers;
        }

        private Popup CreatePopup()
        {
            var popup = new Popup
            {
                PlacementTarget = this,
                Placement = PlacementMode.Padding,
                CanActivate = false,
            };
            popup.Children.Add(new Picture
            {
                Margin = "0",
                PresenterFor = popup,
                Name = "content",
                Source = _popupImageBitmap,
                Stretch = Stretch.Fill,
            });
            return popup;
        }

        private void UpdateTooltip(string text)
        {
            Invoke(() =>
            {
                if (string.IsNullOrEmpty(text))
                {
                    _tooltip.Hide();
                }
                else
                {
                    _tooltip.Placement = PlacementMode.Mouse;
                    //_tooltip.Content = text;
                    _tooltip.FindPresenterByName<ContentControl>("content").Content = text;
                    _tooltip.Visibility = Visibility.Visible;
                }
            });

            _tooltipTimer.Stop();
        }

        //private void TooltipOnClosed(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    _tooltip.Visibility = Visibility.Collapsed;
        //    _tooltip.Placement = PlacementMode.Absolute;
        //}

        #endregion

        #region Methods

        //public void NavigateTo(string url)
        //{
        //    Url = url;
        //}

        //public void LoadString(string content, string url)
        //{
        //    // Remove leading whitespace from the URL
        //    url = url.TrimStart();

        //    if (_browser != null)
        //        _browser.GetMainFrame().LoadString(content, url);
        //}

        public CefCookieManager GetCookieManager()
        {
            return this.BrowserHost?.GetRequestContext().GetCookieManager(null);
        }

        public bool CanGoBack()
        {
            if (_browser != null)
                return _browser.CanGoBack;
            else
                return false;
        }

        public void GoBack()
        {
            if (_browser != null)
                _browser.GoBack();
        }

        public bool CanGoForward()
        {
            if (_browser != null)
                return _browser.CanGoForward;
            else
                return false;
        }

        public void GoForward()
        {
            if (_browser != null)
                _browser.GoForward();
        }

        public void Refresh()
        {
            if (_browser != null)
                _browser.Reload();
        }

        #endregion
    }

    public class AddressChangeEventArgs : EventArgs
    {
        public string Url { get; set; }

        public CefFrame Frame { get; set; }
    }
    //public class TitleChangeEventArgs : EventArgs
    //{
    //    public string Title { get; set; }
    //}
    /// <summary>
    /// 定义一个可以被JS调用的C#方法，需要将方法所在的对象设置到WebBrowser的CommandContext。如果JS端调用的时候最后加个回调函数，那该方法将被异步调用，那方法内部操作UI元素需要委托到主线程
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JSFunction : Attribute
    {

    }
}
