using Microsoft.Extensions.DependencyInjection;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public class ModelComponentProviderCache(
  IServiceProvider ServiceProvider
)
{
  public Type GetComponentType(
    Type modelType,
    ModelComponentKind componentKind
  )
  {
    var componentType =
      GetComponentProvider(modelType, componentKind).ComponentType;
    if (componentType.IsGenericType)
    {
      componentType = componentType.MakeGenericType(modelType);
    }

    return componentType;
  }

  private IModelComponentProvider GetComponentProvider(
    Type modelType,
    ModelComponentKind componentKind
  )
  {
    var key = (modelType, componentKind);
    if (cache.TryGetValue(key, out var provider))
    {
      return provider;
    }

    provider = ServiceProvider
      .GetServices<IModelComponentProvider>()
      .Where(provider => provider.CanRender(modelType))
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.ModelType.IsAssignableTo(acc.ModelType)
            ? acc
            : next);

    if (provider is null)
    {
      var generic = typeof(ModelComponentProviderCache).Assembly
        .GetTypes()
        .FirstOrDefault(type =>
        {
          if (!type.IsAssignableTo(typeof(IModelComponentProvider)))
          {
            return false;
          }

          if (!type.IsGenericType)
          {
            return false;
          }

          var genericTypeArguments = type.GenericTypeArguments;
          if (genericTypeArguments.Length != 1)
          {
            return false;
          }

          var genericArgumentConstraints = genericTypeArguments
            .First()
            .GetGenericParameterConstraints();
          if (genericArgumentConstraints.Length != 1)
          {
            return false;
          }

          return modelType.IsAssignableTo(genericArgumentConstraints.First());
        }) ?? throw new InvalidOperationException(
          "No model component provider found for model type "
          + modelType.FullName);

      provider = ServiceProvider.GetService(generic.MakeGenericType(modelType))
        as IModelComponentProvider
        ?? throw new InvalidOperationException(
          "No model component provider found for model type "
          + modelType.FullName);
    }

    cache.TryAdd(key, provider);

    return provider;
  }

  private readonly Dictionary<
    (Type, ModelComponentKind),
    IModelComponentProvider> cache = new();
}
