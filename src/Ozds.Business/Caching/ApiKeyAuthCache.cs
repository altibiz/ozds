using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class ApiKeyAuthCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, ApiKeyAuthModel>
{
  protected override Task<string?> GetKeyFromDataSourceAsync(
    ApiKeyAuthModel value,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(value.ApiKey.Id)!;
  }

  protected override async Task<ApiKeyAuthModel?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<ApiKeyAuthQueries>();
    var apiKeyAuth = await queries.ReadByApiKeyIdAndScopeId(
      key,
      null,
      cancellationToken
    );
    return apiKeyAuth;
  }
}
