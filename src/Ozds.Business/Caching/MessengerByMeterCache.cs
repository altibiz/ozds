using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MessengerByMeterCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, IMessenger>
{
  protected override async Task<string?> GetKeyFromDataSourceAsync(
    IMessenger value,
    CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeterQueries>();
    var meter = await queries.ReadByMessengerId(
      value.Id,
      cancellationToken
    );
    return meter?.Id;
  }

  protected override async Task<IMessenger?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MessengerQueries>();
    var messenger = await queries.ReadByMeterId(
      key,
      cancellationToken
    );
    return messenger;
  }
}
