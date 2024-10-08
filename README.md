# C#跨平台UI框架CPF https://gitee.com/csharpui/CPF
﻿
## CPF.Cef

对Cef的封装，支持跨平台

### 开发说明

CEF_VERSION = "120.1.8+ge6b45b0+chromium-120.0.6099.109"

导入CefGlue源码，修改命名空间，https://gitlab.com/xiliumhq/chromiumembedded/cefglue

取消CefMainArgs类型的密封修饰

修改CefRuntime.Initialize 增加一段代码

删除cef_string_t.disabled.cs文件

修改CefSettings

将Wrapper/MessageRouter/Helpers.cs 改成public

### 使用说明

案例源码：http://cpf.cskin.net/Item/19

到 https://cef-builds.spotifycdn.com/index.html#windows32:120.1.8%2Bge6b45b0%2Bchromium-120.0.6099.109 下载对应平台的二进制文件，一般是选择 Sample Application ......client.tar.bz2

需要注意的是如果你需要的是Linux平台的，需要手动使用strip命令将调试信息剥离（Linux那边打开控制台输入strip 再把libcef.so拖进来，再按enter，so文件就小了）或者自己调整编译参数重新编译，因为网站上下载的二进制文件巨大，达到一个G了

一般情况下，把压缩包里的比如libcef...同目录里的所有文件和文件夹都复制到你的程序目录就行

如果是Mac的话 将文件\Release\Chromium Embedded Framework.framework\Chromium Embedded Framework复制到你的程序目录并重命名为libcef.dylib 将“\Release\Chromium Embedded Framework.framework\Libraries”文件夹中的所有文件和文件夹复制你的程序目录 将“\Release\Chromium Embedded Framework.framework\Resources”文件夹中的所有文件和文件夹复制到你的程序目录

如果你需要支持视频播放，那你需要自己修改编译参数，重新编译才行，具体教程可以百度

如果你想自定义特殊功能，比如拦截请求，你需要继承 WebBrowser，并重写 OnCreateWebBrowser OnCreateWebBrowser里面写client.LoadHandler = new CpfCefLoadHandler();继承并重写对应的Handler并设置过来

mac上运行在任务栏上可能会有多个图标闪烁之后就没了，是正常的，cef的多进程问题，不影响使用