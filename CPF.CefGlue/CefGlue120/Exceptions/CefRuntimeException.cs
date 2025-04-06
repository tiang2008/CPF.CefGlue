using System;

namespace CPF.CefGlue;

public class CefRuntimeException : Exception
{
    public CefRuntimeException(string message)
        : base(message)
    {
    }
}