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
}
