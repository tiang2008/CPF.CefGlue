using System;
using System.Collections.Generic;
using System.Text;

namespace CPF.Cef
{
    public class CpfCefSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new CpfCefResourceHandler();
        }
    }

    public class CpfCefResourceHandler : CefResourceHandler
    {
        protected override void Cancel()
        {
            
        }

        protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
        {
            var statusCode = _resourceResponse?.HttpStatus ?? System.Net.HttpStatusCode.BadRequest;

            if (_resourceResponse != null)
            {
                response.SetHeaderMap(_resourceResponse.Headers);
            }


            response.Status = (int)statusCode;

            redirectUrl = null;

            if (statusCode == System.Net.HttpStatusCode.OK)
            {




                responseLength = _resourceResponse.Length;
                response.MimeType = _resourceResponse.MimeType;

                if (_isPartContent)
                {
                    response.SetHeaderByName("Accept-Ranges", "bytes", true);

                    var startPos = 0;
                    var endPos = _resourceResponse.Length - 1;

                    if (_buffStartPostition.HasValue && _buffEndPostition.HasValue)
                    {
                        startPos = _buffStartPostition.Value;
                        endPos = _buffStartPostition.Value;
                    }
                    else if (!_buffEndPostition.HasValue && _buffStartPostition.HasValue)
                    {
                        startPos = _buffStartPostition.Value;
                    }

                    response.SetHeaderByName("Content-Range", $"bytes {startPos}-{endPos}/{_resourceResponse.Length}", true);
                    response.SetHeaderByName("Content-Length", $"{endPos - startPos + 1}", true);


                    response.Status = 206;

                    Logger.Verbose($"[Content-Range]: {startPos}-{endPos}/{_resourceResponse.Length}");
                }


                response.SetHeaderByName("Content-Type", response.MimeType, true);

                response.SetHeaderByName(X_POWERED_BY, $"NanUI/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}", true);
            }
            else
            {
                responseLength = 0;
            }
        }

        protected override bool Open(CefRequest request, out bool handleRequest, CefCallback callback)
        {
            var uri = new Uri(request.Url);
            var headers = request.GetHeaderMap();


            if (!string.IsNullOrEmpty(headers.Get("range")))
            {
                var rangeString = headers.Get("range");
                var group = System.Text.RegularExpressions.Regex.Match(rangeString, @"(?<start>\d+)-(?<end>\d*)")?.Groups;
                if (group != null)
                {
                    if (!string.IsNullOrEmpty(group["start"].Value) && int.TryParse(group["start"].Value, out int startPos))
                    {
                        _buffStartPostition = startPos;
                    }

                    if (!string.IsNullOrEmpty(group["end"].Value) && int.TryParse(group["end"].Value, out int endPos))
                    {
                        _buffEndPostition = endPos;
                    }
                }
                _isPartContent = true;
            }

            _readStreamOffset = 0;

            if (_buffStartPostition.HasValue)
            {
                _readStreamOffset = _buffStartPostition.Value;
            }


            byte[] postData = null;
            var uploadFiles = new List<string>();

            if (request.PostData != null)
            {
                var items = request.PostData.GetElements();

                if (items != null && items.Length > 0)
                {
                    var bytes = new List<byte>();
                    foreach (var item in items)
                    {

                        var buffer = item.GetBytes();

                        //var size = (int)item.BytesCount;

                        switch (item.ElementType)
                        {
                            case CefPostDataElementType.Bytes:
                                bytes.AddRange(buffer);
                                break;
                            case CefPostDataElementType.File:
                                uploadFiles.Add(item.GetFile());
                                break;
                        }

                    }

                    postData = bytes.ToArray();
                    bytes = null;
                }
            }

            var method = request.Method;

            var resourceRequest = new ResourceRequest(uri, method, headers, postData, uploadFiles.ToArray(), request);

            handleRequest = false;

            Task.Run(() =>
            {
                try
                {
                    Infos.Add($"[{request.Method}]");
                    Infos.Add($"{resourceRequest.RequestUrl}");

                    _resourceResponse = GetResourceResponse(resourceRequest);


                    if (_resourceResponse == null)
                    {
                        //callback.Cancel();
                        //return;

                        throw new NullReferenceException($"ResourceResponse should not be null.");
                    }


                    if (DisableCORS)
                    {
                        _resourceResponse.Headers.Set(ACCESS_CONTROL_ALLOW_HEADERS, "*");
                        _resourceResponse.Headers.Set(ACCESS_CONTROL_ALLOW_METHODS, "*");
                        _resourceResponse.Headers.Set(X_FRAME_OPTIONS, "ALLOWALL");

                        if (!string.IsNullOrEmpty(request.GetHeaderByName("origin")))
                        {
                            _resourceResponse.Headers.Set(ACCESS_CONTROL_ALLOW_ORIGIN, request.GetHeaderByName("origin"));
                            _resourceResponse.Headers.Set(ACCESS_CONTROL_MAX_AGE, "3600");
                        }
                        else
                        {
                            _resourceResponse.Headers.Set(ACCESS_CONTROL_ALLOW_ORIGIN, "*");

                        }
                    }




                    Infos.Add($"{(int)_resourceResponse.HttpStatus} {_resourceResponse.HttpStatus}");

                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "ResourceHandler");
                    callback.Cancel();
                }

            }, _cancellationTokenSource.Token).ContinueWith(t => callback.Continue());

            return true;

        }

        protected override bool Read(IntPtr dataOut, int bytesToRead, out int bytesRead, CefResourceReadCallback callback)
        {
            var total = _resourceResponse?.Length ?? 0;

            var bytesToCopy = (int)(total - _readStreamOffset);

            if (total == 0 || bytesToCopy <= 0)
            {
                bytesRead = 0;
                return false;
            }

            //if (bytesToCopy > bytesToRead)
            //{
            //    bytesToCopy = bytesToRead;
            //}

            bytesToCopy = Math.Min(bytesToCopy, bytesToRead);

            var buff = new byte[bytesToCopy];

            _resourceResponse.ContentStream.Position = _readStreamOffset;
            _resourceResponse.ContentStream.Read(buff, 0, bytesToCopy);

            Marshal.Copy(buff, 0, dataOut, bytesToCopy);

            _readStreamOffset += bytesToCopy;

            bytesRead = bytesToCopy;

            if (_readStreamOffset == _resourceResponse.Length)
            {

                if (WinFormium.Runtime.IsDebuggingMode)
                {
                    lock (Logger)
                    {
                        Logger.Debug($"[{this.GetType().Namespace}]:");
                        Logger.Verbose($" -> {string.Join(" ", Infos)}");
                    }

                }
                _resourceResponse.Dispose();

                _gcHandle.Free();


            }

            return true;

        }

        protected override bool Skip(long bytesToSkip, out long bytesSkipped, CefResourceSkipCallback callback)
        {
            bytesSkipped = bytesToSkip;
            return true;
        }
    }
}
