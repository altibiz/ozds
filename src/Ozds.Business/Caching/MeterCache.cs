using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MeterCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, IMeter>
{
  protected override Task<string?> GetKeyFromDataSourceAsync(
    IMeter value,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(value.Id)!;
  }

  protected override async Task<IMeter?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<AuditableQueries>();
    var model = await queries.ReadById<IMeter>(key, cancellationToken);
    return model;
  }
}
