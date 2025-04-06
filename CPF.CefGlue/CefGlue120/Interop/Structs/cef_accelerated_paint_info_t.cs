using System;
using System.Runtime.InteropServices;

namespace CPF.CefGlue.Interop;

internal struct cef_accelerated_paint_info_t
{
}

[StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
internal unsafe struct cef_accelerated_paint_info_t_windows
{
    public UIntPtr size;
    public IntPtr shared_texture_handle;
    public CefColorType format;
    
    #region Alloc & Free
    private static int _sizeof;

    static cef_accelerated_paint_info_t_windows()
    {
        _sizeof = Marshal.SizeOf(typeof(cef_accelerated_paint_info_t_windows));
    }

    public static cef_accelerated_paint_info_t_windows* Alloc()
    {
        var ptr = (cef_accelerated_paint_info_t_windows*)Marshal.AllocHGlobal(_sizeof);
        *ptr = new cef_accelerated_paint_info_t_windows();
        ptr->size = (UIntPtr)_sizeof;
        return ptr;
    }

    public static void Free(cef_accelerated_paint_info_t_windows* ptr)
    {
        if (ptr != null)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
    }
    #endregion
}

[StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
internal unsafe struct cef_accelerated_paint_native_pixmap_plane_t
{
    public UInt32 stride;
    public UInt64 offset;
    public UInt64 size;

    public int fd;
}

[StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
internal unsafe struct cef_accelerated_paint_info_t_linux
{
    public UIntPtr size;
    public cef_accelerated_paint_native_pixmap_plane_t plane1;
    public cef_accelerated_paint_native_pixmap_plane_t plane2;
    public cef_accelerated_paint_native_pixmap_plane_t plane3;
    public cef_accelerated_paint_native_pixmap_plane_t plane4;
    
    public int plane_count;
    public UInt64 modifier;
    public CefColorType format;
    
    #region Alloc & Free
    private static int _sizeof;

    static cef_accelerated_paint_info_t_linux()
    {
        _sizeof = Marshal.SizeOf(typeof(cef_accelerated_paint_info_t_linux));
    }

    public static cef_accelerated_paint_info_t_linux* Alloc()
    {
        var ptr = (cef_accelerated_paint_info_t_linux*)Marshal.AllocHGlobal(_sizeof);
        *ptr = new cef_accelerated_paint_info_t_linux();
        ptr->size = (UIntPtr)_sizeof;
        return ptr;
    }

    public static void Free(cef_accelerated_paint_info_t_linux* ptr)
    {
        if (ptr != null)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
    }
    #endregion
}

[StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
internal unsafe struct cef_accelerated_paint_info_t_mac
{
    public UIntPtr size;
    public IntPtr shared_texture_handle;
    public CefColorType format;
    
    #region Alloc & Free
    private static int _sizeof;

    static cef_accelerated_paint_info_t_mac()
    {
        _sizeof = Marshal.SizeOf(typeof(cef_accelerated_paint_info_t_mac));
    }

    public static cef_accelerated_paint_info_t_mac* Alloc()
    {
        var ptr = (cef_accelerated_paint_info_t_mac*)Marshal.AllocHGlobal(_sizeof);
        *ptr = new cef_accelerated_paint_info_t_mac();
        ptr->size = (UIntPtr)_sizeof;
        return ptr;
    }

    public static void Free(cef_accelerated_paint_info_t_mac* ptr)
    {
        if (ptr != null)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }
    }
    #endregion
}
