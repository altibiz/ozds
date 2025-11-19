using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using CachingIdentifiableMutations =
  Ozds.Caching.Mutations.IdentifiableEntityMutations;
using CachingIdentifiableQueries =
  Ozds.Caching.Queries.IdentifiableEntityQueries;
using DataIdentifiableQueries = Ozds.Data.Queries.IdentifiableQueries;

namespace Ozds.Business.Queries;

public class IdentifiableQueries(
  DataIdentifiableQueries queries,
  ModelEntityConverter modelEntityConverter,
  CachingIdentifiableMutations cachingMutations,
  CachingIdentifiableQueries cachingQueries,
  ModelCachingEntityConverter modelCachingEntityConverter
) : IQueries
{
  public async Task<T?> ReadById<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IIdentifiable
  {
    var model = await ReadById(typeof(T), id, cancellationToken);
    return model is null ? default : (T)model;
  }

  public async Task<object?> ReadById(
    Type modelType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IModel)}");
    }

    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      var cachedEntityType = modelCachingEntityConverter
        .EntityType(modelType);

      var cachedEntity = await cachingQueries.Read(
        cachedEntityType,
        id,
        cancellationToken
      );

      var cachedModel = cachedEntity is null
        ? null
        : modelCachingEntityConverter.ToModel(cachedEntity);

      if (cachedModel is not null)
      {
        return cachedModel;
      }
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = await queries.ReadById(
      entityType, id, cancellationToken);
    if (entity is null)
    {
      return default;
    }

    var model = modelEntityConverter.ToModel(entity);

    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      var cachingEntity = modelCachingEntityConverter
        .ToEntity<IIdentifiableEntity>(model);
      await cachingMutations.Create(cachingEntity, cancellationToken);
    }

    return model;
  }

  public async Task<List<T>> ReadByIds<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
    where T : class, IIdentifiable
  {
    var models = await ReadByIds(typeof(T), ids, cancellationToken);
    return models.OfType<T>().ToList();
  }

  public async Task<List<object>> ReadByIds(
    Type modelType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IModel)}");
    }

    var toFetch = ids.ToList();
    var result = new List<object>();

    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      var cachedEntityType = modelCachingEntityConverter
        .EntityType(modelType);

      var fromCache = toFetch.ToList();
      foreach (var id in fromCache)
      {
        var cachedEntity = await cachingQueries.Read(
          cachedEntityType,
          id,
          cancellationToken
        );
        if (cachedEntity is null)
        {
          continue;
        }

        var cachedModel = modelCachingEntityConverter.ToModel(cachedEntity);
        if (cachedModel is null)
        {
          continue;
        }

        result.Add(cachedModel);
        toFetch.Remove(id);
      }
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadByIds(
      entityType,
      toFetch,
      cancellationToken
    );

    var models = entities
      .Select(modelEntityConverter.ToModel)
      .ToList();
    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      foreach (var model in models)
      {
        var cachedEntity = modelCachingEntityConverter
          .ToEntity<IIdentifiableEntity>(model);
        await cachingMutations.Create(cachedEntity, cancellationToken);
      }
    }

    result.AddRange(models);

    return result;
  }

  public async Task<List<T?>> ReadByIdsOrdered<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
    where T : class, IIdentifiable
  {
    var models = await ReadByIdsOrdered(
      typeof(T),
      ids,
      cancellationToken
    );
    return models.Cast<T?>().ToList();
  }

  public async Task<List<object?>> ReadByIdsOrdered(
    Type modelType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(IIdentifiable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IIdentifiable)}");
    }

    var toFetch = ids.ToList();
    var result = toFetch.Select(x => (object?)null).ToList();

    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      var cachedEntityType = modelCachingEntityConverter
        .EntityType(modelType);

      var fromCache = toFetch.ToList();
      for (var i = 0; i < fromCache.Count; i++)
      {
        var cachedEntity = await cachingQueries.Read(
          cachedEntityType,
          fromCache[i],
          cancellationToken
        );
        if (cachedEntity is null)
        {
          continue;
        }

        var cachedModel = modelCachingEntityConverter.ToModel(cachedEntity);

        if (cachedModel is not null)
        {
          result[i] = cachedModel;
          toFetch.RemoveAt(i);
        }
      }
    }

    var entities = await queries.ReadByIdsOrdered(
      modelEntityConverter.EntityType(modelType),
      toFetch,
      cancellationToken
    );

    var models = entities
      .Select(
        entity => entity is null
          ? null
          : modelEntityConverter.ToModel(entity))
      .ToList();
    if (modelType.IsAssignableTo(typeof(ICachedIdentifiable)))
    {
      foreach (var model in models)
      {
        if (model is null)
        {
          continue;
        }

        var cachedEntity = modelCachingEntityConverter
          .ToEntity<IIdentifiableEntity>(model);
        await cachingMutations.Create(cachedEntity, cancellationToken);
      }
    }

    var j = 0;
    for (var i = 0; i < toFetch.Count && j < result.Count; i++)
    {
      while (j < result.Count && result[j] is not null)
      {
        j++;
      }

      if (j >= result.Count)
      {
        break;
      }

      result[j] = models[i];
    }

    return result;
  }

  public async Task<PaginatedList<T>> ReadByTitle<T>(
    string title,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IIdentifiable
  {
    var models = await ReadByTitle(
      typeof(T),
      title,
      pageNumber,
      cancellationToken,
      pageCount);

    return models.Items.OfType<T>().ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> ReadByTitle(
    Type modelType,
    string title,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var entityType = modelEntityConverter.EntityType(modelType);

    var page = await queries.ReadByTitle(
      entityType,
      title,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var models = page.Items
      .Select(modelEntityConverter.ToModel)
      .ToList();

    return models
      .ToPaginatedList(page.TotalCount);
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IIdentifiable
  {
    var models = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount);

    return models.Items.OfType<T>().ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type modelType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var entityType = modelEntityConverter.EntityType(modelType);

    var page = await queries.Read(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var models = page.Items
      .Select(modelEntityConverter.ToModel)
      .ToList();

    return models
      .ToPaginatedList(page.TotalCount);
  }
}
