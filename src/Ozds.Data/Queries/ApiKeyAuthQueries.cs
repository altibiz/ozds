using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class ApiKeyAuthQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<ApiKeyAuthEntity?> ReadByApiKeyIdAndScopeId(
    string? apiKeyId,
    string? scopeId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var initialApiKeys = context.ApiKeys.AsQueryable();

    if (apiKeyId is not null)
    {
      initialApiKeys = initialApiKeys
        .Where(context.PrimaryKeyEquals<ApiKeyEntity>(apiKeyId));
    }

    var apiKey = await initialApiKeys
      .Include(apiKey => apiKey.Scopes)
      .AsSingleQuery()
      .FirstOrDefaultAsync(cancellationToken);
    if (apiKey is null)
    {
      return default;
    }

    var scopes = scopeId is null
      ? apiKey.Scopes
        .ToList()
      : apiKey.Scopes
        .Where(x => x.Id == scopeId)
        .ToList();

    var measurementScopes = await context.Scopes
      .OfType<MeasurementScopeEntity>()
      .Where(
        context.PrimaryKeyIn<MeasurementScopeEntity>(
          scopes.Select(x => x.Id)))
      .Include(x => x.Registers)
      .AsSingleQuery()
      .ToListAsync(cancellationToken);

    return new ApiKeyAuthEntity
    {
      ApiKey = apiKey,
      Scopes = scopes,
      Registers = measurementScopes
        .ToDictionary(
          x => x.Id,
          x => x.Registers.ToList())
    };
  }
}
