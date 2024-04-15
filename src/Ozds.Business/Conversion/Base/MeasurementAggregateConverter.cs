using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Base;

public abstract class
  MeasurementAggregateConverter<TMeasurement, TAggregate> :
  IMeasurementAggregateConverter
  where TAggregate : class, IAggregate
  where TMeasurement : class, IMeasurement
{
  public bool CanConvertToAggregate(Type measurement)
  {
    return measurement.IsAssignableTo(typeof(TMeasurement));
  }

  public IAggregate ToAggregate(IMeasurement measurement,
    IntervalModel interval)
  {
    return ToAggregate(
      measurement as TMeasurement
      ?? throw new ArgumentNullException(nameof(measurement)),
      interval
    );
  }

  protected abstract TAggregate ToAggregate(TMeasurement measurement,
    IntervalModel interval);
}
