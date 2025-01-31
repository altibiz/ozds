using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class MeasurementLocationCache(
  IServiceScopeFactory factory
) : ConcurrentDictionaryCacheBase<string, IMeasurementLocation>
{
  protected override async Task<string?> GetKeyFromDataSourceAsync(
    IMeasurementLocation value,
    CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeasurementLocationQueries>();
    var meter = await queries.ReadMeterByMeasurementLocation(
      value.Id,
      cancellationToken
    );
    return meter?.Id;
  }

  protected override async Task<IMeasurementLocation?>
    GetValueFromDataSourceAsync(
      string key,
      CancellationToken cancellationToken)
  {
    await using var scope = factory.CreateAsyncScope();
    var queries = scope.ServiceProvider
      .GetRequiredService<MeasurementLocationQueries>();
    var measurementLocation = await queries.ReadMeasurementLocationByMeter(
      key,
      cancellationToken
    );
    return measurementLocation;
  }
}
