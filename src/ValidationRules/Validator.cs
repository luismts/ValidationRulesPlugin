using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plugin.ValidationRules
{
    public class Validator<TModel> : IMapperValidator<TModel> where TModel : class
    {
        public ValidationUnit ValidationUnit { get; set; }

        public void InitUnit()
        {
            var validatables = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var values = new List<object>(); 

            foreach (var item in validatables)
            {
                if (item.PropertyType.IsSubclassOf(typeof(IValidity)))
                    continue;

                var value = item.GetValue(this);

                if(value != null)
                    values.Add(value);
            }

            ValidationUnit = new ValidationUnit(values.ToArray());
        }


        /// <summary>
        /// Builds a <see cref="Validatable{T}"/> property
        /// of type <typeparamref name="T"/> using a fluent api.
        /// </summary>
        /// <typeparam name="T">The validatable model type.</typeparam>
        public Validatable<T> Build<T>()
        {
            return Validator.Build<T>();
        }


        public virtual bool Validate()
        {
            if (ValidationUnit == null)
                throw new NotImplementedException("Validation unit is not initialized.");

            return ValidationUnit.Validate();
        }

        public virtual TModel Map()
        {
            throw new NotImplementedException("Model mapping not implemented.");
        }

    }
}
