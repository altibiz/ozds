using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Base;

public abstract class
  ConcreteMeasurementAggregateConverter<TMeasurement, TAggregate> :
  InitializingMeasurementAggregateConverter
  where TAggregate : IAggregate
  where TMeasurement : IMeasurement
{
  public abstract void Initialize(
    TAggregate aggregate,
    TMeasurement measurement,
    IntervalModel interval);

  public override Type AggregateType => typeof(TAggregate);

  public override Type MeasurementType => typeof(TMeasurement);

  public override bool CanConvertToAggregate(Type measurement)
  {
    return measurement.IsAssignableTo(typeof(TMeasurement));
  }

  public override IAggregate Box()
  {
    return Activator.CreateInstance<TAggregate>();
  }

  public override void Initialize(
    IAggregate aggregate,
    IMeasurement measurement,
    IntervalModel interval)
  {
    Initialize(
      (TAggregate)aggregate,
      (TMeasurement)measurement,
      interval);
  }
}
