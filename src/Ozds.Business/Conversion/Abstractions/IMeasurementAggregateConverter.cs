using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Abstractions;

public interface IMeasurementAggregateConverter
{
  public Type AggregateType { get; }

  public Type MeasurementType { get; }

  public bool CanConvertToAggregate(Type measurement);

  public IAggregate ToAggregate(IMeasurement measurement, IntervalModel interval);
}
