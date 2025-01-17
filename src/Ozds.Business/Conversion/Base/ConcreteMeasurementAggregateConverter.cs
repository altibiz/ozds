using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Base;

public abstract class
  ConcreteMeasurementAggregateConverter<TMeasurement, TAggregate> :
  IMeasurementAggregateConverter
  where TAggregate : IAggregate
  where TMeasurement : IMeasurement
{
  public abstract TAggregate ToAggregate(
    TMeasurement measurement,
    IntervalModel interval);

  public virtual Type AggregateType => typeof(TAggregate);

  public virtual Type MeasurementType => typeof(TMeasurement);

  public virtual bool CanConvertToAggregate(Type measurement)
  {
    return measurement.IsAssignableTo(typeof(TMeasurement));
  }

  public virtual IAggregate ToAggregate(
    IMeasurement measurement,
    IntervalModel interval)
  {
    return ToAggregate((TMeasurement)measurement, interval);
  }
}
