using System;
using CPF.CefGlue.Interop;
using CPF.CefGlue.Platform;

namespace CPF.CefGlue;

public abstract unsafe class CefAcceleratedPaintInfo
{
    internal static CefAcceleratedPaintInfo FromNative(cef_accelerated_paint_info_t* acceleratedPaintInfoT)
    {
        switch (CefRuntime.Platform)
        {
            case CefRuntimePlatform.Windows:
                return new CefAcceleratedPaintInfoWindowsImpl(acceleratedPaintInfoT);
            case CefRuntimePlatform.Linux:
                return new CefAcceleratedPaintInfoLinuxImpl(acceleratedPaintInfoT);
            case CefRuntimePlatform.MacOS:
                return new CefAcceleratedPaintInfoMacImpl(acceleratedPaintInfoT);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private bool _own;
    private bool _disposed;

    protected internal CefAcceleratedPaintInfo(bool own)
    {
        _own = own;
    }

    ~CefAcceleratedPaintInfo()
    {
        Dispose();
    }

    internal void Dispose()
    {
        _disposed = true;
        if (_own)
        {
            DisposeNativePointer();
        }
        GC.SuppressFinalize(this);
    }

    internal cef_accelerated_paint_info_t* ToNative()
    {
        cef_accelerated_paint_info_t* ptr = GetNativePointer();
        _own = false;
        return ptr;
    }

    protected internal void ThrowIfDisposed()
    {
        if (_disposed) throw ExceptionBuilder.ObjectDisposed();
    }

    public bool Disposed => _disposed;

    internal abstract cef_accelerated_paint_info_t* GetNativePointer();
    
    protected abstract void DisposeNativePointer();
    
    //Windows specific
    /// <summary>
    /// Handle for the shared texture. The shared texture is instantiated
    /// without a keyed mutex.
    /// </summary>
    public abstract IntPtr TextureHandle { get; set; }

    //Mac specific
    /// <summary>
    /// 
    /// </summary>
    public abstract IntPtr SharedTextureIoSurface { get; set; }
    
    //Common on all platforms
    /// <summary>
    /// The pixel format of the texture.
    /// </summary>
    public abstract CefColorType Format { get; set; }
}
