using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace Plugin.ValidationRules.Rules
{
    public class EmptyRule : IValidationRule<object>
    {
		readonly object _defaultValueForType;

		public EmptyRule(object defaultValueForType)
        {
			_defaultValueForType = defaultValueForType;
		}

        public string ValidationMessage { get; set; }

        public bool Check(object value)
        {
			switch (value)
			{
				case null:
				case string s when string.IsNullOrWhiteSpace(s):
				case ICollection c when c.Count == 0:
				case Array a when a.Length == 0:
				case IEnumerable e when !e.Cast<object>().Any():
					return true;
			}

			if (Equals(value, _defaultValueForType))			
				return true;
			
			return false;
		}
    }
}
