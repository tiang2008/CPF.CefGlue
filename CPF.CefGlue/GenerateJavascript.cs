using System;
using System.Collections.Generic;
using System.Text;

namespace CPF.CefGlue
{
    public class GenerateJavascript
    {
        string _extensionName = string.Empty;
        string _functionName = string.Empty;
        Dictionary<string, string[]> _methodName = new Dictionary<string, string[]>();

        //
        Dictionary<string, string> _getterPropertyName = new Dictionary<string, string>();

        // 保存setter 名称 和参数。 与 _setterPropertyArgs 成对出现。
        Dictionary<string, string> _setterPropertyName = new Dictionary<string, string>();
        Dictionary<string, string[]> _setterPropertyArgs = new Dictionary<string, string[]>();

        //自定义javascript代码
        List<string> _customJavascript = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extensionName">
        /// 插件方法作用域
        /// e.g: window.plugin.test
        /// 其中 plugin 为作用域. 如不设置，添加的js方法在window下.
        /// </param>
        /// <param name="functionName">
        /// 
        /// </param>
        public GenerateJavascript(string extensionName, string functionName)
        {
            _extensionName = extensionName;
            _functionName = functionName;
        }

        /// <summary>
        /// 增加方法
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="args">参数名：arg0,arg1,...arg20 (固定写死)</param>
        public void AddMethod(string methodName, params string[] args)
        {
            //检测是否存在改方法
            if (_methodName.ContainsKey(methodName))
                return;
            _methodName.Add(methodName, args);
        }

        /// <summary>
        /// 增加Getter属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="executeName">执行名称,CEF handler中execute的Name参数同名</param>
        public void AddGetProperty(string propertyName, string executeName)
        {
            if (_getterPropertyName.ContainsKey(propertyName))
                return;

            _getterPropertyName.Add(propertyName, executeName);
        }

        /// <summary>
        /// 增加Setter属性
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="executeName">执行名称,CEF handler中execute的Name参数同名</param>
        /// <param name="args">参数名：arg0,arg1,...arg20 (固定写死)</param>
        public void AddSetProperty(string propertyName, string executeName, params string[] args)
        {
            if (_setterPropertyName.ContainsKey(propertyName) || _setterPropertyArgs.ContainsKey(propertyName))
                return;

            _setterPropertyName.Add(propertyName, executeName);
            _setterPropertyArgs.Add(propertyName, args);
        }

        /// <summary>
        /// 增加自定义的javascript代码。
        /// </summary>
        /// <param name="javascriptCode">注意：functionName一定要大写。
        ///  例如：  TEST.__defineSetter__('hello', function(b) {
        ///  native function sethello();sethello(b);});</param>
        public void AddCustomJavascript(string javascriptCode)
        {
            _customJavascript.Add(javascriptCode);
        }

        /// <summary>
        /// 组装本地JS的一个过程
        /// </summary>
        /// <returns>返回CEF识别的javascript</returns>
        public string Create()
        {
            //System.Threading.Thread.Sleep(3000);
            if (string.IsNullOrEmpty(_functionName)) throw new Exception("JavascriptFull函数名不能为空！");

            StringBuilder sb = new StringBuilder();

            //头部
            if (!string.IsNullOrEmpty(_extensionName))
            {
                sb.Append(string.Format("if (!{0}) var {0} = {{ }}; ", _extensionName));
            }
            if (!string.IsNullOrEmpty(_functionName))
            {
                sb.Append(string.Format("var {0} = function () {{ }}; ", _functionName.ToUpper()));
                if (!string.IsNullOrEmpty(_extensionName))
                    sb.Append(string.Format("if (!{0}.{1}) {0}.{1} = {2};", _extensionName, _functionName, _functionName.ToUpper()));
                else
                    sb.Append(string.Format("if (!{0}) var {0} = {1};", _functionName, _functionName.ToUpper()));
            }

            //开始
            sb.Append("(function () {");

            //方法
            foreach (KeyValuePair<string, string[]> item in _methodName)
            {
                sb.Append(string.Format("{0}.{1} = function ({2}) {{", _functionName.ToUpper(), item.Key, string.Join(",", item.Value)));
                sb.Append(string.Format("native function {0}({1}); return {0}({1});", item.Key, string.Join(",", item.Value)));
                sb.Append("};");
            }

            //GET属性
            foreach (KeyValuePair<string, string> item in _getterPropertyName)
            {
                sb.Append(string.Format("{0}.__defineGetter__('{1}', function () {{", _functionName.ToUpper(), item.Key));
                sb.Append(string.Format("native function {0}(); return {0}();", item.Value));
                sb.Append("});");
            }

            //SET属性
            if (_setterPropertyArgs.Count == _setterPropertyName.Count)
            {
                foreach (KeyValuePair<string, string> item in _setterPropertyName)
                {
                    sb.Append(string.Format("{0}.__defineSetter__('{1}', function ({2}) {{", _functionName.ToUpper(), item.Key, string.Join(",", _setterPropertyArgs[item.Key])));
                    sb.Append(string.Format("native function {0}({1}); return {0}({1});", item.Value, string.Join(",", _setterPropertyArgs[item.Key])));
                    sb.Append("});");
                }
            }

            //自定义javascript
            for (int i = 0; i < _customJavascript.Count; i++)
            {
                sb.Append(_customJavascript[i]);
            }

            //结尾
            sb.Append("})();");

            return sb.ToString();
        }
    }
}
