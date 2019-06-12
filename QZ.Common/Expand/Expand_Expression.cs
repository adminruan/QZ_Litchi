using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QZ.Common.Expand
{
    /// <summary>
    /// 表达式树 拓展类
    /// </summary>
    public static class Expand_Expression
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) where T : class
        {
            if (expr1==null)
            {
                return expr2;
            }
            else if(expr2==null)
            {
                return expr1;
            }

            ParameterExpression parameter = Expression.Parameter(typeof(T),"c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(parameter);

            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);

            var body = Expression.And(left,right);
            return Expression.Lambda<Func<T, bool>>(body,parameter);
        }

        /// <summary>
        /// 合并表达式 expr1 or expr2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                return expr2;
            else if (expr2 == null)
                return expr1;

            ParameterExpression newParameter = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor visitor = new NewExpressionVisitor(newParameter);

            var left = visitor.Replace(expr1.Body);
            var right = visitor.Replace(expr2.Body);
            var body = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            if (expr == null)
                return null;

            var candidateExpr = expr.Parameters[0];
            var body = Expression.Not(expr.Body);
            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
}
