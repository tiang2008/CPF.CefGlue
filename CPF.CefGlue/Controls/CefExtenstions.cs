using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using CPF.CefGlue;
using System.Threading.Tasks;
using System.IO;

namespace CPF.Controls
{
    public static class CefExtenstions
    {
        internal static ConcurrentDictionary<string, ESMessage> EJSMessages = new ConcurrentDictionary<string, ESMessage>();
        /// <summary>
        /// 只能返回这些类型 null，string，int，double，DateTime，bool
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="js"></param>
        /// <returns></returns>
        public static Task<object> ExecuteJavaScript(this CefFrame frame, string js)
        {
            //frame.ExecuteJavaScript("eval()", "", 0);
            return Task.Factory.StartNew(() =>
            {
                var guid = Guid.NewGuid().ToString();
                CefProcessMessage message = CefProcessMessage.Create("CharpCallJS");
                message.Arguments.SetInt(0, frame.Browser.Identifier);
                message.Arguments.SetString(1, frame.Identifier.ToString());
                message.Arguments.SetString(2, js);
                message.Arguments.SetString(3, guid);
                ManualResetEvent manualResetEvent = new ManualResetEvent(false);
                var msg = new ESMessage { ManualReset = manualResetEvent };
                EJSMessages.TryAdd(guid, msg);
                SendProcessMessage(frame.Browser, CefProcessId.Renderer, message);
                manualResetEvent.WaitOne();
                EJSMessages.TryRemove(guid, out _);
                return msg.Result;
            });
        }

        static void SendProcessMessage(CefBrowser _browser, CefProcessId targetProcess, CefProcessMessage message)
        {
#if Net4
            _browser.SendProcessMessage(targetProcess, message);
#else
            _browser.GetMainFrame().SendProcessMessage(targetProcess, message);
#endif
        }

        internal static void WriteStringEx(this BinaryWriter binaryWriter, string str)
        {
            var bytes = Encoding.Unicode.GetBytes(str);
            binaryWriter.Write(bytes.Length);
            binaryWriter.Write(bytes);
        }

        internal static string ReadStringEx(this BinaryReader binaryReader)
        {
            var count = binaryReader.ReadInt32();
            var bytes = binaryReader.ReadBytes(count);
            return Encoding.Unicode.GetString(bytes);
        }

        public static Task<List<CefCookie>> GetCookiesAsync(this CefCookieManager CookieManager)
        {
            var cookie_vistor = new CpfCefCookieVisitor();
            if (CookieManager.VisitAllCookies(cookie_vistor))
            {
                return cookie_vistor.Task;
            }
#if NET40
            return TaskEx.FromResult(new List<CefCookie>());
#else
            return Task.FromResult(new List<CefCookie>());
#endif
        }
        public static List<CefCookie> GetCookies(this CefCookieManager CookieManager)
        {
            return Task.Factory.StartNew(() => CookieManager.GetCookiesAsync().Result).Result;
        }
        public static bool SetCookies(this CefCookieManager CookieManager, string url, List<CefCookie> Cookies)
        {
            try
            {
                foreach (var item in Cookies)
                {
                    item.Expires =  new CefBaseTime(DateTime.Now.AddDays(1).Ticks);
                    item.Creation = new CefBaseTime(DateTime.Now.Ticks);
                    item.LastAccess = new CefBaseTime(DateTime.Now.Ticks);
                    CookieManager.SetCookie(url, item, null);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool ClearCookies(this CefCookieManager CookieManager)
        {
            return CookieManager.DeleteCookies(null, null, null);
        }
    }

    class ESMessage
    {
        public ManualResetEvent ManualReset;

        public object Result;
    }
}
