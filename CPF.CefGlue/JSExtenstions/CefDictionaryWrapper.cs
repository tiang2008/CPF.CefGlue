﻿using CPF.CefGlue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPF.CefGlue.JSExtenstions
{
    internal class CefDictionaryWrapper : CefValueWrapper<string, CefDictionaryValue>
    {
        public CefDictionaryWrapper(CefDictionaryValue container, string index) : base(container, index)
        {
        }

        public override void SetNull()
        {
            _container.SetNull(_index);
        }

        public override void SetBool(bool value)
        {
            _container.SetBool(_index, value);
        }

        public override void SetInt(int value)
        {
            _container.SetInt(_index, value);
        }

        public override void SetDouble(double value)
        {
            _container.SetDouble(_index, value);
        }

        public override void SetString(string value)
        {
            _container.SetString(_index, value);
        }

        public override void SetBinary(CefBinaryValue value)
        {
            _container.SetBinary(_index, value);
        }

        public override void SetList(CefListValue value)
        {
            _container.SetList(_index, value);
        }

        public override void SetDictionary(CefDictionaryValue value)
        {
            _container.SetDictionary(_index, value);
        }

        public override bool GetBool()
        {
            return _container.GetBool(_index);
        }

        public override int GetInt()
        {
            return _container.GetInt(_index);
        }

        public override double GetDouble()
        {
            return _container.GetDouble(_index);
        }

        public override string GetString()
        {
            return _container.GetString(_index);
        }

        public override CefBinaryValue GetBinary()
        {
            return (CefBinaryValue)_container.GetBinary(_index);
        }

        public override CefListValue GetList()
        {
            return (CefListValue)_container.GetList(_index);
        }

        public override CefDictionaryValue GetDictionary()
        {
            return (CefDictionaryValue)_container.GetDictionary(_index);
        }

        public override CefValueType GetValueType()
        {
            return _container.GetValueType(_index);
        }
    }
}
