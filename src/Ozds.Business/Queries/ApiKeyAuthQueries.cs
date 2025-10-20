using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using DataApiKeyAuthQueries = Ozds.Data.Queries.ApiKeyAuthQueries;

namespace Ozds.Business.Queries;

public class ApiKeyAuthQueries(
  DataApiKeyAuthQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<ApiKeyAuthModel?> ReadByApiKeyIdAndScopeId(
    string? apiKeyId,
    string? scopeId,
    CancellationToken cancellationToken
  )
  {
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

    return model;
  }
}
