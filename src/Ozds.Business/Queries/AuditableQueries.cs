using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataAuditableQueries = Ozds.Data.Queries.AuditableQueries;
using DataEntityQueries = Ozds.Data.Queries.EntityQueries;
using DataEventQueries = Ozds.Data.Queries.EventQueries;

namespace Ozds.Business.Queries;

public class AuditableQueries(
  DataAuditableQueries queries,
  DataEventQueries eventQueries,
  DataEntityQueries entityQueries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadById<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditable
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
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
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
    where T : class, IAuditable
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
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
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

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
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
    if (!modelType.IsAssignableTo(typeof(IAuditable)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditable)}");
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

  public async Task<IAuditable?> ReadByEvent(
    IAuditEvent auditEvent,
    CancellationToken cancellationToken
  )
  {
    var original = await entityQueries.ReadById<IAuditEventEntity>(
      auditEvent.Id,
      cancellationToken);
    if (original is null)
    {
      return null;
    }

    var type = await eventQueries.ReadAuditEntityType(
      original.AuditableEntityType,
      cancellationToken);
    var entity = await queries.ReadById(
      type,
      original.AuditableEntityId,
      cancellationToken);

    return entity is null
      ? null
      : modelEntityConverter.ToModel<IAuditable>(entity);
  }
}
