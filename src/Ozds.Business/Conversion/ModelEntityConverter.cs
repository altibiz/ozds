using System.Collections.Concurrent;
using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion;

public class ModelEntityConverter(IServiceProvider serviceProvider)
{
  public TEntity ToEntity<TEntity>(object model)
  {
    return (TEntity)ToEntity(model);
  }

  public TModel ToModel<TModel>(object entity)
  {
    return (TModel)ToModel(entity);
  }

  public Type EntityType(Type type)
  {
    if (entityCache.TryGetValue(type, out var converter))
    {
      return converter.EntityType;
    }

    converter = FindEntityConverter(type);

    entityCache.TryAdd(type, converter);

    return converter.EntityType;
  }

  public Type ModelType(Type type)
  {
    if (modelCache.TryGetValue(type, out var converter))
    {
      return converter.ModelType;
    }

    converter = FindModelConverter(type);

    modelCache.TryAdd(type, converter);

    return converter.ModelType;
  }

  public object ToEntity(object model)
  {
    if (entityCache.TryGetValue(model.GetType(), out var converter))
    {
      return converter.ToEntity(model);
    }

    converter = FindEntityConverter(model.GetType());

    entityCache.TryAdd(model.GetType(), converter);

    if (!converter.CanConvertToEntity(model.GetType()))
    {
      throw new InvalidOperationException(
        $"No entity converter found for model {model.GetType()}.");
    }

    return converter.ToEntity(model);
  }

  public object ToModel(object entity)
  {
    if (modelCache.TryGetValue(entity.GetType(), out var converter))
    {
      return converter.ToModel(entity);
    }

    converter = FindModelConverter(entity.GetType());

    modelCache.TryAdd(entity.GetType(), converter);

    if (!converter.CanConvertToModel(entity.GetType()))
    {
      throw new InvalidOperationException(
        $"No model converter found for entity {entity.GetType()}.");
    }

    return converter.ToModel(entity);
  }

  private IModelEntityConverter FindEntityConverter(Type type)
  {
    return serviceProvider
      .GetServices<IModelEntityConverter>()
      .Where(converter => type.IsAssignableTo(converter.ModelType))
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.ModelType.IsAssignableTo(acc.ModelType)
            ? next
            : acc)
      ?? serviceProvider
          .GetServices<IModelEntityConverter>()
          .Where(converter => converter.ModelType.IsAssignableTo(type))
          .DefaultIfEmpty(null)
          .Aggregate((acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for model {type}.");
  }

  private IModelEntityConverter FindModelConverter(Type type)
  {
    return serviceProvider
      .GetServices<IModelEntityConverter>()
      .Where(converter => type.IsAssignableTo(converter.EntityType))
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.EntityType.IsAssignableTo(acc.EntityType)
            ? next
            : acc)
      ?? serviceProvider
          .GetServices<IModelEntityConverter>()
          .Where(converter => converter.EntityType.IsAssignableTo(type))
          .DefaultIfEmpty(null)
          .Aggregate((acc, next) =>
            acc is null
              ? null
              : next!.EntityType.IsAssignableTo(acc.EntityType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for entity {type}.");
  }

  private readonly
    ConcurrentDictionary<Type, IModelEntityConverter> entityCache = new();

  private readonly
    ConcurrentDictionary<Type, IModelEntityConverter> modelCache = new();
}
