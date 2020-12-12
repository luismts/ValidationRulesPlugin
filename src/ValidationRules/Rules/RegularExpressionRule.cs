using Plugin.ValidationRules.Interfaces;
using System;

namespace Plugin.ValidationRules.Rules
{
    using System.Text.RegularExpressions;

    public class RegularExpressionRule : IValidationRule<string>
    {
        readonly Func<object, Regex> _regexFunc;

        public RegularExpressionRule(string expression)
        {
            Expression = expression;

            var regex = CreateRegex(expression);
            _regexFunc = x => regex;
        }

        public RegularExpressionRule(Regex regex)
        {
            Expression = regex.ToString();
            _regexFunc = x => regex;
        }

        public RegularExpressionRule(string expression, RegexOptions options)
        {
            Expression = expression;
            var regex = CreateRegex(expression, options);
            _regexFunc = x => regex;
        }

        public RegularExpressionRule(Func<object, string> expressionFunc)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x));
        }

        public RegularExpressionRule(Func<object, string> expression, RegexOptions options)
        {
            _regexFunc = x => CreateRegex(expression(x), options);
        }


        public string Expression { get; }
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            var regex = _regexFunc(value);

            if (regex != null && value != null && !regex.IsMatch((string)value))
                return false;
            
            return true;
        }

        private static Regex CreateRegex(string expression, RegexOptions options = RegexOptions.None)
        {
            return new Regex(expression, options, TimeSpan.FromSeconds(2.0));
        }

    }
}
