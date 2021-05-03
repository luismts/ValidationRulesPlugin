using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace Plugin.ValidationRules.Rules
{
    public class NotNullRule : IValidationRule<object>
    {
        public string ValidationMessage { get; set; }

        public bool Check(object value)
        {
			return !(value == null);
		}
    }

    public class NotNullRule<TModel> : IValidationRule<TModel>
    {
        public string ValidationMessage { get; set; }

        public bool Check(TModel value)
        {
            return !(value == null);
        }
    }
}
