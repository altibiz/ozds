using System.Linq.Expressions;

namespace Ozds.Client.Extensions;

public static class ExpressionExtensions
{
  public static string Label<TIn, TOut>(
    this Expression<Func<TIn, TOut>> expression
  )
  {
    if (expression.Body is MemberExpression memberExpression)
    {
      return memberExpression.Member.Name;
    }

    if (expression.Body is UnaryExpression unaryExpression
      && unaryExpression.NodeType == ExpressionType.Convert
      && unaryExpression.Operand is MemberExpression innerMemberExpression)
    {
      return innerMemberExpression.Member.Name;
    }

    return "";
  }

  public static MemberExpression LabelExpression(
    this Expression expression
  )
  {
    var body = expression is LambdaExpression { Body: { } lambdaBody }
      ? lambdaBody
      : expression;

    if (body is MemberExpression memberExpression)
    {
      return memberExpression;
    }

    if (body is UnaryExpression unaryExpression
      && unaryExpression.NodeType == ExpressionType.Convert)
    {
      return unaryExpression.Operand.LabelExpression();
    }

    throw new InvalidOperationException(
      $"Expression {expression} is not a member expression.");
  }

  public static Expression<Func<TIn, TOut>> Prefix<TIn, TMid, TOut>(
    this Expression<Func<TMid, TOut>> outer,
    Expression<Func<TIn, TMid>> inner)
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
    Expression<Func<TMid, TOut>> outer)
  {
    return outer.Prefix(inner);
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
    Expression newExpression)
  {
    return new ParameterReplacer(oldParam, newExpression).Visit(body);
  }
}
