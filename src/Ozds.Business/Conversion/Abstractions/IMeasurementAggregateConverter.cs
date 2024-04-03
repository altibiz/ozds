using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Abstractions;

public interface IMeasurementAggregateConverter
{
  bool CanConvertToAggregate(Type measurement);

  IAggregate ToAggregate(IMeasurement measurement, IntervalModel interval);
}
