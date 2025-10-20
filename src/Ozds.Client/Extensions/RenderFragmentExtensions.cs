using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Extensions;

public static class RenderFragmentExtensions
{
  // NOTE: because RenderFragment<TValue> is invariant on TValue ...
  public static object Concretize<TValue>(
    this RenderFragment<TValue> baseFragment,
    Type concrete
  )
  {
    if (!concrete.IsAssignableTo(typeof(TValue)))
    {
      throw new ArgumentException(
        "Concrete type must be assignable to TValue",
        nameof(concrete)
      );
    }

    var parameter = Expression.Parameter(concrete, "concrete");

    var toBase = Expression.Convert(parameter, typeof(TValue));

    var invoke = Expression.Invoke(
      Expression.Constant(baseFragment),
      toBase
    );

    var lambdaType = typeof(RenderFragment<>).MakeGenericType(concrete);
    var lambda = Expression.Lambda(lambdaType, invoke, parameter);

    return lambda.Compile();
  }
}
