using System;
using System.Collections;
using System.Text.RegularExpressions;
using Plugin.ValidationRules.Interfaces;
using Plugin.ValidationRules.Rules;

namespace Plugin.ValidationRules.Extensions
{
    /// <summary>
    /// Builder for the <see cref="Validatable{TModel}"/> class.
    /// Provides extension methods for configuring the <see cref="Validatable{TModel}"/> class.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Builds a <see cref="Validatable{TModel}"/> property
        /// of type <typeparamref name="TModel"/> using a fluent api.
        /// </summary>
        /// <typeparam name="TModel">The validatable model type.</typeparam>
        public static Validatable<TModel> Build<TModel>()
        {
            return new Validatable<TModel>();
        }

        public static Validatable<TModel> WithRule<TModel>(this Validatable<TModel> validatable, IValidationRule<TModel> validation, string errorMessage = "")
        {
            if (errorMessage != "")
                validation.WithMessage(errorMessage);

            validatable.Validations.Add(validation);

            return validatable;
        }

        public static Validatable<TModel> WithRule<TModel>(this Validatable<TModel> validatable, params IValidationRule<TModel>[] validations)
        {
            validatable.Validations.AddRange(validations);
            return validatable;
        }

        public static Validatable<TModel> IsRequired<TModel>(this Validatable<TModel> validatable, string errorMessage = "")
        {

            if(typeof(TModel) == typeof(string))
                validatable.Validations.Add(new NotEmptyRule<TModel>("").WithMessage(errorMessage));

            if (typeof(TModel).IsClass)
                validatable.Validations.Add(new NotNullRule<TModel>().WithMessage(errorMessage));

            return validatable;
        }

        public static Validatable<TModel> Must<TModel>(this Validatable<TModel> validatable, Func<TModel, bool> predicate, string errorMessage = "")
        {
            validatable.Validations.Add(new FunctionRule<TModel>(predicate).WithMessage(errorMessage));
            return validatable;
        }

        public static Validatable<TModel> When<TModel>(this Validatable<TModel> validatable, Func<TModel, bool> predicate)
        {
            validatable.HasWhenCondition(true);
            validatable.Validations.Insert(0, new WhenRule<TModel>(ref validatable, predicate));
            return validatable;
        }

        #region Default Rules Extension

        /// <summary>
        /// Add the <see cref="CreditCardRule"/> validation to the validatable property.
        /// </summary>
        /// <param name="validatable">The validatable property were the new validation
        /// rule will be added.</param>
        /// <param name="errorMessage">The custom validation message.</param>
        public static Validatable<string> IsCreditCard(
            this Validatable<string> validatable,
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
        public static Validatable<string> IsEmail(
            this Validatable<string> validatable,
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
        public static Validatable<object> IsEmpty(
            this Validatable<object> validatable,
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
        public static Validatable<TModel> IsNotEmpty<TModel>(
            this Validatable<TModel> validatable,
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
        public static Validatable<object> IsEnum(
            this Validatable<object> validatable,
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
        public static Validatable<object> IsEqual(
            this Validatable<object> validatable,
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
        public static Validatable<object> IsEqual(
            this Validatable<object> validatable,
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
        public static Validatable<object> IsNotEqual(
            this Validatable<object> validatable,
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
        public static Validatable<object> IsNotEqual(
            this Validatable<object> validatable,
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
        public static Validatable<IComparable> IsGreaterThan(
            this Validatable<IComparable> validatable,
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
        public static Validatable<IComparable> IsGreaterThanOrEqual(
            this Validatable<IComparable> validatable,
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
        public static Validatable<IComparable> IsInclusiveBetween(
            this Validatable<IComparable> validatable,
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
        public static Validatable<IComparable> IsLessThan(
            this Validatable<IComparable> validatable,
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
        public static Validatable<IComparable> IsLessThanOrEqual(
            this Validatable<IComparable> validatable,
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
        public static Validatable<object> IsNull(
            this Validatable<object> validatable,
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
        public static Validatable<object> IsNotNull(
            this Validatable<object> validatable,
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
        public static Validatable<int> WithLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithExactLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithExactLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithMaxLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithMaxLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithMinimumLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<int> WithMinimumLengthRule(
            this Validatable<int> validatable,
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
        public static Validatable<string> WithRegularExpression(
            this Validatable<string> validatable,
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
        public static Validatable<string> WithRegularExpression(
            this Validatable<string> validatable,
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
        public static Validatable<string> WithRegularExpression(
            this Validatable<string> validatable,
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
        public static Validatable<string> WithRegularExpression(
            this Validatable<string> validatable,
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
        public static Validatable<string> WithRegularExpression(
            this Validatable<string> validatable,
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
