using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace Plugin.ValidationRules.Rules
{
    public class NotEmptyRule<T> : IValidationRule<T>
    {
		readonly object _defaultValueForType;

		public NotEmptyRule(object defaultValueForType)
        {
			_defaultValueForType = defaultValueForType;
		}

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
			switch (value)
			{
				case null:
				case string s when string.IsNullOrWhiteSpace(s):
				case ICollection c when c.Count == 0:
				case Array a when a.Length == 0:
				case IEnumerable e when !e.Cast<object>().Any():
					return false;
			}

			if (Equals(value, _defaultValueForType))			
				return false;
			
			return true;
		}
    }
}
