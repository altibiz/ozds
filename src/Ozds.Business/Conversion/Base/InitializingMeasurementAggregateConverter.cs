using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Base;

public abstract class InitializingMeasurementAggregateConverter
  : IMeasurementAggregateConverter
{
  public abstract Type AggregateType { get; }

  public abstract Type MeasurementType { get; }

  public abstract bool CanConvertToAggregate(Type measurement);

  public IAggregate ToAggregate(
    IMeasurement measurement,
    IntervalModel interval
  )
  {
    var aggregate = Box();
    Initialize(aggregate, measurement, interval);
    return aggregate;
  }

  public abstract IAggregate Box();

  public abstract void Initialize(
    IAggregate aggregate,
    IMeasurement measurement,
    IntervalModel interval);
}
