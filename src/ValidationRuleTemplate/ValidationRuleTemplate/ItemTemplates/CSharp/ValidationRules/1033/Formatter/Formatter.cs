using Plugin.ValidationRules.Interfaces;

namespace $rootnamespace$
{
    public class $safeitemname$<T> : IValueFormatter<T>
    {

        public bool Format(T value)
        {
            if (value == null)
            {
                return false;
            }

            // Your implementation goes here

            
            return true;
        }
    }

}
