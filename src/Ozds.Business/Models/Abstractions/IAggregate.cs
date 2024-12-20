using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IMeasurement
{
  public IntervalModel Interval { get; }

  public long Count { get; }

  public long QuarterHourCount { get; }

  public TariffMeasure<decimal> DerivedActivePower_W { get; }

  public TariffMeasure<decimal> DerivedReactivePower_VAR { get; }

  public TariffMeasure<decimal> DerivedApparentPower_VA { get; }
}
