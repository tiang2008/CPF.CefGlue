using CPF.CefGlue;
using CPF.Linux;//如果需要支持Linux才需要
using CPF.Mac;//如果需要支持Mac才需要
using CPF.Platform;
using CPF.Skia;
using CPF.Windows;
using System;
using System.Linq;

namespace Demo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var mainArgs = new CpfCefMainArgs(args);
            var app = new CpfCefApp();
            var exitCode = CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero);
            if (exitCode != -1)
            {
                Environment.Exit(exitCode);
                return;
            }

            //到 https://cef-builds.spotifycdn.com/index.html#windows64:133.4.8%2Bg99a2ab1%2Bchromium-133.0.6943.142 下载对应平台的二进制文件，一般是选择 Sample Application ......client.tar.bz2
            //需要注意的是如果你需要的是Linux平台的，需要手动使用strip命令将调试信息剥离（Linux那边打开控制台输入strip 再把libcef.so拖进来，再按enter，so文件就小了）或者自己调整编译参数重新编译，因为网站上下载的二进制文件巨大，达到一个G了
            //一般情况下，把压缩包里的比如libcef...同目录里的所有文件和文件夹都复制到你的程序目录就行
            //如果是Mac的话
            //将文件\Release\Chromium Embedded Framework.framework\Chromium Embedded Framework复制到你的程序目录并重命名为libcef.dylib
            //将“\Release\Chromium Embedded Framework.framework\Libraries”文件夹中的所有文件和文件夹复制你的程序目录
            //将“\Release\Chromium Embedded Framework.framework\Resources”文件夹中的所有文件和文件夹复制到你的程序目录

            //如果你需要支持视频播放，那你需要自己修改编译参数，重新编译才行，具体教程可以百度

            //如果你想自定义特殊功能，比如拦截请求，你需要继承 WebBrowser，并重写 OnCreateWebBrowser
            //OnCreateWebBrowser里面写client.LoadHandler = new CpfCefLoadHandler();继承并重写对应的Handler并设置过来

            //mac上运行在任务栏上可能会有多个图标闪烁之后就没了，是正常的，cef的多进程问题，不影响使用

            Application.Initialize(
                (OperatingSystemType.Windows, new WindowsPlatform(), new SkiaDrawingFactory())
                //, (OperatingSystemType.OSX, new MacPlatform(), new SkiaDrawingFactory())//如果需要支持Mac才需要
                //, (OperatingSystemType.Linux, new LinuxPlatform(), new SkiaDrawingFactory())//如果需要支持Linux才需要
            );

            CefRuntime.Load();

            CefRuntime.Initialize(mainArgs, new CefSettings { }, app, IntPtr.Zero);

            var model = new MainModel();
            Application.Run(new Window1 { DataContext = model, CommandContext = model });

            CefRuntime.Shutdown();
        }
    }
}
