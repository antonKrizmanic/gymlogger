using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Core.CodeExtensions;
public static class ExpressionExtensions
{
    /// <summary>
    /// Given an expression for a method that takes in a single parameter (and returns a bool), this
    /// method converts the parameter type of the parameter from TSource to TTarget.
    /// </summary>
    /// <typeparam name="TSource">Type of the source.</typeparam>
    /// <typeparam name="TTarget">Type of the target.</typeparam>
    /// <param name="root">The root to act on.</param>
    /// <returns>
    /// An Expression&lt;Func&lt;TTarget,bool&gt;&gt;
    /// </returns>
    public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(
        this Expression<Func<TSource, bool>> root)
    {
        var visitor = new ParameterTypeVisitor<TSource, TTarget>();
        return (Expression<Func<TTarget, bool>>)visitor.Visit(root);
    }

    /// <summary>
    /// A parameter type visitor.
    /// </summary>
    /// <typeparam name="TSource">Type of the source.</typeparam>
    /// <typeparam name="TTarget">Type of the target.</typeparam>
    /// <seealso cref="T:System.Linq.Expressions.ExpressionVisitor"/>
    private class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
    {
        /// <summary>
        /// Options for controlling the operation.
        /// </summary>
        private ReadOnlyCollection<ParameterExpression> parameters;

        /// <summary>
        /// Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the
        /// original expression.
        /// </returns>
        /// <seealso cref="M:System.Linq.Expressions.ExpressionVisitor.VisitParameter(ParameterExpression)"/>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this.parameters?.FirstOrDefault(p => p.Name == node.Name)
                   ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node);
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.Expression`1" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /// <typeparam name="T">The type of the delegate.</typeparam>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the
        /// original expression.
        /// </returns>
        /// <seealso cref="M:System.Linq.Expressions.ExpressionVisitor.VisitLambda{T}(Expression{T})"/>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            this.parameters = this.VisitAndConvert(node.Parameters, "VisitLambda");
            return Expression.Lambda(this.Visit(node.Body) ?? throw new InvalidOperationException(), this.parameters);
        }
    }
}