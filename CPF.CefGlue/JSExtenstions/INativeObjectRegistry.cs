using System;
using System.Threading.Tasks;

namespace CPF.CefGlue.JSExtenstions
{
    internal interface INativeObjectRegistry
    {
        Task<bool> Bind(string objName);
        void Unbind(string objName);
    }
}
