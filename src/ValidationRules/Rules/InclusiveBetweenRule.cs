using Plugin.ValidationRules.Interfaces;
using System;

namespace Plugin.ValidationRules.Rules
{
    public class InclusiveBetweenRule : IValidationRule<IComparable> 
    {
        public InclusiveBetweenRule(IComparable from, IComparable to)
        {
            To = to;
            From = from;

            if (to.CompareTo(from) == -1)
            {
                throw new ArgumentOutOfRangeException(nameof(to), "To should be larger than from.");
            }
        }

        public IComparable From { get; }
        public IComparable To { get; }

        public string ValidationMessage { get; set; }

        public bool Check(IComparable value)
        {
            // If the value is null then we abort and assume success.
            // This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
            if (value == null) return true;

            if (value.CompareTo(From) < 0 || value.CompareTo(To) > 0)            
                return false;
            
            return true;
        }
    }
}
