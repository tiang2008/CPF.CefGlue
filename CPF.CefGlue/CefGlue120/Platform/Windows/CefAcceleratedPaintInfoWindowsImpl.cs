using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue.Platform;

internal sealed unsafe class CefAcceleratedPaintInfoWindowsImpl : CefAcceleratedPaintInfo
{
    private cef_accelerated_paint_info_t_windows* _self;

    public CefAcceleratedPaintInfoWindowsImpl(cef_accelerated_paint_info_t* ptr)
    : base(true)
    {
        if (CefRuntime.Platform != CefRuntimePlatform.Windows)
            throw new InvalidOperationException();

        _self = (cef_accelerated_paint_info_t_windows*)ptr;
    }

    internal override cef_accelerated_paint_info_t* GetNativePointer()
    {
        return (cef_accelerated_paint_info_t*)_self; 
    }

    protected override void DisposeNativePointer()
    {
        cef_accelerated_paint_info_t_windows.Free(_self);
        _self = null;
    }

    public override IntPtr TextureHandle 
    {
        get
        {
            ThrowIfDisposed();
            return _self->shared_texture_handle;
        }
        set
        {
            ThrowIfDisposed();
            _self->shared_texture_handle = value;
        }
    }
    
    public override IntPtr SharedTextureIoSurface
    {
        get
        {
            ThrowIfDisposed();
            return IntPtr.Zero;
        }
        set => ThrowIfDisposed();
    }

    public override CefColorType Format
    { 
        get
        {
            ThrowIfDisposed();
            return _self->format;
        }
        set
        {
            ThrowIfDisposed();
            _self->format = value;
        }
    }
}