using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public class EventQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector entityReflector
) : IQueries
{
  public async Task<PaginatedList<T>> Read<T>(
    LevelEntity minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    string? title = null
  )
    where T : class, IEventEntity
  {
    var entities = await Read(
      typeof(T),
      minLevel,
      pageNumber,
      cancellationToken,
      pageCount,
      title
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type entityType,
    LevelEntity minLevel,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    string? title = null
  )
  {
    if (!entityType.IsAssignableTo(typeof(IEventEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IEventEntity)}"
      );
    }

    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = context.Events.Where(x => x.Level >= minLevel);

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered.OrderBy(context.PrimaryKeyOf(entityType));

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }

  public async Task<PaginatedList<object>> ReadAuditEvents(
    Type entityType,
    IAuditableEntity auditableEntity,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    string? title = null
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditEventEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditEventEntity)}"
      );
    }

    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    // NOTE: filtering only by table name because potential TPH
    var auditableEntityId = auditableEntity.AuditingId;
    var auditableEntityTable = entityReflector.ResolveEntityTable(
      auditableEntity.GetType()
    );
    var filtered = context
      .Events.OfType<AuditEventEntity>()
      .Where(x =>
        x.AuditableEntityId == auditableEntityId
        && x.AuditableEntityTable == auditableEntityTable
      );

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered.OrderByDescending(x => x.Timestamp);

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
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var messengerEvent = await context
      .Events.OfType<MessengerEventEntity>()
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
}
