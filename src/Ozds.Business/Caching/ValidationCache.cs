using Ozds.Business.Caching.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Caching;

public class ValidationCache(
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
    var meter = await queries.ReadMeterByMeasurementValidator(
      value.Id,
      cancellationToken);
    return meter?.Id;
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
      .ReadMeasurementValidatorByMeter(key, cancellationToken);
    return model;
  }
}
