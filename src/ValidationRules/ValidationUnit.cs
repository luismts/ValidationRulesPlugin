using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.ValidationRules
{
    /// <summary>
    /// Provides a way for a List of <see cref="Validatable{T}"/> to be validated.
    /// </summary>
    public class ValidationUnit : ExtendedPropertyChanged, IValidity
    {
        private object[] _objects;

        public ValidationUnit()
        {
            _isValid = true;
            _errors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationUnit"/> class.
        /// </summary>
        /// <param name="objects">List of <see cref="Validatable{T}"/> to be validated</param>
        public ValidationUnit(params object[] objects) : this()
        {
            _objects = objects;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationUnit"/> class.
        /// </summary>
        /// <param name="objects">List of <see cref="Validatable{T}"/> to be validated</param>
        public ValidationUnit(IEnumerable<IValidity> objects) : this()
        {
            _objects = objects.ToArray();
        }

        #region Properties
        private bool _isValid;
        /// <summary>
        /// The value indicating whether the validation succeeded.
        /// </summary>
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }
        private List<string> _errors;
        /// <summary>
        /// List of errors users have before can save the record
        /// </summary>
        public List<string> Errors
        {
            get => _errors;
            set
            {
                Error = value?.Count > 0 ? value.FirstOrDefault() : string.Empty;
                SetProperty(ref _errors, value);
            }
        }

        private bool _hasErrors;
        /// <summary>
        /// The value indicating whether the validation has errors.
        /// </summary>
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        private string _error;
        /// <summary>
        /// First or Default error of the main error list. 
        /// </summary>
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        } 
        #endregion

        /// <summary>
        /// Used to  perform the validations of the list of <see cref="Validatable{T}"/>
        /// </summary>
        /// <returns>Gets a value indicating whether the validation succeeded.</returns> 
        public bool Validate()
        {
            // Remove all elements from the list
            Errors.Clear();
            List<string> errors = new();

            foreach (var obj in _objects)
            {
                if (obj is IValidity validatableObj)
                {
                    // Used to  perform the validations of the property
                    var isValid = validatableObj.Validate();

                    // If it's valid...
                    if (isValid)
                        continue; // Continue to the next iteration

                    if(validatableObj.Errors.Count > 0)
                        errors.AddRange(validatableObj.Errors);

                    Errors = errors.ToList();
                    HasErrors = Errors.Any();
                }
                
                return IsValid = false;   // Returns IsValid property set to false
            }

            // If all the properties are valid...
            return IsValid = true; // Returns IsValid property set to true
        }
        
    }
}
