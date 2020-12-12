using Plugin.ValidationRules.Interfaces;
using System;

namespace Plugin.ValidationRules.Rules
{
    public class GreaterThanOrEqualRule : IValidationRule<IComparable> 
    {
        IComparable _valueToCompare;

        public GreaterThanOrEqualRule(IComparable value)
        {
            _valueToCompare = value;
        }

        public string ValidationMessage { get; set; }

        public bool Check(IComparable value)
        {
            if (value == null)
                return false;

            return value.CompareTo(_valueToCompare) >= 0;
        }
    }
}
