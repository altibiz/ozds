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

  public Type GetPageComponentType(Type modelType)
  {
    return GetPageProvider(modelType).PageType;
  }

  public string CreateLink(object model)
  {
    return GetPageProvider(model.GetType()).CreateLink(model);
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

    provider = typeof(ModelComponentProviderCache).Assembly
      .GetTypes()
      .Where(type =>
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
        if (genericArgumentConstraints.Length == 0)
        {
          return true;
        }

        if (genericArgumentConstraints.Length != 1)
        {
          return false;
        }

        return constraintType.IsAssignableTo(
          genericArgumentConstraints.First());
      })
      .Select(generic =>
      {
        var constraintType = generic
          .GetGenericArguments()[0]
          .GetGenericParameterConstraints()
          .FirstOrDefault()
          ?? typeof(object);
        var service = (ServiceProvider
          .GetService(generic.MakeGenericType(modelType))
          as IModelComponentProvider)!;

        return new
        {
          Service = service,
          ConstraintType = constraintType
        };
      })
      .Where(x => x.Service.CanRender(modelType))
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.ConstraintType.IsAssignableTo(acc.ConstraintType)
            ? acc
            : next)
      ?.Service
      ?? throw new InvalidOperationException(
        "No model component provider found for model type "
        + modelType.FullName
        + " with constraint type "
        + constraintType.FullName);

    genericCache.TryAdd(key, provider);

    return provider;
  }

  private IModelPageComponentProvider GetPageProvider(Type modelType)
  {
    if (pageCache.TryGetValue(modelType, out var provider))
    {
      return provider;
    }

    provider = ServiceProvider
      .GetServices<IModelPageComponentProvider>()
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

    pageCache.TryAdd(modelType, provider);

    return provider;
  }

  private readonly Dictionary<
    (Type, ModelComponentKind),
    IModelComponentProvider> cache = new();

  private readonly Dictionary<
    (Type, Type, ModelComponentKind),
    IModelComponentProvider> genericCache = new();

  private readonly Dictionary<Type, IModelPageComponentProvider> pageCache = new();
}
