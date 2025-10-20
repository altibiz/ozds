using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MeterCache(
  IServiceScopeFactory factory
) : BatchedConcurrentDictionaryCacheBase<string, IMeter>
{
  protected override Task<string?> GetKeyFromDataSourceAsync(
    IMeter value,
    CancellationToken cancellationToken)
  {
    return Task.FromResult(value.Id)!;
  }

  protected override Task<IReadOnlyCollection<string?>>
    GetKeysFromDataSourceAsync(
      IReadOnlyCollection<IMeter> values,
      CancellationToken cancellationToken)
  {
    return Task.FromResult(
      values
          .Select(x => x.Id)
          .Cast<string?>()
          .ToList()
        as IReadOnlyCollection<string?>);
  }

  protected override async Task<IMeter?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<IdentifiableQueries>();
    var model = await queries.ReadById<IMeter>(key, cancellationToken);
    return model;
  }

  protected override async Task<IReadOnlyCollection<IMeter?>>
    GetValuesFromDataSourceAsync(
      IReadOnlyCollection<string> keys,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<IdentifiableQueries>();
    var models = await queries.ReadByIdsOrdered<IMeter>(
      keys, cancellationToken);
    return models;
  }
}
