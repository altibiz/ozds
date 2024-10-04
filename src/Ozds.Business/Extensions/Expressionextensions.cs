using System.Linq.Expressions;

namespace Ozds.Business.Extensions;

public static class ExpressionExtensions
{
  public static Expression<Func<TIn, TOut>> Prefix<TIn, TMid, TOut>(
    this Expression<Func<TMid, TOut>> expr,
    Expression<Func<TIn, TMid>> other)
  {
    var parameter = Expression.Parameter(typeof(TIn));
    var callOther = Expression.Invoke(other, parameter);
    var callExpr = Expression.Invoke(expr, callOther);
    return Expression.Lambda<Func<TIn, TOut>>(callExpr, parameter);
  }
}
