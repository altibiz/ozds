using Microsoft.Extensions.DependencyInjection;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models;

public class ModelComponentProvider(
  IServiceProvider ServiceProvider
)
{
  public Type GetComponentType(
    Type modelType,
    ModelComponentKind componentKind
  )
  {
    var provider = GetComponentProvider(modelType, componentKind);

    return provider.ComponentType;
  }

  public Type GetGenericComponentType(
    Type modelType,
    Type constraintType,
    ModelComponentKind componentKind
  )
  {
    var provider = GetGenericComponentProvider(
      modelType,
      constraintType,
      componentKind
    );

    return provider.ComponentType;
  }

  public Type GetPrefixedComponentType(
    Type prefixType,
    Type modelType,
    Type constraintType,
    ModelComponentKind componentKind
  )
  {
    var provider = GetPrefixedComponentProvider(
      prefixType,
      modelType,
      constraintType,
      componentKind
    );

    return provider.ComponentType;
  }

  public Type GetPageComponentType(Type modelType)
  {
    var provider = GetPageProvider(modelType);

    return provider.PageType;
  }

  public string CreateLink(object model)
  {
    var provider = GetPageProvider(model.GetType());

    return provider.CreateLink(model);
  }

  private IModelComponentProvider GetComponentProvider(
    Type modelType,
    ModelComponentKind componentKind
  )
  {
    var key = new CacheKey(modelType, componentKind);
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
    var key = new GenericCacheKey(modelType, constraintType, kind);

    if (genericCache.TryGetValue(key, out var provider))
    {
      return provider;
    }

    provider = typeof(ModelComponentProvider).Assembly
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

        var genericTypeArguments = type.GetGenericArguments();
        if (genericTypeArguments.Length != 1)
        {
          return false;
        }

        var genericArgumentConstraints = genericTypeArguments[0]
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

  private IModelComponentProvider GetPrefixedComponentProvider(
    Type prefixType,
    Type modelType,
    Type constraintType,
    ModelComponentKind kind)
  {
    var key = new PrefixedCacheKey(prefixType, modelType, constraintType, kind);

    if (prefixedCache.TryGetValue(key, out var provider))
    {
      return provider;
    }

    provider = typeof(ModelComponentProvider).Assembly
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

        var genericTypeArguments = type.GetGenericArguments();
        if (genericTypeArguments.Length != 2)
        {
          return false;
        }

        var genericArgumentConstraints = genericTypeArguments[1]
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
          .GetGenericArguments()[1]
          .GetGenericParameterConstraints()
          .FirstOrDefault()
          ?? typeof(object);
        var service = (ServiceProvider
          .GetService(generic.MakeGenericType(prefixType, modelType))
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

    prefixedCache.TryAdd(key, provider);

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
    CacheKey,
    IModelComponentProvider> cache = new();

  private readonly Dictionary<
    GenericCacheKey,
    IModelComponentProvider> genericCache = new();

  private readonly Dictionary<
    PrefixedCacheKey,
    IModelComponentProvider> prefixedCache = new();

  private readonly Dictionary<
    Type,
    IModelPageComponentProvider> pageCache = new();

  private sealed record CacheKey(
    Type ModelType,
    ModelComponentKind ComponentKind
  );

  private sealed record GenericCacheKey(
    Type ModelType,
    Type ConstraintType,
    ModelComponentKind ComponentKind
  );

  private sealed record PrefixedCacheKey(
    Type PrefixType,
    Type ModelType,
    Type ConstraintType,
    ModelComponentKind ComponentKind
  );
}
