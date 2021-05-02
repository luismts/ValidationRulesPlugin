using Plugin.ValidationRules.Interfaces;

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
}
