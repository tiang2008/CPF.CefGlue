﻿using System;

namespace CPF.CefGlue
{
	public class LoadErrorEventArgs : EventArgs
	{
		public LoadErrorEventArgs(CefFrame frame, CefErrorCode errorCode, string errorText, string failedUrl)
		{
			Frame = frame;
			ErrorCode = errorCode;
			ErrorText = errorText;
			FailedUrl = failedUrl;
		}

		public string FailedUrl { get; private set; }

		public string ErrorText { get; private set; }

		public CefErrorCode ErrorCode { get; private set; }

		public CefFrame Frame { get; private set; }
	}
}
