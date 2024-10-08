using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPF.CefGlue
{
    public class LoadStartEventArgs : EventArgs
    {
		public LoadStartEventArgs(CefFrame frame)
		{
			Frame = frame;
		}

		public CefFrame Frame { get; private set; }
    }
}
