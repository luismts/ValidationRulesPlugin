using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Extensions
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }
}
