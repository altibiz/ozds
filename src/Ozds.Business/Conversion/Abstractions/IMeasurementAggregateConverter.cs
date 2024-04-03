using System.ComponentModel;
using System.Globalization;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IMeasurementAggregateConverter
{
  public Type AggregateType { get; }

  public Type MeasurementType { get; }

  IMeasurement ToMeasurement(IAggregate aggregate);

  IAggregate ToAggregate(IMeasurement measurement);
}
