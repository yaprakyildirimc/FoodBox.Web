using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Reflection;

namespace FoodBox.Web.Services
{
    public class DataTableFilterService<TEntity> where TEntity : class
    {
        private readonly Dictionary<Operation, Func<Expression, Expression, Expression>> Expressions;
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public DataTableFilterService()
        {
            Expressions = new Dictionary<Operation, Func<Expression, Expression, Expression>>();
            Expressions.Add(Operation.EqualTo, (member, constant) => Expression.Equal(member, constant));
            Expressions.Add(Operation.NotEqualTo, (member, constant) => Expression.NotEqual(member, constant));
            Expressions.Add(Operation.GreaterThan, (member, constant) => Expression.GreaterThan(member, constant));
            Expressions.Add(Operation.GreaterThanOrEqualTo, (member, constant) => Expression.GreaterThanOrEqual(member, constant));
            Expressions.Add(Operation.LessThan, (member, constant) => Expression.LessThan(member, constant));
            Expressions.Add(Operation.LessThanOrEqualTo, (member, constant) => Expression.LessThanOrEqual(member, constant));
            Expressions.Add(Operation.Contains, (member, constant) => Expression.Call(member, containsMethod, constant));
            Expressions.Add(Operation.StartsWith, (member, constant) => Expression.Call(member, startsWithMethod, constant));
            Expressions.Add(Operation.EndsWith, (member, constant) => Expression.Call(member, endsWithMethod, constant));
        }

        public enum Operation
        {
            EqualTo,
            Contains,
            StartsWith,
            EndsWith,
            NotEqualTo,
            GreaterThan,
            GreaterThanOrEqualTo,
            LessThan,
            LessThanOrEqualTo
        }

        public class SearchList
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private MemberExpression GetMemberExpression(Expression param, string propertyName)
        {
            if (propertyName.Contains("."))
            {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }

        private List<SearchList> GetSearchList(int columnCount, HttpContext request)
        {
            List<SearchList> list = new List<SearchList>();
            for (int i = 0; i < columnCount; i++)
            {
                list.Add(
                    new SearchList
                    {
                        Name = request.Request.Form["columns[" + i + "][name]"].ToString(),
                        Value = request.Request.Form["columns[" + i + "][search][value]"].ToString()
                    });
            }
            return list;
        }

        public Expression<Func<TEntity, bool>> FilterWithExpression(HttpContext request)
        {
            List<SearchList> list = GetSearchList(request.Request.Form.Keys.Where(x => x.Contains("data")).Count(), request);

            Expression finalExpression = Expression.Constant(true);
            Expression invokeExpression = null;


            var parameter = Expression.Parameter(typeof(TEntity), "x");

            Operation operation = Operation.Contains;

            foreach (var item in list)
            {
                if (item.Value.Length > 0)
                {
                    MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);
                    MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
                    MethodInfo toStringMethod = typeof(string).GetMethod("ToString", new Type[0]);

                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                    var memberExpression = GetMemberExpression(parameter, item.Name);

                    MethodCallExpression memberExpressionCall = null;
                    MethodCallExpression constantCall = null;

                    var constant = Expression.Constant(item.Value);

                    if (memberExpression.Type.Name == "String")
                    {
                        var trimMemberCall = Expression.Call(memberExpression, trimMethod);
                        // x.Name.Trim().ToLower()
                        memberExpressionCall = Expression.Call(trimMemberCall, toLowerMethod);
                        // "John ".Trim()
                        var trimConstantCall = Expression.Call(constant, trimMethod);
                        // "John ".Trim().ToLower()
                        constantCall = Expression.Call(trimConstantCall, toLowerMethod);

                        //invokeExpression = Expressions[operation].Invoke(memberExpressionCall, constantCall);

                        invokeExpression = Expression.Call(memberExpressionCall, containsMethod, constantCall);


                        var nullCheck = Expression.NotEqual(memberExpression, Expression.Constant(null, typeof(object)));
                        finalExpression = Expression.AndAlso(finalExpression, nullCheck);
                    }
                    else if (memberExpression.Type.Name == "Int32" || memberExpression.Type.GenericTypeArguments.FirstOrDefault().Name == "Int32")
                    {
                        operation = Operation.EqualTo;
                        var convertedExpression = Expression.Call(
                         Expression.Convert(memberExpression, typeof(object)),
                         typeof(object).GetMethod("ToString"));
                        invokeExpression = Expressions[operation].Invoke(convertedExpression, constant);
                    }
                    else if (memberExpression.Type.Name == "DateTime")
                    {
                        operation = Operation.EqualTo;
                        var dateTimeConstantFirst = Expression.Constant(Convert.ToDateTime(item.Value));
                        var dateTimeConstantLast = Expression.Constant(Convert.ToDateTime(item.Value).AddDays(1));

                        invokeExpression = Expressions[Operation.GreaterThanOrEqualTo].Invoke(memberExpression, dateTimeConstantFirst);
                        invokeExpression = Expression.AndAlso(invokeExpression, Expressions[Operation.LessThanOrEqualTo].Invoke(memberExpression, dateTimeConstantLast));
                    }
                    //------------Test------------
                    //var convertedExpression = Expression.Call(
                    // Expression.Convert(memberExpression, typeof(object)),
                    // typeof(object).GetMethod("ToString"));

                    //var bodyTest = Expression.NotEqual(convertedExpression, constant);
                    //------------Test------------


                    //var body = Expression.Equal(memberExpression, constant); //x=> x.parameter == value
                    //var body = Expression.NotEqual(memberExpression, constant); //x=> x.parameter == value



                    //var containsValueExpression = Expression.Call(memberExpressionCall, containsMethod, constantCall);
                    finalExpression = Expression.AndAlso(finalExpression, invokeExpression);
                }
            }

            return Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameter);
        }
    }

}