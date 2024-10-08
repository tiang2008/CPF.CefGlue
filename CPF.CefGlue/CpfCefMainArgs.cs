using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CPF.CefGlue
{
    public class CpfCefMainArgs : CefMainArgs
    {
        public CpfCefMainArgs(string[] args) : base(ChangeArgs(args))
        { }

        static string[] ChangeArgs(string[] args)
        {
            var argv = args;
            if (CefRuntime.Platform != CefRuntimePlatform.Windows)
            {
                argv = new string[args.Length + 1];
                Array.Copy(args, 0, argv, 1, args.Length);
                argv[0] = "-";
                if (CefRuntime.Platform == CefRuntimePlatform.MacOS && argv != null && argv.Length > 0 && argv.Any(a => a.StartsWith("--type")))
                {
                    var mac = CPF.Platform.Application.GetRuntimePlatform();
                    mac.GetType().GetMethod("HideDockIcon").Invoke(mac, null);
                }
            }
            return argv;
        }
    }
}
