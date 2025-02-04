using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsPrefixedModelComponentBase<TPrefix, TModel> :
  OzdsModelComponentBase<TModel>
{
  private Expression<Func<TPrefix, TModel?>>? exp;

  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; }

  protected Expression<Func<TPrefix, TModel?>> Exp
  {
    get { return exp ??= CreateExp(); }
  }

  protected Expression<Func<TPrefix, T?>> Wrap<T>(
      Expression<Func<TModel, T?>> next
  )
  {
    var inner = Exp;

    var nextReplaced = ParameterReplacer.Replace(
      next.Body,
      next.Parameters[0],
      inner.Body
    );

    var condition = Expression.Condition(
      Expression.Equal(inner.Body, Expression.Default(typeof(TModel))),
      Expression.Default(typeof(T)),
      nextReplaced
    );

    return Expression.Lambda<Func<TPrefix, T?>>(
      condition,
      inner.Parameters[0]
    );
  }

  protected virtual Expression<Func<TPrefix, TModel?>> CreateExp()
  {
    return Prefix ?? (x => (TModel?)(object?)x);
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

    if (exp is not null)
    {
      exp = CreateExp();
    }
  }
}
