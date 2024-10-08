using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPF.CefGlue
{
    public class CpfCefJSDialogHandler : CefJSDialogHandler
    {
        public WebBrowser WebBrowser { get; private set; }
        internal void SetWebBrowser(WebBrowser WebBrowser)
        {
            this.WebBrowser = WebBrowser;
        }
#if Net4
        protected override bool OnJSDialog(CefBrowser browser, string originUrl, string acceptLang, CefJSDialogType dialogType, string message_text, string default_prompt_text, CefJSDialogCallback callback, out bool suppress_message)
        {
            bool success = false;
            string input = null;

            switch (dialogType)
            {
                case CefJSDialogType.Alert:
                    success = this.ShowJSAlert(message_text);
                    break;
                case CefJSDialogType.Confirm:
                    success = this.ShowJSConfirm(message_text);
                    break;
                case CefJSDialogType.Prompt:
                    success = this.ShowJSPrompt(message_text, default_prompt_text, out input);
                    break;
            }

            callback.Continue(success, input);
            suppress_message = false;
            return true;
        }
#else
        protected override bool OnJSDialog(CefBrowser browser, string originUrl, CefJSDialogType dialogType, string message_text, string default_prompt_text, CefJSDialogCallback callback, out bool suppress_message)
        {
            bool success = false;
            string input = null;

            switch (dialogType)
            {
                case CefJSDialogType.Alert:
                    success = this.ShowJSAlert(message_text);
                    break;
                case CefJSDialogType.Confirm:
                    success = this.ShowJSConfirm(message_text);
                    break;
                case CefJSDialogType.Prompt:
                    success = this.ShowJSPrompt(message_text, default_prompt_text, out input);
                    break;
            }

            callback.Continue(success, input);
            suppress_message = false;
            return true;
        }
#endif
        protected override bool OnBeforeUnloadDialog(CefBrowser browser, string messageText, bool isReload, CefJSDialogCallback callback)
        {
            return true;
        }

        protected override void OnDialogClosed(CefBrowser browser)
        {
        }

        protected override void OnResetDialogState(CefBrowser browser)
        {
        }

        private bool ShowJSAlert(string message)
        {
            //WpfCefJSAlert alert = new WpfCefJSAlert(message);
            //return alert.ShowDialog() == true;
            //Debug.WriteLine("Alert弹窗：" + message);
            var result = MessageBox.ShowSync(message, "网页显示");
            return result != null;
        }

        private bool ShowJSConfirm(string message)
        {
            //WpfCefJSConfirm confirm = new WpfCefJSConfirm(message);
            //return confirm.ShowDialog() == true;
            Window main = Window.Windows.FirstOrDefault(a => a.IsKeyboardFocusWithin);
            if (main == null)
            {
                main = Window.Windows.FirstOrDefault(a => a.IsMain);
            }
            var os = CPF.Platform.Application.OperatingSystem;
            if (main == null && (os == CPF.Platform.OperatingSystemType.Windows || os == CPF.Platform.OperatingSystemType.Linux || os == CPF.Platform.OperatingSystemType.OSX))
            {
                throw new Exception("需要有主窗体");
            }
            object result = null;
            main.Invoke(() =>
            {
                Window window = new Window { CanResize = false, Background = null, Icon = main.Icon, MinWidth = 200, Name = "messageBox", Title = "网页显示" };
                window.LoadStyle(main);
                window.Children.Add(new WindowFrame(window, new Panel
                {
                    Width = "100%",
                    Children =
                    {
                        new TextBlock
                        {
                            Name="message", Text = message == null ? "" : message,MarginBottom=50,MarginTop=15
                        },
                        new Button
                        {
                            Content="Yes",
                            Width=60,
                            MarginBottom=15,
                            MarginLeft=15,
                            Commands=
                            {
                                {nameof(Button.Click),(s,e)=> { window.DialogResult = true; } }
                            }
                        },
                        new Button
                        {
                            Content="No",
                            Width=60,
                            MarginBottom=15,
                            MarginRight=15,
                            Commands=
                            {
                                {nameof(Button.Click),(s,e)=> { window.DialogResult = false; } }
                            }
                        },
                    }
                })
                { MinimizeBox = false, MaximizeBox = false, });
                result = window.ShowDialogSync(main);
            });
            return result != null;
        }

        private bool ShowJSPrompt(string message, string defaultText, out string input)
        {
            //WpfCefJSPrompt promt = new WpfCefJSPrompt(message, defaultText);
            //if (promt.ShowDialog() == true)
            //{
            //    input = promt.Input;
            //    return true;
            //}
            //else
            //{
            ///input = null;
            //    return false;
            //}     
            Window main = Window.Windows.FirstOrDefault(a => a.IsKeyboardFocusWithin);
            if (main == null)
            {
                main = Window.Windows.FirstOrDefault(a => a.IsMain);
            }
            var os = CPF.Platform.Application.OperatingSystem;
            if (main == null && (os == CPF.Platform.OperatingSystemType.Windows || os == CPF.Platform.OperatingSystemType.Linux || os == CPF.Platform.OperatingSystemType.OSX))
            {
                throw new Exception("需要有主窗体");
            }

            //Task<object> task = null;
            object result = null;
            //var cancel = new System.Threading.CancellationTokenSource();
            main.Invoke(() =>
            {
                Window window = new Window { CanResize = false, Background = null, Icon = main.Icon, MinWidth = 200, Name = "messageBox", Title = "网页显示" };
                window.LoadStyle(main);
                window.Children.Add(new WindowFrame(window, new Panel
                {
                    Width = "100%",
                    Children =
                    {
                        new TextBlock
                        {
                            Name="message", Text = message == null ? "" : message,MarginBottom=100,MarginTop=15
                        },
                        new TextBox
                        {
                            MarginLeft=20,
                            MarginRight=20,
                            MarginTop=50,
                            //Height=50,
                            BorderFill="#aaa",
                            BorderStroke="1",
                            Text=defaultText==null?"":defaultText,
                            PresenterFor=window,
                            Name="textBox",
                        },
                        new Button
                        {
                            Content="Yes",
                            Width=60,
                            MarginBottom=15,
                            MarginLeft=15,
                            Commands=
                            {
                                {nameof(Button.Click),(s,e)=> { window.DialogResult = window.FindPresenterByName<TextBox>("textBox").Text; } }
                            }
                        },
                        new Button
                        {
                            Content="No",
                            Width=60,
                            MarginBottom=15,
                            MarginRight=15,
                            Commands=
                            {
                                {nameof(Button.Click),(s,e)=> { window.Close(); } }
                            }
                        },
                    }
                })
                { MinimizeBox = false, MaximizeBox = false, });
                //task = window.ShowDialog(main);
                result = window.ShowDialogSync(main);
            });

            //Task.Factory.StartNew(() =>
            //{
            //    result = task.Result;
            //    cancel.Cancel();
            //});
            //CPF.Platform.Application.Run(cancel.Token);
            //Console.WriteLine(result);
            if (result != null)
            {
                input = result.ToString();
                return true;
            }
            else
            {
                input = null;
                return false;
            }
        }

    }
}
