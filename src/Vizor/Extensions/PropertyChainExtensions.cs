using System.Linq.Expressions;

namespace Vizor.Extensions;

//TODO: add support for indexers
public static class PropertyChainExtensions
{
    public static string ToPropertyChain(this LambdaExpression expression)
        => GetChain(expression.Body);

    public static string ToPropertyChain(this Expression<Func<object?>> expression)
        => GetChain(expression.Body);

    private static string GetChain(Expression expression)
    {
        var names = new Stack<string>(3);

        var memberExpr = GetMemberExpression(expression);
        while (memberExpr != null && memberExpr.Member.MemberType == System.Reflection.MemberTypes.Property)
        {
            names.Push(memberExpr.Member.Name);
            memberExpr = GetMemberExpression(memberExpr.Expression);
        }

        return names.Count switch
        {
            0 => string.Empty,
            1 => names.First(),
            _ => string.Join(".", names)
        };
    }

    private static MemberExpression? GetMemberExpression(Expression? expression)
    {
        if (expression is UnaryExpression unary)
        {
            return unary.Operand as MemberExpression;
        }

        return expression as MemberExpression;
    }
}
