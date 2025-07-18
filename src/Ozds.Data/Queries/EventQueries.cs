using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class EventQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<T>> Read<T>(
    LevelEntity minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IEventEntity
  {
    var entities = await Read(
      typeof(T),
      minLevel,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type entityType,
    LevelEntity minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!entityType.IsAssignableTo(typeof(IEventEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IEventEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.Events
      .Where(x => x.Level >= minLevel);

    var ordered = filtered
      .OrderBy(context.PrimaryKeyOf(entityType));

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }

  public async Task<PaginatedList<object>> ReadAuditEventsDynamic(
    Type entityType,
    IAuditableEntity auditableEntity,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    var auditableEntityId = auditableEntity.Id;
    var auditableEntityType = await ReadAuditEntityTypeName(
      auditableEntity.GetType(),
      cancellationToken);
    var auditableEntityTable = await ReadAuditEntityTableName(
      auditableEntityType.GetType(),
      cancellationToken
    );

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.Events
      .OfType<AuditEventEntity>()
      .Where(
        x =>
          x.AuditableEntityId == auditableEntityId
          && x.AuditableEntityType == auditableEntityType
          && x.AuditableEntityTable == auditableEntityTable);

    var ordered = filtered
      .OrderBy(context.PrimaryKeyOf(entityType));

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }

  public async Task<MessengerEventEntity?> ReadLastByMessengerId(
    string id,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var messengerEvent = await context.Events
      .OfType<MessengerEventEntity>()
      .Where(
        context.ForeignKeyEquals<MessengerEventEntity>(
          nameof(MessengerEventEntity.Messenger),
          id
        )
      )
      .OrderByDescending(x => x.Timestamp)
      .FirstOrDefaultAsync(cancellationToken);
    return messengerEvent;
  }

  public async Task<string> ReadAuditEntityTypeName(
    Type entityType,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return context.GetEntityTypeNameFromEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a type name");
  }

  public async Task<string> ReadAuditEntityTableName(
    Type entityType,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return context.GetTableNameFromEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a table");
  }

  public async Task<Type> ReadAuditEntityType(
    string entityTypeName,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return context.GetEntityTypeFromEntityTypeName(entityTypeName)
      ?? throw new InvalidOperationException(
        $"Type {entityTypeName} doesn't have a type");
  }
}
