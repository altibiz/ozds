using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using DataAuditableQueries = Ozds.Data.Queries.AuditableQueries;
using DataReadonlyQueries = Ozds.Data.Queries.ReadonlyQueries;

namespace Ozds.Business.Queries;

public class EventQueries(
  DataReadonlyQueries queries,
  DataAuditableQueries auditableQueries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<T>> ReadByMinLevel<T>(
    LevelModel minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IEvent
  {
    var models = await ReadByMinLevelDynamic(
      typeof(T),
      minLevel,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return models.Items
      .OfType<T>()
      .ToPaginatedList(models.TotalCount);
  }

  public async Task<PaginatedList<IEvent>> ReadByMinLevelDynamic(
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

    var entities = await queries.ReadDynamic(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount: pageCount,
      where: entity => ((EventEntity)entity).Level >= minLevelEntity
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<IEvent>)
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
    var entityTableName = await auditableQueries
      .ReadEntityTableName(entityType, cancellationToken)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a table");
    var entityTypeName = await auditableQueries
      .ReadEntityTypeName(entityType, cancellationToken)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a type name");

    var entities = await queries.ReadDynamic(
      entityType,
      pageNumber,
      cancellationToken,
      pageCount: pageCount,
      where: entity =>
        ((AuditEventEntity)entity).AuditableEntityId == auditable.Id
        && ((AuditEventEntity)entity).AuditableEntityType == entityTypeName
        && ((AuditEventEntity)entity).AuditableEntityTable == entityTableName
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<IAuditEvent>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<IAuditable?> ReadAuditable(
    IAuditEvent auditEvent,
    CancellationToken cancellationToken
  )
  {
    var original = await queries.ReadSingle<IAuditEventEntity>(
      auditEvent.Id,
      cancellationToken);
    if (original is null)
    {
      return null;
    }

    var type = await auditableQueries.ReadEntityType(
      original.AuditableEntityType,
      cancellationToken);
    var entity = await auditableQueries.ReadSingleDynamic(
      type,
      original.AuditableEntityId,
      cancellationToken);

    return entity is null
      ? null
      : modelEntityConverter.ToModel<IAuditable>(entity);
  }
}
