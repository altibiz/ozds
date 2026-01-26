using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public class ApiKeyQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector entityReflector
) : IQueries
{
  public async Task<PaginatedList<ApiKeyEntity>> ReadByRepresentativeId(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    // NOTE: filtering only by table name because potential TPH
    var filtered = context.ApiKeys
      .Where(x => x.PrincipalEntityTable == entityReflector
        .ResolveEntityTable(typeof(RepresentativeEntity)))
      .Where(x => x.PrincipalEntityId == representativeId);

    filtered = deleted
      ? filtered.Where(x => x.IsDeleted)
      : filtered.Where(x => !x.IsDeleted);

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn)
      .OrderByDescending(x => x.DeletedOn);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }

  public async Task<PaginatedList<ApiKeyEntity>> ReadByMessengerId(
    string messengerId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    // NOTE: filtering only by table name because potential TPH
    var filtered = context.ApiKeys
      .Where(x => x.PrincipalEntityTable == entityReflector
        .ResolveEntityTable(typeof(MessengerEntity)))
      .Where(x => x.PrincipalEntityId == messengerId);

    filtered = deleted
      ? filtered.Where(x => x.IsDeleted)
      : filtered.Where(x => !x.IsDeleted);

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn)
      .OrderByDescending(x => x.DeletedOn);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }
}
