﻿using CPF.CefGlue;

namespace CPF.CefGlue.JSExtenstions
{
    public class MessageReceivedEventArgs
    {
        public MessageReceivedEventArgs(CefBrowser browser, CefFrame frame, CefProcessId processId, CefProcessMessage message)
        {
            Browser = browser;
            Frame = frame;
            ProcessId = processId;
            Message = message;
        }

        public CefBrowser Browser { get; }
        public CefFrame Frame { get; }
        public CefProcessId ProcessId { get; }
        public CefProcessMessage Message { get; }
    }
}
