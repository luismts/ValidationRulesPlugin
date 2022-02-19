using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Formatters
{
    public class StringNumericFormatter : IValueFormatter<string>
    {
        string _oldValue;

        public string Format(string text)
        {
            if (text == _oldValue)
                return text;

            _oldValue = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                double _;

                if (double.TryParse(text, out _))
                    return text;
                else
                    return _oldValue;
            }

            return "";
        }

        public string UnFormat(string value)
        {
            return value;
        }
    }
}
