using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue.Platform;

internal sealed unsafe class CefAcceleratedPaintInfoLinuxImpl : CefAcceleratedPaintInfo
{
    private cef_accelerated_paint_info_t_linux* _self;
    
    internal CefAcceleratedPaintInfoLinuxImpl(cef_accelerated_paint_info_t* ptr) : base(true)
    {
        if (CefRuntime.Platform != CefRuntimePlatform.Linux)
            throw new InvalidOperationException();

        _self = (cef_accelerated_paint_info_t_linux*)ptr;
    }

    internal override unsafe cef_accelerated_paint_info_t* GetNativePointer()
    {
        return (cef_accelerated_paint_info_t*)_self; 
    }

    protected override void DisposeNativePointer()
    {
        cef_accelerated_paint_info_t_linux.Free(_self);
        _self = null;
    }

    public override IntPtr TextureHandle { get; set; }
    public override IntPtr SharedTextureIoSurface { get; set; }
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