﻿using CPF.CefGlue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPF.CefGlue.JSExtenstions
{
    internal abstract class CefValueWrapper
    {
        public abstract void SetNull();
        public abstract void SetBool(bool value);
        public abstract void SetInt(int value);
        public abstract void SetDouble(double value);
        public abstract void SetString(string value);
        public abstract void SetBinary(CefBinaryValue value);
        public abstract void SetList(CefListValue value);
        public abstract void SetDictionary(CefDictionaryValue value);

        public abstract bool GetBool();
        public abstract int GetInt();
        public abstract double GetDouble();
        public abstract string GetString();
        public abstract CefBinaryValue GetBinary();
        public abstract CefListValue GetList();
        public abstract CefDictionaryValue GetDictionary();

        public abstract CefValueType GetValueType();
    }

    internal abstract class CefValueWrapper<TIndex, TCefContainerUnderlyingType> : CefValueWrapper
    {
        protected readonly TIndex _index;
        protected readonly TCefContainerUnderlyingType _container;

        public CefValueWrapper(TCefContainerUnderlyingType container, TIndex index)
        {
            _index = index;
            _container = container;
        }
    }
}
