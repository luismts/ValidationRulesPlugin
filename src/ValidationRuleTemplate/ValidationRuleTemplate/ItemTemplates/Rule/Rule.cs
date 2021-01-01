using Plugin.ValidationRules.Interfaces;

namespace $rootnamespace$
{
    public class $safeitemname$<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
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
