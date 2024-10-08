using System;
using System.Threading;

namespace CPF.CefGlue
{
    internal class CPFBrowserProcessHandler : CefBrowserProcessHandler
    {
        private IDisposable _current;
        private object _schedule = new object();

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            lock (_schedule)
            {
                if (_current != null)
                {
                    _current.Dispose();
                }

                if (delayMs <= 0)
                {
                    delayMs = 1;
                }

                new Thread(a =>
                {
                    while (true)
                    {
                        try
                        {
                            Thread.Sleep(15);
                            if (CPF.Platform.Application.Main != null)
                            {
                                CPF.Threading.Dispatcher.MainThread.Invoke(() =>
                                {
                                    CefRuntime.DoMessageLoopWork();
                                });
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                })
                { IsBackground = true, Name = "cefœﬂ≥Ã" }.Start();
            }
        }
    }
}
