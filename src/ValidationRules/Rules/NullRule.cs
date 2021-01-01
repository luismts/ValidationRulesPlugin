using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace Plugin.ValidationRules.Rules
{
    public class NullRule : IValidationRule<object>
    {
        public string ValidationMessage { get; set; }

        public bool Check(object value)
        {
			return value == null;
		}
    }
}
