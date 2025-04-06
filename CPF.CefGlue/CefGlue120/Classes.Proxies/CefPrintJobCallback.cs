using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

/// <summary>
///     Callback interface for asynchronous continuation of print job requests.
/// </summary>
public sealed unsafe partial class CefPrintJobCallback
{
    /// <summary>
    ///     Indicate completion of the print job.
    /// </summary>
    public void Continue()
    {
        cef_print_job_callback_t.cont(_self);
    }
}