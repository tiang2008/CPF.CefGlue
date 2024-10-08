using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CPF.CefGlue
{
    class ILogger
    {
        public void Debug(string format, params object[] vs)
        {
            System.Diagnostics.Debug.WriteLine(format, vs);
        }
        public void Trace(string format, params object[] vs)
        {
            System.Diagnostics.Trace.WriteLine(string.Format(format, vs));
        }
        public void ErrorException(string format, Exception e)
        {
            System.Diagnostics.Debug.WriteLine(format + "：" + e);
            Console.WriteLine(format + "：" + e);
        }
    }
}
