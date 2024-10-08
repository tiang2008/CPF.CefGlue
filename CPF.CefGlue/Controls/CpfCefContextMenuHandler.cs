using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CPF.CefGlue
{
    public class CpfCefContextMenuHandler : CefContextMenuHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
            contextMenu = new ContextMenu
            {
                PopupMarginLeft = 5,
                PopupMarginTop = 5,
                PopupMarginBottm = "auto",
                PopupMarginRight = "auto",
                Placement = PlacementMode.Mouse,
                Items = {
                new MenuItem{
                    Header="全选",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.SelectAll(); } }
                    }
                },
                new MenuItem{
                    Header="复制",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Copy(); } }
                    }
                },
                new MenuItem{
                    Header="粘贴",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Paste(); } }
                    }
                },
                new MenuItem{
                    Header="剪切",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Cut(); } }
                    }
                },
                new MenuItem{
                    Header="撤销",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Undo(); } }
                    }
                },
                new MenuItem{
                    Header="重做",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Redo(); } }
                    }
                },
                new MenuItem{
                    Header="删除",
                    Commands={
                        {nameof(MenuItem.MouseUp),(s,e)=>{if(cefFrame!=null&&cefFrame.IsValid)cefFrame.Delete() ; }}
                    }
                },
            }
            };
        }

        ContextMenu contextMenu;
        CefFrame cefFrame;
        protected override void OnBeforeContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams state, CefMenuModel model)
        {

        }

        protected override void OnContextMenuDismissed(CefBrowser browser, CefFrame frame)
        {
            cefFrame = null;
        }

        protected override bool OnContextMenuCommand(CefBrowser browser, CefFrame frame, CefContextMenuParams state, int commandId, CefEventFlags eventFlags)
        {
            return base.OnContextMenuCommand(browser, frame, state, commandId, eventFlags);
        }

        protected override bool RunContextMenu(CefBrowser browser, CefFrame frame, CefContextMenuParams parameters, CefMenuModel model, CefRunContextMenuCallback callback)
        {
            if (browser.GetHost().GetClient() is CpfCefClient client && client.Owner.Browser.Identifier == frame.Browser.Identifier)
            {
                cefFrame = frame;
            }
            else
            {
                cefFrame = null;
                return false;
            }
            var states = parameters.EditState;
            Threading.Dispatcher.MainThread.Invoke(() =>
            {
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("全选")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanSelectAll) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("复制")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanCopy) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("粘贴")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanPaste) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("剪切")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanCut) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("撤销")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanUndo) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("重做")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanRedo) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.Items.Cast<MenuItem>().First(a => a.Header.Equal("删除")).Visibility = states.HasFlag(CefContextMenuEditStateFlags.CanDelete) ? Visibility.Visible : Visibility.Collapsed;
                contextMenu.PlacementTarget = WebBrowser;
                contextMenu.IsOpen = true;
            });
            return frame != null;
        }
    }
}
