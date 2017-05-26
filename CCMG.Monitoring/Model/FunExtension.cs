using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CCMG.Monitoring.Model
{
    public static class FunExtension
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,params Expression<Func<T, bool>>[] exprList)
        {
            if (exprList.Length > 0)
            {
                var parameter = Expression.Parameter(typeof(T));
                var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
                var left = leftVisitor.Visit(expr1.Body);
                foreach (var item in exprList)
                {
                    var rightVisitor = new ReplaceExpressionVisitor(item.Parameters[0], parameter);
                    var right = rightVisitor.Visit(item.Body);

                    expr1 = Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
                }
            }
            return expr1;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,Expression<Func<T, bool>>[] exprList)
        {
            if (exprList.Length > 0)
            {
                var parameter = Expression.Parameter(typeof(T));
                var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
                var left = leftVisitor.Visit(expr1.Body);
                foreach (var item in exprList)
                {
                    var rightVisitor = new ReplaceExpressionVisitor(item.Parameters[0], parameter);
                    var right = rightVisitor.Visit(item.Body);

                    expr1 = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
                }
            }
            return expr1;
        }



        private class ReplaceExpressionVisitor: ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }


}
