using System;
using System.Globalization;

namespace Plugin.ValidationRules.Interfaces
{
    public interface IValueFormatter<T>
    {
        T Format(T value);
        T UnFormat(T value);
    }
}
