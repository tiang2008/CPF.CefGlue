using System;

namespace CPF.CefGlue;

[Flags]
public enum CefLogItems
{
    Default = 0,
    None = 1,
    FlagProcessId = 1 << 1,
    FlagThreadId = 1 << 2,
    FlagTimeStamp = 1 << 3,
    FlagTickCount = 1 << 4
}