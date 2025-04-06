using System;
using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

public unsafe partial class CefTaskManager
{
    /// <summary>
    /// Returns the global task manager object.
    /// Returns nullptr if the method was called from the incorrect thread.
    /// </summary>
    /// <returns></returns>
    public static CefTaskManager? GetTaskManager()
    {
        cef_task_manager_t* task_manager_t = cef_task_manager_t.get();
        return FromNativeOrNull(task_manager_t);
    }

    public UIntPtr GetTasksCount()
    {
        UIntPtr task_count = cef_task_manager_t.get_tasks_count(_self);
        return task_count;
    }
}