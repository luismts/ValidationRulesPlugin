using System;
using System.Collections.Generic;
using Plugin.ValidationRules.Interfaces;

namespace Plugin.ValidationRules
{
    /// <summary>
    /// Provides a way for an object to be validated.
    /// </summary>
    /// <typeparam name="T">Type of the data to be validated</typeparam>
    public class ValidatableList<T> : Validatable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatableList{T}"/> class.
        /// </summary>
        public ValidatableList() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatableList{T}"/> class that takes a variable number of <see cref="IValidationRule{T}"/>.
        /// </summary>
        /// <param name="validations">List of <see cref="IValidationRule{T}"/> to be added.</param>
        public ValidatableList(params IValidationRule<T>[] validations) : base(validations)
        {
        }

        IList<T> _itemsSource;
        public IList<T> ItemsSource
        {
            get => _itemsSource;
            set => SetProperty(ref _itemsSource, value);
        }

        T _selectedItem;
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                var selectedIndex = ItemsSource?.IndexOf(value);
                var selectedValue = value;

                if (selectedIndex != null && selectedIndex >= 0)
                {
                    _selectedIndex = selectedIndex.Value;
                }
                else
                {
                    selectedValue = default(T);
                    _selectedIndex = -1;
                }
                
                Value = selectedValue;
                SetProperty(ref _selectedItem, selectedValue);
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                var selectedIndex = -1;

                if (ItemsSource?.Count > 0)
                {
                    var selectedItem = ItemsSource[value];

                    if (selectedItem != null)
                    {
                        selectedIndex = value;

                        _selectedItem = selectedItem;
                        Value = _selectedItem;
                    }
                    else
                    {
                        selectedIndex = -1;
                        _selectedItem = default(T);
                    }
                }                

                SetProperty(ref _selectedIndex, selectedIndex);
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler SelectedIndexChanged;

        ~ValidatableList()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
    }
}
