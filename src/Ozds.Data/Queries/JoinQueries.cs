using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class JoinQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public Task<PaginatedList<LocationRepresentativeEntity>>
    ReadRepresentativesForLocation(
      string locationId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<LocationRepresentativeEntity>(
      nameof(LocationRepresentativeEntity.Location),
      locationId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public Task<PaginatedList<LocationRepresentativeEntity>>
    ReadLocationsForRepresentative(
      string representativeId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<LocationRepresentativeEntity>(
      nameof(LocationRepresentativeEntity.Representative),
      representativeId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public Task<PaginatedList<NetworkUserRepresentativeEntity>>
    ReadRepresentativesForNetworkUser(
      string networkUserId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<NetworkUserRepresentativeEntity>(
      nameof(NetworkUserRepresentativeEntity.NetworkUser),
      networkUserId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public Task<PaginatedList<NetworkUserRepresentativeEntity>>
    ReadNetworkUsersForRepresentative(
      string representativeId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<NetworkUserRepresentativeEntity>(
      nameof(NetworkUserRepresentativeEntity.Representative),
      representativeId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public Task<PaginatedList<ApiKeyScopeEntity>>
    ReadScopesForApiKey(
      string apiKeyId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<ApiKeyScopeEntity>(
      nameof(ApiKeyScopeEntity.ApiKey),
      apiKeyId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public Task<PaginatedList<ApiKeyScopeEntity>>
    ReadApiKeysForScope(
      string scopeId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    return ReadAuditable<ApiKeyScopeEntity>(
      nameof(ApiKeyScopeEntity.Scope),
      scopeId,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  private async Task<PaginatedList<T>> ReadAuditable<T>(
    string property,
    string id,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAuditableEntity
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<T>();

    var filtered = queryable
      .Where(context.ForeignKeyEquals<T>(property, id));

    var ordered = filtered.OrderByDescending(x => x.CreatedOn);

    var total = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<T>().ToPaginatedList(total);
  }
}
