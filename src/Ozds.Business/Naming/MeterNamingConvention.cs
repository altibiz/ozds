using System.Collections.Concurrent;
using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Agnostic;

public class MeterNamingConvention(IServiceProvider serviceProvider)
{
  public Type MeasurementTypeForMeterId(string meterId)
  {
    return GetMeterNamingConvention(meterId).MeasurementType;
  }

  public Type AggregateTypeForMeterId(string meterId)
  {
    return GetMeterNamingConvention(meterId).AggregateType;
  }

  public Type ValidatorTypeForMeterId(string meterId)
  {
    return GetMeterNamingConvention(meterId).MeasurementValidatorType;
  }

  public Type MeterTypeForMeterId(string meterId)
  {
    return GetMeterNamingConvention(meterId).MeterType;
  }

  private IMeterNamingConvention GetMeterNamingConvention(
    string meterId
  )
  {
    var meterIdPrefix = string.Join('-', meterId.Split('-').SkipLast(1));

    if (cache.TryGetValue(meterIdPrefix, out var meterNamingConvention))
    {
      return meterNamingConvention;
    }

    meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>()
      .FirstOrDefault(service => meterIdPrefix == service.IdPrefix)
      ?? throw new InvalidOperationException(
        $"No MeterNamingConvention found for {meterId}");

    cache.TryAdd(meterId, meterNamingConvention);

    return meterNamingConvention;
  }

  private readonly ConcurrentDictionary<string, IMeterNamingConvention> cache =
    new();
}
