using System;
using System.Diagnostics;
using CPF.Controls;

namespace CPF.CefGlue
{
    public class CpfCefDisplayHandler : CefDisplayHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }

        //public CpfCefDisplayHandler(WebBrowser owner)
        //{
        //    if (owner == null) throw new ArgumentNullException("owner");

        //    WebBrowser = owner;
        //}

        //protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        //{
        //}

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
            WebBrowser.OnAddressChange(browser, new AddressChangeEventArgs { Frame = frame, Url = url });
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
            WebBrowser.OnTitleChange(browser, title);
        }

        protected override bool OnTooltip(CefBrowser browser, string text)
        {
            return WebBrowser.OnTooltip(text);
        }

        protected override void OnStatusMessage(CefBrowser browser, string value)
        {
        }
#if Net4
        protected override bool OnConsoleMessage(CefBrowser browser, string message, string source, int line)
        {
            Debug.WriteLine(message + "行号：" + line + " 源：" + source);
            return false;
        }
#else
        protected override bool OnConsoleMessage(CefBrowser browser, CefLogSeverity level, string message, string source, int line)
        {
            if (WebBrowser.ShowConsoleMessage)
            {
                Debug.WriteLine(message + "行号：" + line + " 源：" + source);
                Console.WriteLine(message + "行号：" + line + " 源：" + source);
            }
            return false;
        }


        protected override bool OnCursorChange(CefBrowser browser, IntPtr cursorHandle, CefCursorType type, CefCursorInfo customCursorInfo)
        {
            CPF.Threading.Dispatcher.MainThread.Invoke(() =>
            {
                //Cursor cursor = CursorInteropHelper.Create(new SafeFileHandle(cursorHandle, false));
                Cursor cursor = Cursors.Arrow;
                switch (type)
                {
                    case CefCursorType.Pointer:
                        break;
                    case CefCursorType.Cross:
                        cursor = Cursors.Cross;
                        break;
                    case CefCursorType.Hand:
                        cursor = Cursors.Hand;
                        break;
                    case CefCursorType.IBeam:
                        cursor = Cursors.Ibeam;
                        break;
                    case CefCursorType.Wait:
                        cursor = Cursors.Wait;
                        break;
                    case CefCursorType.Help:
                        cursor = Cursors.Help;
                        break;
                    case CefCursorType.EastResize:
                        cursor = Cursors.RightSide;
                        break;
                    case CefCursorType.NorthResize:
                        cursor = Cursors.TopSide;
                        break;
                    case CefCursorType.NorthEastResize:
                        cursor = Cursors.TopRightCorner;
                        break;
                    case CefCursorType.NorthWestResize:
                        cursor = Cursors.TopLeftCorner;
                        break;
                    case CefCursorType.SouthResize:
                        cursor = Cursors.BottomSide;
                        break;
                    case CefCursorType.SouthEastResize:
                        cursor = Cursors.BottomRightCorner;
                        break;
                    case CefCursorType.SouthWestResize:
                        cursor = Cursors.BottomLeftCorner;
                        break;
                    case CefCursorType.WestResize:
                        cursor = Cursors.LeftSide;
                        break;
                    case CefCursorType.NorthSouthResize:
                        cursor = Cursors.SizeNorthSouth;
                        break;
                    case CefCursorType.EastWestResize:
                        cursor = Cursors.SizeWestEast;
                        break;
                    case CefCursorType.NorthEastSouthWestResize:
                        cursor = Cursors.SizeAll;
                        break;
                    case CefCursorType.NorthWestSouthEastResize:
                        cursor = Cursors.SizeAll;
                        break;
                    case CefCursorType.ColumnResize:
                        break;
                    case CefCursorType.RowResize:
                        break;
                    case CefCursorType.MiddlePanning:
                        break;
                    case CefCursorType.EastPanning:
                        break;
                    case CefCursorType.NorthPanning:
                        break;
                    case CefCursorType.NorthEastPanning:
                        break;
                    case CefCursorType.NorthWestPanning:
                        break;
                    case CefCursorType.SouthPanning:
                        break;
                    case CefCursorType.SouthEastPanning:
                        break;
                    case CefCursorType.SouthWestPanning:
                        break;
                    case CefCursorType.WestPanning:
                        break;
                    case CefCursorType.Move:
                        break;
                    case CefCursorType.VerticalText:
                        break;
                    case CefCursorType.Cell:
                        break;
                    case CefCursorType.ContextMenu:
                        break;
                    case CefCursorType.Alias:
                        break;
                    case CefCursorType.Progress:
                        cursor = Cursors.Wait;
                        break;
                    case CefCursorType.NoDrop:
                        cursor = Cursors.No;
                        break;
                    case CefCursorType.Copy:
                        cursor = Cursors.DragCopy;
                        break;
                    case CefCursorType.None:
                        break;
                    case CefCursorType.NotAllowed:
                        cursor = Cursors.No;
                        break;
                    case CefCursorType.ZoomIn:
                        break;
                    case CefCursorType.ZoomOut:
                        break;
                    case CefCursorType.Grab:
                        break;
                    case CefCursorType.Grabbing:
                        break;
                    case CefCursorType.Custom:
                        break;
                    default:
                        break;
                }
                WebBrowser.Cursor = cursor;
            });
            return true;
        }
#endif
    }
}
