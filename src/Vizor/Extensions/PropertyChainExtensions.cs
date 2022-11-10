//   Copyright 2022 DataHint BV
//   Copyright 2022 Ben Motmans
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

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
