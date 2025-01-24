using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class ListModelComponent<TPrefix, TModel> : ModelComponent
{
  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  [Parameter]
  public Func<TPrefix, TModel>? Prefix { get; set; } = default!;

  protected override Type CreateModelType()
  {
    return Models
      .Select(x => x.GetType())
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.IsAssignableFrom(acc)
            ? next
            : acc)
      ?? throw new InvalidOperationException(
        $"No model type found for {nameof(Models)}.");
  }

  protected override Type CreateComponentType()
  {
    var constraintType =
      GetType()
      .GetGenericTypeDefinition()
      .GetGenericArguments()[1]
      .GetGenericParameterConstraints()[0];

    return Cache.GetPrefixedComponentType(
      ModelType,
      constraintType,
      typeof(TPrefix),
      ComponentKind
    );
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    return new()
    {
      [nameof(OzdsListModelComponentBase<object, object>.Models)] = Models!,
      [nameof(OzdsListModelComponentBase<object, object>.Prefix)] = Prefix!,
    };
  }
}
