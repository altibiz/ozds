using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataEventQueries = Ozds.Data.Queries.EventQueries;

namespace Ozds.Business.Queries;

public class EventQueries(
  DataEventQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<T>> Read<T>(
    LevelModel minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IEvent
  {
    var entityType = modelEntityConverter.EntityType(typeof(T));

    var minLevelEntity = minLevel.ToEntity();

    var models = await queries.Read(
      entityType,
      minLevelEntity,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return models.Items
      .Select(modelEntityConverter.ToModel<T>)
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type modelType,
    LevelModel minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(IEvent)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IEvent)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);

    var minLevelEntity = minLevel.ToEntity();

    var entities = await queries.Read(
      entityType,
      minLevelEntity,
      pageNumber,
      cancellationToken,
      pageCount);

    return entities.Items
      .Select(modelEntityConverter.ToModel<object>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<T>> ReadAuditEvents<T>(
    IAuditable auditable,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAuditEvent
  {
    var entities = await ReadAuditEventsDynamic(
      typeof(T),
      auditable,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .OfType<T>()
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<IAuditEvent>> ReadAuditEventsDynamic(
    Type modelType,
    IAuditable auditable,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(IAuditEvent)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditEvent)}");
    }

    var entityType = modelEntityConverter.EntityType(modelType);

    var auditableEntity = modelEntityConverter
      .ToEntity<IAuditableEntity>(auditable);

    var entities = await queries.ReadAuditEventsDynamic(
      entityType,
      auditableEntity,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<IAuditEvent>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<MessengerEventModel?> ReadLastByMessengerId(
    string id,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadLastByMessengerId(id, cancellationToken);

    return entity is null
      ? null
      : modelEntityConverter.ToModel<MessengerEventModel>(entity);
  }
}
