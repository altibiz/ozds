using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MeasurementLocationByMeterCache(
  IServiceScopeFactory factory
) : BatchedConcurrentDictionaryCacheBase<string, IMeasurementLocation>
{
  protected override async Task<string?> GetKeyFromDataSourceAsync(
    IMeasurementLocation value,
    CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeterQueries>();
    var meter = await queries.ReadByMeasurementLocationId(
      value.Id,
      cancellationToken
    );
    return meter?.Id;
  }

  protected override async Task<IReadOnlyCollection<string?>>
    GetKeysFromDataSourceAsync(
      IReadOnlyCollection<IMeasurementLocation> values,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeterQueries>();
    var meters = await queries.ReadByMeasurementLocationIdsOrdered(
      values.Select(x => x.Id),
      cancellationToken
    );
    return meters.Select(x => x?.Id).ToList();
  }

  protected override async Task<IMeasurementLocation?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeasurementLocationQueries>();
    var measurementLocation = await queries.ReadByMeterId(
      key,
      cancellationToken
    );
    return measurementLocation;
  }

  protected override async Task<IReadOnlyCollection<IMeasurementLocation?>>
    GetValuesFromDataSourceAsync(
      IReadOnlyCollection<string> keys,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeasurementLocationQueries>();
    var measurementLocation = await queries.ReadByMeterIdsOrdered(
      keys,
      cancellationToken
    );
    return measurementLocation;
  }
}
