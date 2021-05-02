using System;
using System.Collections;

using Plugin.ValidationRules.Properties;
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

        #region Credit Card

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
            var invalidMessage = errorMessage ?? ValidationMessages.CreditCardRuleMessage;

            validatable.Validations.Add(new CreditCardRule { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

        #region Email

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
            var invalidMessage = errorMessage ?? ValidationMessages.EmailRuleMessage;

            validatable.Validations.Add(new CreditCardRule { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

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
            var invalidMessage = errorMessage ?? ValidationMessages.EmptyRuleMessage;

            validatable.Validations.Add(new EmptyRule(defaultValue) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.NotEmptyRuleMessage;

            validatable.Validations.Add(new NotEmptyRule<TModel>(defaultValue) { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

        #region Enumerable

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
            var invalidMessage = errorMessage ?? ValidationMessages.EnumRuleMessage;

            validatable.Validations.Add(new EnumRule(enumType) { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

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
            var invalidMessage = errorMessage ?? ValidationMessages.EqualRuleMessage;

            validatable.Validations.Add(new EqualRule(comparisonValue, equalityComparer) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.EqualRuleMessage;

            validatable.Validations.Add(new EqualRule(func, equalityComparer) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.NotEqualRuleMessage;

            validatable.Validations.Add(new NotEqualRule(comparisonValue, equalityComparer) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.NotEqualRuleMessage;

            validatable.Validations.Add(new NotEqualRule(func, equalityComparer) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.GreaterThanRuleMessage, defaultValue);

            validatable.Validations.Add(new GreaterThanRule(defaultValue) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.GreaterThanOrEqualRuleMessage, defaultValue);

            validatable.Validations.Add(new GreaterThanOrEqualRule(defaultValue) { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

        #region Inclusive Between

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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.InclusiveBetweenRuleMessage, from, to);

            validatable.Validations.Add(new InclusiveBetweenRule(from, to) { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.LessThanRuleMessage, defaultValue);

            validatable.Validations.Add(new LessThanRule(defaultValue) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.LessThanOrEqualRuleMessage, defaultValue);

            validatable.Validations.Add(new LessThanOrEqualRule(defaultValue) { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.NullRuleMessage;

            validatable.Validations.Add(new NullRule { ValidationMessage = invalidMessage });

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
            var invalidMessage = errorMessage ?? ValidationMessages.NotNullRuleMessage;

            validatable.Validations.Add(new NotNullRule { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion

        #region Length



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
            var invalidMessage = errorMessage ??
                string.Format(ValidationMessages.LessThanRuleMessage, expression);

            validatable.Validations.Add(new RegularExpressionRule(expression) { ValidationMessage = invalidMessage });

            return validatable;
        }

        #endregion
    }
}
