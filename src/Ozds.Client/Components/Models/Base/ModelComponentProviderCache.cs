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
    return GetComponentProvider(modelType, componentKind).ComponentType;
  }

  public Type GetGenericComponentType(
    Type modelType,
    Type constraintType,
    ModelComponentKind componentKind
  )
  {
    return GetGenericComponentProvider(modelType, constraintType, componentKind)
      .ComponentType;
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
            : next)
      ?? throw new InvalidOperationException(
        "No model component provider found for model type "
        + modelType.FullName);

    cache.TryAdd(key, provider);

    return provider;
  }

  private IModelComponentProvider GetGenericComponentProvider(
    Type modelType,
    Type constraintType,
    ModelComponentKind kind)
  {
    var key = (modelType, constraintType, kind);

    if (genericCache.TryGetValue(key, out var provider))
    {
      return provider;
    }

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

        return genericArgumentConstraints.First() == constraintType;
      }) ?? throw new InvalidOperationException(
        "No model component provider found for model type "
        + modelType.FullName);

    provider = ServiceProvider.GetService(generic.MakeGenericType(modelType))
      as IModelComponentProvider
      ?? throw new InvalidOperationException(
        "No model component provider found for model type "
        + modelType.FullName);

    if (!provider.CanRender(modelType))
    {
      throw new InvalidOperationException(
        "Model component provider for model type "
        + modelType.FullName);
    }

    genericCache.TryAdd(key, provider);

    return provider;
  }

  private readonly Dictionary<
    (Type, ModelComponentKind),
    IModelComponentProvider> cache = new();

  private readonly Dictionary<
    (Type, Type, ModelComponentKind),
    IModelComponentProvider> genericCache = new();
}
