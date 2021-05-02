using Plugin.ValidationRules.Interfaces;
using Plugin.ValidationRules.Properties;

namespace Plugin.ValidationRules.Rules
{
    public class EmailRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = ValidationMessages.EmailRuleMessage;

        public bool Check(string value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string valueAsString))
            {
                return false;
            }

            // only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            int index = valueAsString.IndexOf('@');

            return
                index > 0 &&
                index != valueAsString.Length - 1 &&
                index == valueAsString.LastIndexOf('@');
        }
    }
}
