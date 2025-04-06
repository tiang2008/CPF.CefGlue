using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue.Platform;

internal sealed unsafe class CefAcceleratedPaintInfoMacImpl : CefAcceleratedPaintInfo
{
    private cef_accelerated_paint_info_t_mac* _self;
    
    internal CefAcceleratedPaintInfoMacImpl(cef_accelerated_paint_info_t* ptr) : base(true)
    {
        _self = (cef_accelerated_paint_info_t_mac*)ptr;
    }

    internal override unsafe cef_accelerated_paint_info_t* GetNativePointer()
    {
        return (cef_accelerated_paint_info_t*)_self; 
    }

    protected override void DisposeNativePointer()
    {
        cef_accelerated_paint_info_t_mac.Free(_self);
        _self = null;
    }

    public override IntPtr TextureHandle { get; set; }
    public override IntPtr SharedTextureIoSurface
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