﻿namespace CPF.CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CPF.CefGlue.Interop;

    public struct CefPoint
    {
        private int _x;
        private int _y;

        public CefPoint(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
