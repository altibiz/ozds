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

  protected override async Task<IReadOnlyCollection<string?>>
    GetKeysFromDataSourceAsync(
      IReadOnlyCollection<IMessenger> values,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeterQueries>();
    var meters = await queries.ReadByMessengerIdsOrdered(
      values.Select(x => x.Id),
      cancellationToken
    );
    return meters.Select(x => x?.Id).ToList();
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

  protected override async Task<IReadOnlyCollection<IMessenger?>>
    GetValuesFromDataSourceAsync(
      IReadOnlyCollection<string> keys,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MessengerQueries>();
    return await queries.ReadByMeterIdsOrdered(keys, cancellationToken);
  }
}
