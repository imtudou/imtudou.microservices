namespace Imtudou.Core.Extensions
{
    using System;
    using System.Linq.Expressions;

    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }

    /// <summary>
    /// 表达式构造
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// 拼接查询条件or
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="one">调用者</param>
        /// <param name="another">Lambda表达式</param>
        /// <returns>Lambda表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "x");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            if (one == null)
            {
                return another;
            }

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.OrElse(left, right);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 拼接查询条件and
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="one">调用者</param>
        /// <param name="another">Lambda表达式</param>
        /// <returns>Lambda表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "x");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            if (one == null)
            {
                return another;
            }

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.AndAlso(left, right);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// bool Expression WhereIf
        /// </summary>
        /// <typeparam name="TSource">T</typeparam>
        /// <param name="query">query</param>
        /// <param name="flag">flag</param>
        /// <param name="expression">expression</param>
        /// <returns>Expression<Func<TSource, bool>></returns>
        public static Expression<Func<TSource, bool>> WhereIf<TSource>(this Expression<Func<TSource, bool>> query, bool flag, Expression<Func<TSource, bool>> expression)
        {
            return flag ? query.And(expression) : query;
        }

        /// <summary>
        /// string Expression WhereIf
        /// </summary>
        /// <typeparam name="TSource">T</typeparam>
        /// <param name="query">query</param>
        /// <param name="flag">flag</param>
        /// <param name="expression">expression</param>
        /// <returns>Expression<Func<TSource, bool>></returns>
        public static Expression<Func<TSource, bool>> WhereIf<TSource>(this Expression<Func<TSource, bool>> query, string flag, Expression<Func<TSource, bool>> expression)
        {
            return !string.IsNullOrEmpty(flag) ? query.And(expression) : query;
        }
    }
}
