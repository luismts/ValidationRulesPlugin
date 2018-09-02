using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;


namespace Plugin.ValidationRules.Extensions
{
    /// <summary>
    /// Provides a mechanism by which application developers can propagate changes that
    /// are made to data in one object to another, by enabling validation, type coercion,
    /// and an event system.
    /// </summary>
    public abstract class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// Notifies the data source of a change to a property value of the specified <param name="property"></param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        /// <summary>
        /// Obtains information about the attributes of a member and provides access to member metadata.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;

            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }

            return operand.Member;
        }
    }
}
