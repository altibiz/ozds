using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataTrackableQueries = Ozds.Data.Queries.TrackableQueries;

namespace Ozds.Business.Queries;

public class TrackableQueries(
  DataTrackableQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadById<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, ITrackableIdentifiable
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
    if (!modelType.IsAssignableTo(typeof(ITrackableIdentifiable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(ITrackableIdentifiable)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entity = await queries.ReadById(
      entityType, id, cancellationToken);
    if (entity is null)
    {
      return default;
    }

    var model = modelEntityConverter.ToModel(entity);
    return model;
  }

  public async Task<List<T>> ReadByIds<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
    where T : class, ITrackableIdentifiable
  {
    var models = await ReadByIds(
      typeof(T),
      ids,
      cancellationToken,
      deleted
    );
    return models.OfType<T>().ToList();
  }

  public async Task<List<object>> ReadByIds(
    Type modelType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    if (!modelType.IsAssignableTo(typeof(ITrackableIdentifiable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(ITrackableIdentifiable)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadByIds(
      entityType,
      ids,
      cancellationToken,
      deleted
    );

    return entities
      .Select(modelEntityConverter.ToModel)
      .ToList();
  }

  public async Task<List<T?>> ReadByIdsOrdered<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
    where T : class, ITrackableIdentifiable
  {
    var models = await ReadByIdsOrdered(
      typeof(T),
      ids,
      cancellationToken,
      deleted
    );
    return models.Cast<T?>().ToList();
  }

  public async Task<List<object?>> ReadByIdsOrdered(
    Type modelType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    if (!modelType.IsAssignableTo(typeof(ITrackableIdentifiable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(ITrackableIdentifiable)}");
    }

    var entities = await queries.ReadByIdsOrdered(
      modelEntityConverter.EntityType(modelType),
      ids,
      cancellationToken,
      deleted
    );

    return entities
      .Select(
        entity => entity is null
          ? null
          : modelEntityConverter.ToModel(entity))
      .ToList();
  }

  public async Task<PaginatedList<T>> ReadByTitle<T>(
    string title,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
    where T : class, ITrackableIdentifiable
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
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    var entityType = modelEntityConverter.EntityType(modelType);

    var page = await queries.ReadByTitle(
      entityType,
      title,
      pageNumber,
      cancellationToken,
      pageCount,
      deleted
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
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
    where T : class, ITrackable
  {
    var models = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount,
      deleted
    );

    return models.Items
      .OfType<T>()
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type modelType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    if (!modelType.IsAssignableTo(typeof(ITrackable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(ITrackable)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.Read(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount,
      deleted
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel)
      .ToPaginatedList(entities.TotalCount);
  }
}
