 using CPF.CefGlue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Demo
{
    class MainModel : CPF.CpfObject
    {
        /// <summary>
        /// 定义的方法必须是公开的
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [JSFunction]
        public string TestFun(string test)
        {
            Thread.Sleep(5000);
            return "返回值：" + DateTime.Now.ToString();
        }
    }
}
