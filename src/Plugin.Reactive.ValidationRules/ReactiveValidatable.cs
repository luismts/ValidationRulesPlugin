using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.ValidationRules.Interfaces;
using ReactiveUI;

namespace Plugin.Reactive.ValidationRules
{
    /// <summary>
    /// A validatable instance, that leverages the power of
    /// reactive programming to automatically validate its value when
    /// a change is made to it, and generates error messages based on
    /// the validation rules it is given.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReactiveValidatable<T> : ReactiveObject, IValidity, IDisposable
    {
        public List<IValidationRule<T>> _validations;
        protected IDisposable _valueDisposable;
        private bool _disposed;
        
        private T _value;
        /// <summary>
        /// The value to be validated.
        /// NOTE: This object automatically validates
        /// this value when it changes. No check has to
        /// be done by external code.
        /// </summary>
        public T Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        private bool _isValid;
        /// <summary>
        /// Determines if the Value is valid or not.
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { this.RaiseAndSetIfChanged(ref _isValid, value); }
        }

        private string _errorText;
        /// <summary>
        /// A simple error message displayed by
        /// the UI when validation fails.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorText; }
            set { this.RaiseAndSetIfChanged(ref _errorText, value); }
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
                this.RaiseAndSetIfChanged(ref _errors, value);
            }
        }
        private bool _hasErrors;
        /// <summary>
        /// The value indicating whether the validation has errors.
        /// </summary>
        public bool HasErrors
        {
            get => _hasErrors;
            set => this.RaiseAndSetIfChanged(ref _hasErrors, value);
        }
        public string Error { get; set; }
        /// <summary>
        /// Formats the list of error messages
        /// into a simple error message understandable
        /// in one line.
        /// </summary>
        public Func<List<string>, string> ErrorMessageFormatter { get; set; }

        /// <summary>
        /// Create an instance of a reactive validatable object.
        /// </summary>
        /// <param name="value">The value to be watched and validated.</param>
        public ReactiveValidatable(T value)
        {
            ErrorMessageFormatter = (errorMessages) =>
            {
                if (errorMessages != null && errorMessages.Any())
                {
                    var builder = new StringBuilder();
                    for (int i = 0; i < errorMessages.Count;i++)
                    {
                        builder.Append(errorMessages[i]);
                        if (i < errorMessages.Count - 1)
                        {
                            builder.Append(Environment.NewLine);
                        }
                    }

                    return builder.ToString();
                }
                
                return string.Empty;
            };
            Value = value;
            _validations = new List<IValidationRule<T>>();
            Errors = new List<string>();
            _valueDisposable = this.WhenAnyValue(obj => obj.Value).Subscribe((val) =>
            {
                Validate();
            });
        }

        /// <summary>
        /// Create an instance of a reactive validatable object.
        /// </summary>
        /// <param name="value">The value to be watched and validated.</param>
        /// <param name="validations">The validation rules that the value should be checked for.</param>
        public ReactiveValidatable(T value, params IValidationRule<T>[] validations) : this(value)
        {
            _validations.AddRange(validations);
        }

        /// <summary>
        /// Create an instance of a reactive validatable object.
        /// </summary>
        /// <param name="value">The value to be watched and validated.</param>
        /// <param name="errorMessageFormatter">A function to format the list of errors to be displayed in a single string on the UI.</param>
        /// <param name="validations">The validation rules that the value should be checked for.</param>
        public ReactiveValidatable(T value,  Func<IEnumerable<string>, string> errorMessageFormatter,
            params IValidationRule<T>[] validations) : this(value, validations)
        {
            ErrorMessageFormatter = errorMessageFormatter;
        }

        /// <summary>
        /// Validate the value of this property
        /// each time it changes.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            IsValid = TryValidate(Value);
            if (Errors.Any())
            {
                HasErrors = true;
                ErrorMessage = ErrorMessageFormatter(Errors);
            }
            else
            {
                ErrorMessage = string.Empty;
            }
            return IsValid;
        }

        /// <summary>
        /// Tells if the object is valid without 
        /// setting its state 
        /// </summary>
        /// <returns></returns>
        public bool TryValidate(T val)
        {
            Errors.Clear();
            Errors = new List<string>(_validations.Where(v => !v.Check(val))
                .Select(v => v.ValidationMessage));

            return !Errors.Any();
        }
        
        #region Disposing

        private void ReleaseManagedResources()
        {
            // Release resources
            _validations?.Clear();
            _errors?.Clear();
            _value = default(T);
            _valueDisposable.Dispose();
        }
        
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ReleaseManagedResources();
                }
                
                _disposed = true;
            }
        }

        ~ReactiveValidatable()
        {
            Dispose(false);
        }
        #endregion

    }
}
