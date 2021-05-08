using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Rules
{
    internal class FunctionRule<T> : IValidationRule<T>
    {
        readonly Func<T, bool> _ruleFunc;

        public FunctionRule(Func<T, bool> ruleFunc)
        {
            _ruleFunc = ruleFunc;
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            return _ruleFunc(value);
        }
    }
}
