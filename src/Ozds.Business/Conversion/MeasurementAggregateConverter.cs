using System.Collections.Concurrent;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion;

public class MeasurementAggregateConverter(
  IServiceProvider serviceProvider)
{
  public TAggregate ToAggregate<TAggregate>(
    IMeasurement model,
    IntervalModel interval)
    where TAggregate : class, IAggregate
  {
    return (TAggregate)ToAggregate(model, interval);
  }

  public IAggregate ToAggregate(
    IMeasurement measurement,
    IntervalModel interval
  )
  {
    if (cache.TryGetValue(measurement.GetType(), out var converter))
    {
      return converter.ToAggregate(measurement, interval);
    }

    converter = serviceProvider
        .GetServices<IMeasurementAggregateConverter>()
        .FirstOrDefault(
          converter =>
            converter.CanConvertToAggregate(measurement.GetType()))
      ?? throw new InvalidOperationException(
        $"No converter found for measurement type {measurement.GetType()}.");

    cache.TryAdd(measurement.GetType(), converter);

    return converter.ToAggregate(measurement, interval);
  }

  private readonly
    ConcurrentDictionary<Type, IMeasurementAggregateConverter> cache = new();
}
