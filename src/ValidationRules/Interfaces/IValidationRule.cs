
namespace Plugin.ValidationRules.Interfaces
{
    /// <summary>
    /// Verify that the data a user enters in a record meets the standards you specify before the user can save the record.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// The validation error message that will be displayed if validation fails.
        /// </summary>
        string ValidationMessage { get; set; }

        /// <summary>
        /// Used to perform the required validation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Check(T value);
    }
    
}
