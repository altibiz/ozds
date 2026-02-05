using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class ScopeQueries(IDbContextFactory<DataDbContext> factory) : IQueries
{
  public async Task<PaginatedList<ScopeEntity>> ReadByApiKeyId(
    string apiKeyId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = context
      .ApiKeyScopes.Where(
        context.ForeignKeyEquals<ApiKeyScopeEntity>(
          nameof(ApiKeyScopeEntity.ApiKey),
          apiKeyId
        )
      )
      .Include(x => x.Scope)
      .Select(x => x.Scope);

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
