using Plugin.ValidationRules.Interfaces;
using System.Text.RegularExpressions;

namespace ValidationRulesTest.Validations
{
    public class EmailRule : IValidationRule<string>
    {

        public string ValidationMessage { get; set; } = "Email is not valid.";

        public bool Check(string value)
        {

            if (value == null)
            {
                ValidationMessage = "A email is required.";
                return false;
            }

            var str = value as string;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            if (!match.Success)
                ValidationMessage = "Email is not valid.";

            return match.Success;
        }
    }
}
