using System.Collections.Concurrent;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming;

public class MeterNamingConvention(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<
    string,
    IMeterNamingConvention
  > idCache = new();

  private readonly ConcurrentDictionary<
    Type,
    IMeterNamingConvention
  > typeCache = new();

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

  public ICapabilities CapabilitiesForMeterId(string meterId)
  {
    return GetMeterNamingConvention(meterId).Capabilities;
  }

  public string IdPrefixForMeterType(Type meterType)
  {
    return GetMeterNamingConvention(meterType).IdPrefix;
  }

  private IMeterNamingConvention GetMeterNamingConvention(string meterId)
  {
    var meterIdPrefix = string.Join('-', meterId.Split('-').SkipLast(1));

    if (idCache.TryGetValue(meterIdPrefix, out var meterNamingConvention))
    {
      return meterNamingConvention;
    }

    meterNamingConvention =
      serviceProvider
        .GetServices<IMeterNamingConvention>()
        .FirstOrDefault(service => meterIdPrefix == service.IdPrefix)
      ?? throw new InvalidOperationException(
        $"No MeterNamingConvention found for {meterId}"
      );

    idCache.TryAdd(meterId, meterNamingConvention);

    return meterNamingConvention;
  }

  private IMeterNamingConvention GetMeterNamingConvention(Type meterType)
  {
    if (typeCache.TryGetValue(meterType, out var meterNamingConvention))
    {
      return meterNamingConvention;
    }

    meterNamingConvention =
      serviceProvider
        .GetServices<IMeterNamingConvention>()
        .FirstOrDefault(service => meterType == service.MeterType)
      ?? throw new InvalidOperationException(
        $"No MeterNamingConvention found for {meterType}"
      );

    typeCache.TryAdd(meterType, meterNamingConvention);

    return meterNamingConvention;
  }
}
