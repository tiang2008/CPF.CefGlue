using System;
using System.Runtime.InteropServices;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

[StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
internal struct cef_task_info_t
{
    public UIntPtr size;
    
    public Int64 id;

    public CefTaskType type;

    public int is_killable;

    public cef_string_t title;

    public double cpu_usage;

    public int number_of_processors;

    public Int64 memory;

    public Int64 gpu_memory;

    public int is_gpu_memory_inflated;
}