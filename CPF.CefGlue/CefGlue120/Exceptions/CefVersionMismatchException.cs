﻿namespace CPF.CefGlue;

public sealed class CefVersionMismatchException : CefRuntimeException
{
    public CefVersionMismatchException(string message)
        : base(message)
    {
    }
}