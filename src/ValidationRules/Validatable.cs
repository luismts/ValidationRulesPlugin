using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;

namespace Plugin.ValidationRules
{
    /// <summary>
    /// Provides a way for an object to be validated.
    /// </summary>
    /// <typeparam name="T">Type of the data to be validated</typeparam>
    public class Validatable<T> : ExtendedPropertyChanged, IValidity, IDisposable
    {
        bool _hasWhenCondition = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Validatable{T}"/> class.
        /// </summary>
        public Validatable()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Validatable{T}"/> class that takes a variable number of <see cref="IValidationRule{T}"/>.
        /// </summary>
        /// <param name="validations">List of <see cref="Validatable{T}"/> to be added.</param>
        public Validatable(params IValidationRule<T>[] validations) : this()
        {
            _validations.AddRange(validations);
        }

        #region Properties

        private readonly List<IValidationRule<T>> _validations;
        /// <summary>
        /// List of standards or validations you specify before the user can save the record
        /// </summary>
        public List<IValidationRule<T>> Validations => _validations;

        /// <summary>
        /// Reference formatter is attempting to format the property.
        /// </summary>
        public IValueFormatter<T> Formatter { private get; set; }

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

        private string _error;
        /// <summary>
        /// First or Default error of the main error list. 
        /// </summary>
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        private T _value;
        /// <summary>
        /// Reference value is attempting to assign to the property.
        /// </summary>
        public T Value
        {
            get => _value;
            set 
            {
                var oldValue = _value;
                T valueFormatted;

                if (Formatter != null)
                    valueFormatted = Formatter.Format(value);
                else
                    valueFormatted = value;

                SetProperty(ref _value, value);
                SetProperty(ref _valueFormatted, valueFormatted);
                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>() { OldValue = oldValue, NewValue = value });
            }
        }

        private T _valueFormatted;
        /// <summary>
        /// Formatted value.
        /// </summary>
        public T ValueFormatted
        {
            get => _valueFormatted;
            set
            {
                var oldValue = _valueFormatted;
                T newValue;

                if (Formatter != null)
                {
                    newValue = Formatter.Format(value);
                    _value = Formatter.UnFormat(value);
                }
                else
                {
                    newValue = value;
                    _value = value;
                }

                SetProperty(ref _value, value);
                SetProperty(ref _valueFormatted, newValue);
                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>() { OldValue = oldValue, NewValue = newValue });
            }
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

        private bool _hasErrors;
        /// <summary>
        /// The value indicating whether the validation has errors.
        /// </summary>
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }
        #endregion

        #region Commands
        private ICommand _validateCommand;
        public ICommand ValidateCommand => _validateCommand ?? (_validateCommand = new RelayCommand(_ => Validate()));
        #endregion

        #region Events
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;
        #endregion

        #region Methods
        /// <summary>
        /// Used to  perform the validations of the property
        /// </summary>
        /// <returns>Gets a value indicating whether the validation succeeded.</returns> 
        public bool Validate()
        {
            // Remove all elements from the list
            Errors.Clear();

            if (_hasWhenCondition)
            {
                var whenRule = _validations.Find(rule => rule.GetType() == typeof(Rules.WhenRule<T>));
                var isvalidCondition = whenRule?.Check(Value) ?? true;

                if (!isvalidCondition)                
                    return _isValid = false; // UI is not updated
            }            

            // Used to  perform the validations of the property
            var failedValidations = _validations.Where(v => !v.Check(Value)).ToList();
            IEnumerable<string> errors = failedValidations.Select(v => v.ValidationMessage);

            IsValid = !failedValidations.Any();
            Errors = errors.ToList();
            HasErrors = Errors.Any();

            // Gets a value indicating whether the validation succeeded.
            return this.IsValid;
        }

        internal void HasWhenCondition(bool has) => _hasWhenCondition = has;

        private void ReleaseManagedResources()
        {
            // Release resources
            _validations?.Clear();
            _errors?.Clear();
            _value = default(T);
            _validateCommand = null;
            ValueChanged = null;
        }
        #endregion

        #region IDispoble members

        // Track whether Dispose has been called. 
        private bool disposed = false;

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
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // someone want the deterministic release of all resources
                    //Let us release all the managed resources
                    ReleaseManagedResources();
                }
                else
                {
                    // Do nothing, no one asked a dispose, the object went out of
                    // scope and finalized is called so lets next round of GC 
                    // release these resources
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        #endregion

        ~Validatable()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
    }
}
