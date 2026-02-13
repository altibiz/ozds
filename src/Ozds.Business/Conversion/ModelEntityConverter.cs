using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion;

public class ModelEntityConverter(IServiceProvider serviceProvider)
{
  private readonly
    ConcurrentDictionary<Type, IModelEntityConverter> entityCache = new();

  private readonly
    ConcurrentDictionary<Type, IModelEntityConverter> modelCache = new();

  public TEntity ToEntity<TEntity>(object model)
  {
    return (TEntity)ToEntity(model);
  }

  public object ToEntity(object model)
  {
    var converter = GetEntityConverterForConversion(model.GetType());
    return converter.ToEntity(model);
  }

  public IEnumerable<TEntity> ToEntities<TEntity>(IEnumerable<object> models)
  {
    return ToEntities(models).OfType<TEntity>();
  }

  public IEnumerable<object> ToEntities(IEnumerable<object> models)
  {
    var enumerator = models.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetEntityConverterForConversion(current.GetType());

    yield return converter.ToEntity(current);

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetEntityConverterForConversion(next.GetType());
        current = next;
      }

      yield return converter.ToEntity(next);
    }
  }

  public IAsyncEnumerable<TEntity> ToEntities<TEntity>(
    IAsyncEnumerable<object> models,
    CancellationToken cancellationToken
  )
  {
    return ToEntities(models, cancellationToken).OfType<TEntity>();
  }

  public async IAsyncEnumerable<object> ToEntities(
    IAsyncEnumerable<object> models,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = models.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync(cancellationToken))
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetEntityConverterForConversion(current.GetType());

    yield return converter.ToEntity(current);

    while (await enumerator.MoveNextAsync(cancellationToken))
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetEntityConverterForConversion(next.GetType());
        current = next;
      }

      yield return converter.ToEntity(next);
    }
  }

  public TModel ToModel<TModel>(object entity)
  {
    return (TModel)ToModel(entity);
  }

  public object ToModel(object entity)
  {
    var converter = GetModelConverterForConversion(entity.GetType());
    return converter.ToModel(entity);
  }

  public IEnumerable<TModel> ToModels<TModel>(IEnumerable<object> entities)
  {
    return ToModels(entities).OfType<TModel>();
  }

  public IEnumerable<object> ToModels(IEnumerable<object> entities)
  {
    var enumerator = entities.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetModelConverterForConversion(current.GetType());

    yield return converter.ToModel(current);

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetModelConverterForConversion(next.GetType());
        current = next;
      }

      yield return converter.ToModel(next);
    }
  }

  public IAsyncEnumerable<TModel> ToModels<TModel>(
    IAsyncEnumerable<object> entities,
    CancellationToken cancellationToken
  )
  {
    return ToModels(entities, cancellationToken).OfType<TModel>();
  }

  public async IAsyncEnumerable<object> ToModels(
    IAsyncEnumerable<object> entities,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = entities.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetModelConverterForConversion(current.GetType());

    yield return converter.ToModel(current);

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetModelConverterForConversion(next.GetType());
        current = next;
      }

      yield return converter.ToModel(next);
    }
  }

  public Type EntityType(Type type)
  {
    var converter = GetEntityConverter(type);
    return converter.EntityType;
  }

  public Type ModelType(Type type)
  {
    var converter = GetModelConverter(type);
    return converter.ModelType;
  }

  private IModelEntityConverter GetEntityConverterForConversion(
    Type type
  )
  {
    var converter = GetEntityConverter(type);

    if (!converter.CanConvertToEntity(type))
    {
      throw new InvalidOperationException(
        $"No entity converter found for model {type}.");
    }

    return converter;
  }

  private IModelEntityConverter GetEntityConverter(Type type)
  {
    if (modelCache.TryGetValue(type, out var converter))
    {
      return converter;
    }

    converter = serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(converter => type.IsAssignableTo(converter.ModelType))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? next
                : acc)
      ?? serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(converter => converter.ModelType.IsAssignableTo(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for model {type}.");

    modelCache.TryAdd(type, converter);

    return converter;
  }

  private IModelEntityConverter GetModelConverterForConversion(
    Type type
  )
  {
    var converter = GetModelConverter(type);

    if (!converter.CanConvertToModel(type))
    {
      throw new InvalidOperationException(
        $"No model converter found for entity {type}.");
    }

    return converter;
  }

  private IModelEntityConverter GetModelConverter(Type type)
  {
    if (entityCache.TryGetValue(type, out var converter))
    {
      return converter;
    }

    converter = serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(converter => type.IsAssignableTo(converter.EntityType))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.EntityType.IsAssignableTo(acc.EntityType)
                ? next
                : acc)
      ?? serviceProvider
        .GetServices<IModelEntityConverter>()
        .Where(converter => converter.EntityType.IsAssignableTo(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.EntityType.IsAssignableTo(acc.EntityType)
                ? acc
                : next)
      ?? throw new InvalidOperationException(
        $"No converter found for entity {type}.");

    entityCache.TryAdd(type, converter);

    return converter;
  }
}
