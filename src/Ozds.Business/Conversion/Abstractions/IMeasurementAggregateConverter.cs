using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Abstractions;

public interface IMeasurementAggregateConverter
{
  Type AggregateType { get; }

  Type MeasurementType { get; }

  bool CanConvertToAggregate(Type measurement);

  IAggregate ToAggregate(IMeasurement measurement, IntervalModel interval);
}
