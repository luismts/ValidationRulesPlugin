using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Plugin.ValidationRules.Interfaces;
using Plugin.ValidationRules.Rules;

namespace Plugin.ValidationRules.Extensions
{
    /// <summary>
    /// Builder for the <see cref="ValidatableList{TModel}"/> class.
    /// Provides extension methods for configuring the <see cref="ValidatableList{TModel}"/> class.
    /// </summary>
    public static class ValidatorList
    {
        /// <summary>
        /// Builds a <see cref="ValidatableList{TModel}"/> property
        /// of type <typeparamref name="TModel"/> using a fluent api.
        /// </summary>
        /// <typeparam name="TModel">The validatable model type.</typeparam>
        public static ValidatableList<TModel> Build<TModel>()
        {
            return new ValidatableList<TModel>();
        }

        public static ValidatableList<TModel> AddItemsSource<TModel>(this ValidatableList<TModel> validatable, IList<TModel> source)
        {
            validatable.ItemsSource = source;
            return validatable;
        }

        public static ValidatableList<TModel> WithRule<TModel>(this ValidatableList<TModel> validatable, IValidationRule<TModel> validation, string errorMessage = "")
        {
            if (errorMessage != "")
                validation.WithMessage(errorMessage);

            validatable.Validations.Add(validation);

            return validatable;
        }

        public static ValidatableList<TModel> WithRule<TModel>(this ValidatableList<TModel> validatable, params IValidationRule<TModel>[] validations)
        {
            validatable.Validations.AddRange(validations);
            return validatable;
        }

        public static ValidatableList<TModel> IsRequired<TModel>(this ValidatableList<TModel> validatable, string errorMessage = "")
        {

            if(typeof(TModel) == typeof(string))
                validatable.Validations.Add(new NotEmptyRule<TModel>("").WithMessage(errorMessage));

            if (typeof(TModel).IsClass)
                validatable.Validations.Add(new NotNullRule<TModel>().WithMessage(errorMessage));

            return validatable;
        }

        public static ValidatableList<TModel> Must<TModel>(this ValidatableList<TModel> validatable, Func<TModel, bool> predicate, string errorMessage = "")
        {
            validatable.Validations.Add(new FunctionRule<TModel>(predicate).WithMessage(errorMessage));
            return validatable;
        }

        public static ValidatableList<TModel> When<TModel>(this ValidatableList<TModel> validatable, Func<TModel, bool> predicate)
        {
            var validatableTemp = (Validatable<TModel>)validatable;
            validatable.HasWhenCondition(true);
            validatable.Validations.Insert(0, new WhenRule<TModel>(ref validatableTemp, predicate));
            return validatable;
        }

        #region Default Rules Extension

