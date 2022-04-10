using Plugin.ValidationRules.Interfaces;
using System;
using ValidationRulesTest.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace ValidationRulesTest.Validations
{
    public class PasswordRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if (value == null)
            {
                ValidationMessage = "A password is required.";
                return false;
            }

            if (!char.IsLetter(value[0]))
            {
                ValidationMessage = "First character must be a letter.";
                return false;
            }

            if (!char.IsUpper(value[0]))
            {
                ValidationMessage = "First letter must be Capitalize.";
                return false;
            }

            if (value.Length < 8)
            {
                ValidationMessage = "Password length must be 8 characters minimum.";
                return false;
            }

            if (!value.Any(char.IsDigit))
            {
                ValidationMessage = "Your password must contain numbers.";
                return false;
            }

            if (!value.Any(char.IsSymbol) && !value.Any(char.IsPunctuation))
            {
                ValidationMessage = "Your password must contain symbols.";
                return false;
            }

            return true; // Yupiii ! We did !!!
        }
    }

}
