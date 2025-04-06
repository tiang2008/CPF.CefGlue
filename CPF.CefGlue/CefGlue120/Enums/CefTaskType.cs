namespace CPF.CefGlue;

/// <summary>
/// Specifies the task type variants supported by CefTaskManager.
/// Should be kept in sync with Chromium's task_manager::Task::Type type.
/// </summary>
public enum CefTaskType
{
    Unknown = 0,
    /// <summary>
    /// The main browser process.
    /// </summary>
    Browser,
    
    /// <summary>
    /// A graphics process.
    /// </summary>
    Gpu,
    
    /// <summary>
    /// A Linux zygote process.
    /// </summary>
    Zygote,
    
    
    /// <summary>
    /// A browser utility process.
    /// </summary>
    Utility,
    
    /// <summary>
    /// A normal WebContents renderer process.
    /// </summary>
    Renderer,
    
    /// <summary>
    /// An extension or app process.
    /// </summary>
    Extension,
    
    /// <summary>
    /// A browser plugin guest process.
    /// </summary>
    Guest,
    
    /// <summary>
    /// A plugin process.
    /// </summary>
    Plugin,
    
    /// <summary>
    /// A sandbox helper process
    /// </summary>
    SandboxHelper,
    
    /// <summary>
    /// A dedicated worker running on the renderer process.
    /// </summary>
    Dedicatedworker,
    
    /// <summary>
    /// A shared worker running on the renderer process.
    /// </summary>
    SharedWorker,
    
    /// <summary>
    /// A service worker running on the renderer process.
    /// </summary>
    ServiceWorker,
}