        /// <summary>
        /// Add the <see cref="CreditCardRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> IsCreditCard(
            this ValidatableList<string> validatable,
            string errorMessage = null)
        {
            validatable.Validations.Add(new CreditCardRule { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="EmailRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> IsEmail(
            this ValidatableList<string> validatable,
            string errorMessage = null)
        {
            validatable.Validations.Add(new EmailRule { ValidationMessage = errorMessage });

            return validatable;
        }


        #region Empty

        /// <summary>
        /// Add the <see cref="EmptyRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="EmptyRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsEmpty(
            this ValidatableList<object> validatable,
            object defaultValue = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new EmptyRule(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="NotEmptyRule{TModel}"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="NotEmptyRule{TModel}"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<TModel> IsNotEmpty<TModel>(
            this ValidatableList<TModel> validatable,
            TModel defaultValue = default,
            string errorMessage = null)
        {
            validatable.Validations.Add(new NotEmptyRule<TModel>(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion


        /// <summary>
        /// Add the <see cref="EnumRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="enumType">The enumerable type used in the <see cref="EnumRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsEnum(
            this ValidatableList<object> validatable,
            Type enumType = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new EnumRule(enumType) { ValidationMessage = errorMessage });

            return validatable;
        }


        #region Equal

        /// <summary>
        /// Add the <see cref="EqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="comparisonValue">Default value to be compare with.</param>
        /// <param name="equalityComparer">Default comparer object.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsEqual(
            this ValidatableList<object> validatable,
            object comparisonValue,
            IEqualityComparer equalityComparer = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new EqualRule(comparisonValue, equalityComparer) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="EqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="func">Delegate method used to get the value to compare with.</param>
        /// <param name="equalityComparer">Default comparer object.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsEqual(
            this ValidatableList<object> validatable,
            Func<object, object> func,
            IEqualityComparer equalityComparer = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new EqualRule(func, equalityComparer) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="NotEqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="comparisonValue">Default value to be compare with.</param>
        /// <param name="equalityComparer">Default comparer object.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsNotEqual(
            this ValidatableList<object> validatable,
            object comparisonValue,
            IEqualityComparer equalityComparer = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new NotEqualRule(comparisonValue, equalityComparer) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="NotEqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="func">Delegate method used to get the value to compare with.</param>
        /// <param name="equalityComparer">Default comparer object.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsNotEqual(
            this ValidatableList<object> validatable,
            Func<object, object> func,
            IEqualityComparer equalityComparer = null,
            string errorMessage = null)
        {
            validatable.Validations.Add(new NotEqualRule(func, equalityComparer) { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion

        #region Greater Than

        /// <summary>
        /// Add the <see cref="GreaterThanRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="GreaterThanRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<IComparable> IsGreaterThan(
            this ValidatableList<IComparable> validatable,
            IComparable defaultValue,
            string errorMessage = null)
        {
            validatable.Validations.Add(new GreaterThanRule(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="GreaterThanOrEqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="GreaterThanOrEqualRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<IComparable> IsGreaterThanOrEqual(
            this ValidatableList<IComparable> validatable,
            IComparable defaultValue,
            string errorMessage = null)
        {
            validatable.Validations.Add(new GreaterThanOrEqualRule(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion


        /// <summary>
        /// Add the <see cref="InclusiveBetweenRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="from">Minimum value comparer.</param>
        /// <param name="to">Maximum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<IComparable> IsInclusiveBetween(
            this ValidatableList<IComparable> validatable,
            IComparable from,
            IComparable to,
            string errorMessage = null)
        {
            validatable.Validations.Add(new InclusiveBetweenRule(from, to) { ValidationMessage = errorMessage });

            return validatable;
        }


        #region Less Than

        /// <summary>
        /// Add the <see cref="LessThanRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="LessThanRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<IComparable> IsLessThan(
            this ValidatableList<IComparable> validatable,
            IComparable defaultValue,
            string errorMessage = null)
        {
            validatable.Validations.Add(new LessThanRule(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="LessThanOrEqualRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="defaultValue">The default value used in the <see cref="LessThanOrEqualRule"/>
        /// validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<IComparable> IsLessThanOrEqual(
            this ValidatableList<IComparable> validatable,
            IComparable defaultValue,
            string errorMessage = null)
        {
            validatable.Validations.Add(new LessThanOrEqualRule(defaultValue) { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion

        #region Null

        /// <summary>
        /// Add the <see cref="NullRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsNull(
            this ValidatableList<object> validatable,
            string errorMessage = null)
        {
            validatable.Validations.Add(new NullRule { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="EmailRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<object> IsNotNull(
            this ValidatableList<object> validatable,
            string errorMessage = null)
        {
            validatable.Validations.Add(new NotNullRule { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion

        #region Length

        /// <summary>
        /// Add the <see cref="LengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="min">Minimum value comparer.</param>
        /// <param name="max">Maximum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithLengthRule(
            this ValidatableList<int> validatable,
            int min,
            int max,
            string errorMessage = null)
        {
            validatable.Validations.Add(new LengthRule(min, max) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="LengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="min">Delegate function that provides the minimum value comparer.</param>
        /// <param name="max">Delegate function that provides the maximum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithLengthRule(
            this ValidatableList<int> validatable,
            Func<object, int> min,
            Func<object, int> max,
            string errorMessage = null)
        {
            validatable.Validations.Add(new LengthRule(min, max) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="ExactLengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="length">Length use for the <see cref="ExactLengthRule"/> validator.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithExactLengthRule(
            this ValidatableList<int> validatable,
            int length,
            string errorMessage = null)
        {
            validatable.Validations.Add(new ExactLengthRule(length) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="LengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="length">Delegate function that provides the length value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithExactLengthRule(
            this ValidatableList<int> validatable,
            Func<object, int> length,
            string errorMessage = null)
        {
            validatable.Validations.Add(new ExactLengthRule(length) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="MaxLengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="max">Maximum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithMaxLengthRule(
            this ValidatableList<int> validatable,
            int max,
            string errorMessage = null)
        {
            validatable.Validations.Add(new MaxLengthRule(max) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="MaxLengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="max">Delegate function that provides the maximum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithMaxLengthRule(
            this ValidatableList<int> validatable,
            Func<object, int> max,
            string errorMessage = null)
        {
            validatable.Validations.Add(new MaxLengthRule(max) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="MinimumLengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="min">Minimum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithMinimumLengthRule(
            this ValidatableList<int> validatable,
            int min,
            string errorMessage = null)
        {
            validatable.Validations.Add(new MinimumLengthRule(min) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="MinimumLengthRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="min">Delegate function that provides the minimum value comparer.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<int> WithMinimumLengthRule(
            this ValidatableList<int> validatable,
            Func<object, int> min,
            string errorMessage = null)
        {
            validatable.Validations.Add(new MinimumLengthRule(min) { ValidationMessage = errorMessage });

            return validatable;
        }


        #endregion

        #region Regular Expression

        /// <summary>
        /// Add the <see cref="RegularExpressionRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="expression">The regular expression in <see cref="string"/> format
        /// used in the <see cref="RegularExpressionRule"/> validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> WithRegularExpression(
            this ValidatableList<string> validatable,
            string expression,
            string errorMessage = null)
        {
            validatable.Validations.Add(new RegularExpressionRule(expression) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="RegularExpressionRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="regex">The regular expression in <see cref="Regex"/> format
        /// used in the <see cref="RegularExpressionRule"/> validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> WithRegularExpression(
            this ValidatableList<string> validatable,
            Regex regex,
            string errorMessage = null)
        {
            validatable.Validations.Add(new RegularExpressionRule(regex) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="RegularExpressionRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="expression">The regular expression in <see cref="string"/> format
        /// used in the <see cref="RegularExpressionRule"/> validation.</param>
        /// <param name="options">The regular expression option.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> WithRegularExpression(
            this ValidatableList<string> validatable,
            string expression,
            RegexOptions options,
            string errorMessage = null)
        {
            validatable.Validations.Add(new RegularExpressionRule(expression, options) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="RegularExpressionRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="expressionFunc">The regular expression delegate function
        /// used in the <see cref="RegularExpressionRule"/> validation.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> WithRegularExpression(
            this ValidatableList<string> validatable,
            Func<object, string> expressionFunc,
            string errorMessage = null)
        {
            validatable.Validations.Add(new RegularExpressionRule(expressionFunc) { ValidationMessage = errorMessage });

            return validatable;
        }

        /// <summary>
        /// Add the <see cref="RegularExpressionRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="expression">The regular expression delegate function
        /// used in the <see cref="RegularExpressionRule"/> validation.</param>
        /// <param name="options">The regular expression option.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static ValidatableList<string> WithRegularExpression(
            this ValidatableList<string> validatable,
            Func<object, string> expression,
            RegexOptions options,
            string errorMessage = null)
        {
            validatable.Validations.Add(new RegularExpressionRule(expression, options) { ValidationMessage = errorMessage });

            return validatable;
        }

        #endregion

        #endregion
    }
}
