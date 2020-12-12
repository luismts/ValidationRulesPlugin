using System;
using System.Globalization;

namespace Plugin.ValidationRules.Interfaces
{
    public interface IRuleValueConverter<T>
    {
        T Convert(T value);
    }
}
