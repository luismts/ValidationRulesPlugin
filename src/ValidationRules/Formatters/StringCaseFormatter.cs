using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;

namespace Plugin.ValidationRules.Formatters
{
    public enum StringCases { Upper, Lower, Capitalize }

    public class StringCaseFormatter : IValueFormatter<string>
    {
        StringCases _stringCase;

        public StringCaseFormatter(StringCases stringCase)
        {
            _stringCase = stringCase;
        }

        public string Format(string stringValue)
        {
            if (_stringCase == StringCases.Upper)            
                return stringValue.ToUpper();            

            if (_stringCase == StringCases.Lower)            
                return stringValue.ToLower();

            if (_stringCase == StringCases.Capitalize)
                return stringValue.ToCapitalizeCase();

            return stringValue;
        }

        public string UnFormat(string stringValue)
        {
            return stringValue.ToLower();
        }
    }
}
