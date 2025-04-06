using System.Runtime.InteropServices;
using System.Security;

namespace CPF.CefGlue.Interop;

#if !DEBUG
    [SuppressUnmanagedCodeSecurity]
#endif
internal static unsafe partial class libcef
{
    internal const string DllName = "libcef";

    internal const int ALIGN = 0;

    internal const CallingConvention CEF_CALL = CallingConvention.Cdecl;

    // Windows: CallingConvention.StdCall 
    //    Unix: CallingConvention.Cdecl
    // FIXME: CEF#598 (http://code.google.com/p/chromiumembedded/issues/detail?id=598)
    internal const CallingConvention CEF_CALLBACK = CallingConvention.Winapi;

    #region cef_version.h

    [DllImport(DllName, EntryPoint = "cef_version_info", CallingConvention = CEF_CALL)]
    public static extern int version_info(int entry);

    [DllImport(DllName, EntryPoint = "cef_api_hash", CallingConvention = CEF_CALL)]
    public static extern sbyte* api_hash(int version, int entry);

    #endregion
}