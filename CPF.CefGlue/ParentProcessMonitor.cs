using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CPF.CefGlue
{
    /// <summary>
    /// Prevents current process from staying alive after parent process dies.
    /// </summary>
    public static class ParentProcessMonitor
    {
        public static void StartMonitoring(int parentProcessId)
        {
            Task.Factory.StartNew(() => AwaitParentProcessExit(parentProcessId), TaskCreationOptions.LongRunning);
        }

        private static async void AwaitParentProcessExit(int parentProcessId)
        {
            try
            {
                var parentProcess = Process.GetProcessById(parentProcessId);
                parentProcess.WaitForExit();
            }
            catch
            {
                //main process probably died already
            }

            await Delay(10); // wait a bit before exiting

            Environment.Exit(0);
        }

        public static Task Delay(int milliseconds)
         {
             var tcs = new TaskCompletionSource<object>();
             var timer = new System.Timers.Timer(milliseconds) { AutoReset = false };
             timer.Elapsed += delegate { timer.Dispose();tcs.SetResult(null); };
             timer.Start();
             return tcs.Task;
         }
    }
}
