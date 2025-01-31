using System.Linq.Expressions;

namespace Ozds.Client.Components.Models.Base;

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
