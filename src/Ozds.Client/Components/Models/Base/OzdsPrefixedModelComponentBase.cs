using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsPrefixedModelComponentBase<TPrefix, TModel> :
  OzdsModelComponentBase<TModel>
{
  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; } = default!;

  protected Expression<Func<TPrefix, T?>> Wrap<T>(
    Expression<Func<TModel, T?>> next
  )
  {
    var first = Exp;
    var param = first.Parameters[0];
    var modelExpression = first.Body;
    var nullCheck = Expression.Equal(
      modelExpression,
      Expression.Constant(null, typeof(TModel))
    );
    var ifTrue = Expression.Constant(default(T), typeof(T));
    var ifFalse = Expression.Invoke(next, modelExpression);
    var body = Expression.Condition(nullCheck, ifTrue, ifFalse);
    return Expression.Lambda<Func<TPrefix, T?>>(body, param);
  }

  protected Expression<Func<TPrefix, TModel?>> Exp =>
    exp ??= CreateExp();

  private Expression<Func<TPrefix, TModel?>>? exp;

  protected virtual Expression<Func<TPrefix, TModel?>> CreateExp()
  {
    return Prefix ?? ((TPrefix x) => (TModel?)(object?)x);
  }

  protected override Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      baseModelType,
      ComponentKind
    );
  }

  protected override Type CreateBaseComponentType(Type baseModelType)
  {
    if (!baseModelType.IsAssignableFrom(typeof(TModel)))
    {
      throw new InvalidOperationException(
        $"{baseModelType} is not assignable to {typeof(TModel)}");
    }

    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      baseModelType,
      ComponentKind
    );
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (exp is { })
    {
      exp = CreateExp();
    }
  }
}
