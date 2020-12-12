using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Rules
{
    public class LessThanRule : IValidationRule<IComparable> 
    {
        IComparable _valueToCompare;

        public LessThanRule(IComparable value)
        {
            _valueToCompare = value;
        }

        public string ValidationMessage { get; set; }

        public bool Check(IComparable value)
        {
            if (value == null)
                return false;

            return value.CompareTo(_valueToCompare) < 0;
        }
    }
}
