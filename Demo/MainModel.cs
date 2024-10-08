 using CPF.CefGlue;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPF_Cef
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
            return "返回值：" + test;
        }
    }
}
