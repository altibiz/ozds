using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MeasurementValidatorByMeterCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, IMeasurementValidator>
{
  protected override async Task<string?> GetKeyFromDataSourceAsync(
    IMeasurementValidator value,
    CancellationToken cancellationToken
  )
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<ValidationQueries>();
    var meter = await queries.ReadMeterByMeasurementValidatorId(
      value.Id,
      cancellationToken);
    return meter?.Id;
  }

  protected override async Task<IReadOnlyCollection<string?>>
    GetKeysFromDataSourceAsync(
      IReadOnlyCollection<IMeasurementValidator> values,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<ValidationQueries>();
    var meters = await queries.ReadMetersByMeasurementValidatorIdsOrdered(
      values.Select(x => x.Id),
      cancellationToken);
    return meters.Select(x => x?.Id).ToList();
  }

  protected override async Task<IMeasurementValidator?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken
    )
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<ValidationQueries>();
    var model = await queries
      .ReadMeasurementValidatorByMeterId(key, cancellationToken);
    return model;
  }

  protected override async Task<IReadOnlyCollection<IMeasurementValidator?>>
    GetValuesFromDataSourceAsync(
      IReadOnlyCollection<string> keys,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<ValidationQueries>();
    var models = await queries
      .ReadMeasurementValidatorsByMeterIdsOrdered(keys, cancellationToken);
    return models;
  }
}
