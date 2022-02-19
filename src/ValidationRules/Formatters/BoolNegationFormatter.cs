using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Formatters
{
    public class BoolNegationFormatter : IValueFormatter<bool>
    {
        public bool TrueValue { get; set; }
        public bool FalseValue { get; set; }
        public bool IsInverted { get; set; }

        public bool Format(bool value)
        {
            var returnValue = this.FalseValue;

            if (value is bool boolValue)
            {
                if (this.IsInverted)
                {
                    returnValue = boolValue ? this.FalseValue : this.TrueValue;
                }
                else
                {
                    returnValue = boolValue ? this.TrueValue : this.FalseValue;
                }
            }

            return returnValue;
        }

        public bool UnFormat(bool value)
        {
            var returnValue = this.FalseValue;

            if (value is bool boolValue)
            {
                if (this.IsInverted)
                {
                    returnValue = boolValue ? this.TrueValue : this.FalseValue;
                }
                else
                {
                    returnValue = boolValue ? this.FalseValue : this.TrueValue;
                }
            }

            return returnValue;
        }
    }

    public class InverseBoolFormatter : BoolNegationFormatter
    {
    }
}
