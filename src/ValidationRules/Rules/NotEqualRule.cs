using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections;

namespace Plugin.ValidationRules.Rules
{
    public class NotEqualRule : IValidationRule<object>
    {
        private readonly IEqualityComparer _comparer;
        private readonly Func<object, object> _func;

        public NotEqualRule(object comparisonValue, IEqualityComparer equalityComparer = null)
        {
            ValueToCompare = comparisonValue;
            _comparer = equalityComparer;
        }

        public NotEqualRule(Func<object, object> func, IEqualityComparer equalityComparer = null)
        {
            _func = func;
            _comparer = equalityComparer;
        }

        public object ValueToCompare { get; set; }
        public string ValidationMessage { get; set; }

        public bool Check(object value)
        {
            var comparisonValue = GetComparisonValue(value);
            bool success = !Compare(comparisonValue, value);        

            return success;
        }

        private object GetComparisonValue(object value)
        {
            if (_func != null)            
                return _func(value);            

            return ValueToCompare;
        }

        protected bool Compare(object comparisonValue, object propertyValue)
        {
            if (_comparer != null)            
                return _comparer.Equals(comparisonValue, propertyValue);
            
            return Equals(comparisonValue, propertyValue);
        }

    }
}
