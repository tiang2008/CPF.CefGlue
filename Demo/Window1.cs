﻿using CPF;
using CPF.Animation;
 using CPF.CefGlue;
using CPF.Charts;
using CPF.Controls;
using CPF.Drawing;
using CPF.Shapes;
using CPF.Styling;
using CPF.Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransLinkApp.Core;

namespace Demo
{
    public class Window1 : Window
    {
        protected override void InitializeComponent()
        {
            LoadStyleFile("res://Demo/Stylesheet1.css");
            //加载样式文件，文件需要设置为内嵌资源

            Title = "标题";
            Width = 690;
            Height = 434;
            Background = null;
            CanResize = true;
            Children.Add(new WindowFrame(this, new Panel
            {
                Width = "100%",
                Height = "100%",
                Children =
                {
                    new Button
                    {
                        Commands =
                        {
                            {
                                nameof(Button.Click),
                                nameof(SetUrl),
                                this,
                                CommandParameter.EventSender,
                                CommandParameter.EventArgs
                            },
                        },
                        Height = 25,
                        Width = 60,
                        MarginLeft = 25,
                        MarginTop = 15,
                        Content="浏览"
                    },
                    new TextBox
                    {
                        PresenterFor = this,
                        Name = nameof(textBox),
                        Bindings =
                        {
                            {
                                nameof(TextBox.Text),
                                "Url",
                                FindPresenterByName("webBrowser")
                            },
                        },
                        Classes = "singleLine",
                        Height = 26,
                        MarginLeft = 105,
                        MarginTop = 15,
                        Width = 322,
                    },
                    new MyWebBrowser
                    {
                        ID="a",
                        PresenterFor = this,
                        Name = nameof(webBrowser),
                        Bindings =
                        {
                            {
                                nameof(WebBrowser.Title),
                                "Title",
                                this,
                                BindingMode.OneWayToSource
                            },
                        },
                        //CommandContext=this,
                        MarginTop=50,
                        MarginLeft=0,
                        MarginRight=0,
                        MarginBottom=0,
                    },
                    new Button
                    {
                        Commands =
                        {
                            {
                                nameof(Button.Click),
                                nameof(ShowDev),
                                this,
                                CommandParameter.EventSender,
                                CommandParameter.EventArgs
                            },
                        },
                        Width = 90,
                        Height = 25,
                        MarginLeft = 444,
                        MarginTop = 15,
                        Content = "开发者工具",
                    },
                    new Button
                    {
                        Commands =
                        {
                            {
                                nameof(Button.Click),
                                nameof(InvokeJS),
                                this,
                                CommandParameter.EventSender,
                                CommandParameter.EventArgs
                            },
                        },
                        Width = 105,
                        Height = 25,
                        MarginLeft = 559,
                        MarginTop = 15,
                        Content = "调用JS并调用C#",
                    },
                }
            })
            {
                MaximizeBox = true
            });
            if (!DesignMode)//设计模式下不执行，也可以用#if !DesignMode
            {

            }
        }
        TextBox textBox;
        WebBrowser webBrowser;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            webBrowser = FindPresenterByName<WebBrowser>(nameof(webBrowser));
            textBox = FindPresenterByName<TextBox>(nameof(textBox));

            webBrowser.LoadEnd += WebBrowser_LoadEnd;
        }

        private void WebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            
        }

        void SetUrl(CpfObject obj, RoutedEventArgs eventArgs)
        {
           
            webBrowser.Url = textBox.Text;
        }
        void ShowDev(CpfObject obj, RoutedEventArgs eventArgs)
        {//开发者工具暂时只能支持Windows
            webBrowser.ShowDev();
        }
        async void InvokeJS(CpfObject obj, RoutedEventArgs eventArgs)
        {
            string js = @"
 function GetMessageFromDBAsync(text) {
                console.log(""TransLinkApp_GetMessageFromDB"", text);
                return new Promise((resolve) => {

                    // 使用 setTimeout 将同步操作异步化
                    //setTimeout(() => {
                    TestFun(text,resolve);
                    //}, 0);
                });
            }

GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
GetMessageFromDBAsync('调用绑定到JS里的C#方法')
                                .then((transferText) => {
                                    console.log('handleMessageIncome:'+ transferText);
                                }).catch(() => {
                                    console.log(""catch"");
                                });
";
            var r = await webBrowser.ExecuteJavaScript(js);
            //MessageBox.Show("111");
        }

    }
}
