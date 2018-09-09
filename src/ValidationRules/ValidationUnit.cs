using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;
using System;

namespace Plugin.ValidationRules
{
    /// <summary>
    /// Provides a way for a List of <see cref="ValidatableObject{T}"/> to be validated.
    /// </summary>
    public class ValidationUnit : ExtendedPropertyChanged, IValidity
    {
        private object[] _objects;


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationUnit"/> class.
        /// </summary>
        /// <param name="objects">List of <see cref="ValidatableObject{T}"/> to be validated</param>
        public ValidationUnit(params object[] objects)
        {
            _objects = objects;
        }


        private bool _isValid;
        /// <summary>
        /// The value indicating whether the validation succeeded.
        /// </summary>
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }


        /// <summary>
        /// Used to  perform the validations of the list of <see cref="ValidatableObject{T}"/>
        /// </summary>
        /// <returns>Gets a value indicating whether the validation succeeded.</returns> 
        public bool Validate()
        {
            foreach (var obj in _objects)
            {
                if (obj is IValidity validatableObj)
                {
                    // Used to  perform the validations of the property
                    var isValid = validatableObj.Validate();

                    // If it's valid...
                    if (isValid)
                        continue; // Continue to the next iteration
                }
                
                return IsValid = false;   // Returns IsValid property set to false
            }

            // If all the properties are valid...
            return IsValid = true; // Returns IsValid property set to true
        }
        
    }
}
