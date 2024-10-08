using CPF.CefGlue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPF.Controls
{
    public class CpfCefCookieVisitor : CefCookieVisitor
    {
        private readonly TaskCompletionSource<List<CefCookie>> TaskSource;
        private List<CefCookie> Cookies;
        public CpfCefCookieVisitor()
        {
            this.TaskSource = new TaskCompletionSource<List<CefCookie>>();
            Cookies = new List<CefCookie>();
        }
        protected override bool Visit(CefCookie cookie, int count, int total, out bool delete)
        {
            Cookies.Add(cookie);
            if (count >= total - 1)
            {
                TaskSource.TrySetResult(Cookies);
            }
            delete = false;
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            TaskSource.TrySetResult(Cookies ?? new List<CefCookie>());
            base.Dispose(disposing);
        }
        public Task<List<CefCookie>> Task
        {
            get => TaskSource.Task;
        }
    }
}
