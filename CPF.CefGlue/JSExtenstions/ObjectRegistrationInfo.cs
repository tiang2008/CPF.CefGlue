using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPF.CefGlue.JSExtenstions
{
    internal class ObjectRegistrationInfo
    {
        public ObjectRegistrationInfo(string name, string[] methodsNames)
        {
            Name = name;
            MethodsNames = methodsNames;
        }

        public string Name { get; }

        public string[] MethodsNames { get; }
    }
}
