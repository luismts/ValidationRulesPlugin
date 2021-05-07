using Plugin.ValidationRules.Interfaces;
using System;
using System.Reflection;

namespace Plugin.ValidationRules.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Capitalize the first character and add a space before each capitalized letter (except the first character).
        /// </summary>
        /// <param name="the_string"></param>
        /// <returns></returns>
        public static string ToCapitalizeCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }

        public static Model MapValidator<Model, Validator>(this Validator validator) where Model : new()
        {
            if (validator == null)
                return default(Model);

            Model newModel = new Model();

            Type modelObjectType = newModel.GetType();
            PropertyInfo[] modelPropList = modelObjectType.GetProperties();

            Type validatorType = validator.GetType();
            PropertyInfo[] validatorPropList = validatorType.GetProperties();

            foreach (PropertyInfo validatorPropInfo in validatorPropList)
            {
                foreach (PropertyInfo modelPropInfo in modelPropList)
                {
                    if (modelPropInfo.Name == validatorPropInfo.Name)
                    {
                        try
                        {
                            PropertyInfo validatorProp = validatorPropInfo.PropertyType.GetProperty(nameof(Validatable<string>.Value));

                            if (validatorProp == null)
                                break;

                            var validatorPropValue = validatorPropInfo.GetValue(validator); // Not working directly
                            var propValue = validatorProp.GetValue(validatorPropValue, null);

                            modelPropInfo.SetValue(newModel, propValue, null);
                        }
                        catch (Exception) { }

                        break;
                    }
                }
            }

            return newModel;
        }

        public static IValidationRule<T> WithMessage<T>(this IValidationRule<T> rule, string message)
        {
            if(message?.Length > 0)
                rule.ValidationMessage = message;

            return rule;
        }

        public static IValidationRule<T> WithMessage<T>(this IValidationRule<T> rule, Func<string> messageProvider)
        {
            var message = messageProvider();

            if (message?.Length > 0)
                rule.ValidationMessage = message;

            return rule;
        }
    }
}
