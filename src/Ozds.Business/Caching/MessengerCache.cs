using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MessengerCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, IMessenger>
{
  protected override Task<string?> GetKeyFromDataSourceAsync(
    IMessenger value,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(value.Id)!;
  }

  protected override async Task<IMessenger?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<AuditableQueries>();
    var model = await queries.ReadById<IMessenger>(key, cancellationToken);
    return model;
  }
}
