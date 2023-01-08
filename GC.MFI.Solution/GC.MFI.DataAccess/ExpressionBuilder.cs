using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GC.MFI.DataAccess
{
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }
            if (exp == null)
                return null;
            else
                return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            if (filter != null && !string.IsNullOrEmpty(filter.PropertyName))
            {
                MemberExpression member = Expression.Property(param, filter.PropertyName);
                ConstantExpression constant = Expression.Constant(filter.Value);

                switch (filter.Operation)
                {
                    case MathenaticalOperators.Equals:
                        return Expression.Equal(member, constant);

                    case MathenaticalOperators.GreaterThan:
                        return Expression.GreaterThan(member, constant);

                    case MathenaticalOperators.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);

                    case MathenaticalOperators.LessThan:
                        return Expression.LessThan(member, constant);

                    case MathenaticalOperators.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);

                    case MathenaticalOperators.Contains:
                        return Expression.Call(member, containsMethod, constant);

                    case MathenaticalOperators.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);

                    case MathenaticalOperators.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                }
            }
            return null;
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
    public class Filter
    {
        public string PropertyName { get; set; }
        public MathenaticalOperators Operation { get; set; }
        public object Value { get; set; }
    }
    public enum MathenaticalOperators
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
}
