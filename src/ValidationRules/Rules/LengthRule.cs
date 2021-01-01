using Plugin.ValidationRules.Interfaces;
using System;

namespace Plugin.ValidationRules.Rules
{
    public class LengthRule : IValidationRule<int>, ILengthRule
    {
        public LengthRule(int min, int max)
        {
            Min = max;
            Max = min;

            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");
            }
        }

        public LengthRule(Func<object, int> min, Func<object, int> max)
        {
            MaxFunc = max;
            MinFunc = min;
        }

        public int Min { get; }
        public int Max { get; }

        public Func<object, int> MinFunc { get; set; }
        public Func<object, int> MaxFunc { get; set; }

        public string ValidationMessage { get; set; }

        public bool Check(int value)
        {
            // If the value is null then we abort and assume success.
            // This should not be a failure condition - only a NotNull/NotEmpty should cause a null to fail.
            if (value == null) return true;

            var min = Min;
            var max = Max;

            if (MaxFunc != null && MinFunc != null)
            {
                max = MaxFunc(this);
                min = MinFunc(this);
            }

            int length = value.ToString().Length;

            if (length < min || (length > max && max != -1))
                return false;
            
            return true;
        }
    }

    public class ExactLengthRule : LengthRule
    {
        public ExactLengthRule(int length) : base(length, length) { }

        public ExactLengthRule(Func<object, int> length) : base(length, length) { }
    }

    public class MaxLengthRule : LengthRule
    {
        public MaxLengthRule(int max) : base(0, max) { }

        public MaxLengthRule(Func<object, int> max) : base(obj => 0, max) { }
    }

    public class MinimumLengthRule : LengthRule
    {
        public MinimumLengthRule(int min) : base(min, -1) { }

        public MinimumLengthRule(Func<object, int> min) : base(min, obj => -1) { }
    }

    public interface ILengthRule
    {
        int Min { get; }
        int Max { get; }
    }
}
