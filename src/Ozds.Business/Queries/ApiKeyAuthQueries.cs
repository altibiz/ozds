using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Composite;
using CachingCompositeMutations = Ozds.Caching.Mutations.CompositeEntityMutations;
using CachingCompositeQueries = Ozds.Caching.Queries.CompositeEntityQueries;
using DataApiKeyAuthQueries = Ozds.Data.Queries.ApiKeyAuthQueries;

namespace Ozds.Business.Queries;

public class ApiKeyAuthQueries(
  DataApiKeyAuthQueries queries,
  ModelEntityConverter modelEntityConverter,
  CachingCompositeQueries cachingCompositeQueries,
  CachingCompositeMutations cachingCompositeMutations,
  ModelCachingEntityConverter modelCachingEntityConverter
) : IQueries
{
  public async Task<ApiKeyAuthModel?> ReadByApiKeyIdAndScopeId(
    string? apiKeyId,
    string? scopeId,
    CancellationToken cancellationToken
  )
  {
    var cachedEntity = apiKeyId is null
      ? null
      : await cachingCompositeQueries.Read<ApiKeyAuthEntity>(
          apiKeyId,
          cancellationToken);

    var cachedModel = cachedEntity is null
      ? null
      : new ApiKeyAuthModel
      {
        ApiKey = modelCachingEntityConverter
          .ToModel<ApiKeyModel>(cachedEntity.ApiKey),
        Scopes = cachedEntity.Scopes
          .Select(modelCachingEntityConverter.ToModel<ScopeModel>)
          .ToList(),
        Registers = cachedEntity.Registers
          .GroupBy(x => x.ScopeId)
          .ToDictionary(
            x => x.Key,
            x => x
              .Select(modelCachingEntityConverter.ToModel<RegisterModel>)
              .ToList())
      };

    if (cachedModel is not null)
    {
      return cachedModel;
    }

    var entity = await queries.ReadByApiKeyIdAndScopeId(
      apiKeyId,
      scopeId,
      cancellationToken);
    if (entity is null)
    {
      return default;
    }

    var model = new ApiKeyAuthModel
    {
      ApiKey = modelEntityConverter
        .ToModel<ApiKeyModel>(entity.ApiKey),
      Scopes = entity.Scopes
        .Select(modelEntityConverter.ToModel<ScopeModel>)
        .ToList(),
      Registers = entity.Registers
        .ToDictionary(
          x => x.Key,
          x => x.Value
            .Select(modelEntityConverter.ToModel<RegisterModel>)
            .ToList())
    };

    var cachingEntity = new ApiKeyAuthEntity
    {
      ApiKey = modelCachingEntityConverter
        .ToEntity<ApiKeyEntity>(model.ApiKey),
      Scopes = model.Scopes
        .Select(modelCachingEntityConverter.ToEntity<ScopeEntity>)
        .ToList(),
      Registers = model.Registers
        .SelectMany(x => x.Value)
        .Select(modelCachingEntityConverter.ToEntity<RegisterEntity>)
        .ToList()
    };

    await cachingCompositeMutations.Create(cachingEntity, cancellationToken);

    return model;
  }
}
