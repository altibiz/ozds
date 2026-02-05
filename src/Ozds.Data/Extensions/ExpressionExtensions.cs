using System.Linq.Expressions;

namespace Ozds.Data.Extensions;

public static class ExpressionExtensions
{
  public static Expression<Func<TIn, TOut>> Prefix<TIn, TMid, TOut>(
    this Expression<Func<TMid, TOut>> outer,
    Expression<Func<TIn, TMid>> inner
  )
  {
    var parameter = Expression.Parameter(typeof(TIn));
    var innerBody = ParameterReplacer.Replace(
      inner.Body,
      inner.Parameters[0],
      parameter
    );
    var body = ParameterReplacer.Replace(
      outer.Body,
      outer.Parameters[0],
      innerBody
    );
    return Expression.Lambda<Func<TIn, TOut>>(body, parameter);
  }

  public static Expression<Func<TIn, TOut>> Suffix<TIn, TMid, TOut>(
    this Expression<Func<TIn, TMid>> inner,
    Expression<Func<TMid, TOut>> outer
  )
  {
    return outer.Prefix(inner);
  }

  public static IEnumerable<string> GetMemberExpressionPath(
    this MemberExpression expr
  )
  {
    IEnumerable<string> Reverse()
    {
      Expression? current = expr;
      while (current is MemberExpression memberExpression)
      {
        current = memberExpression.Expression;
        yield return memberExpression.Member.Name;
      }
    }

    return Reverse().Reverse();
  }

  public static MemberExpression ToMemberExpression<TParameter, TReturn>(
    this Expression<Func<TParameter, TReturn>> expr
  )
  {
    var expression = expr.Body;
    if (
      expression is UnaryExpression unaryExpression
      && unaryExpression.NodeType == ExpressionType.Convert
    )
    {
      expression = unaryExpression.Operand;
    }

    if (expression is not MemberExpression memberExpression)
    {
      throw new InvalidOperationException(
        $"Expression {expression} is not a member expression."
      );
    }

    return memberExpression;
  }
}

internal sealed class ParameterReplacer(
  ParameterExpression oldParam,
  Expression newExpression
) : ExpressionVisitor
{
  protected override Expression VisitParameter(ParameterExpression node)
  {
    return node == oldParam ? newExpression : base.VisitParameter(node);
  }

  public static Expression Replace(
    Expression body,
    ParameterExpression oldParam,
    Expression newExpression
  )
  {
    return new ParameterReplacer(oldParam, newExpression).Visit(body);
  }
}
