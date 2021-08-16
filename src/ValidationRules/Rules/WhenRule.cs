using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Rules
{
    internal class WhenRule<T> : IValidationRule<T>
    {
        readonly Validatable<T> _context;
        readonly Func<T, bool> _ruleFunc;
        List<IValidationRule<T>> _rules = new List<IValidationRule<T>>();

        public WhenRule(ref Validatable<T> context, Func<T, bool> ruleFunc)
        {
            _context = context;
            _ruleFunc = ruleFunc;
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var isValid = _ruleFunc(value);

            if (isValid)
            {
                if(_rules.Count >= _context?.Validations?.Count)
                    _context.Validations.AddRange(_rules.FindAll(rule => rule.GetType() != this.GetType()));
            }
            else
            {
                if (_rules.Count < _context?.Validations?.Count)
                {
                    _rules = new List<IValidationRule<T>>(_context.Validations);
                    _context.Validations.RemoveAll(rule => rule.GetType() != this.GetType());
                }                    
            }

            return true; // When rule now is an anonymous condition
        }
    }
}
