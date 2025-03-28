using Ozds.Business.Models.Complex;

namespace Ozds.Fake.Extensions;

public static class AggregateMeasureExtensions
{
  public static InstantaneousAggregateMeasureModel Clone(
    this InstantaneousAggregateMeasureModel measure
  )
  {
    return new InstantaneousAggregateMeasureModel
    {
      Avg = measure.Avg,
      Min = measure.Min,
      MinTimestamp = measure.MinTimestamp,
      Max = measure.Max,
      MaxTimestamp = measure.MaxTimestamp
    };
  }

  public static CumulativeAggregateMeasureModel Clone(
    this CumulativeAggregateMeasureModel measure
  )
  {
    return new CumulativeAggregateMeasureModel
    {
      Min = measure.Min,
      Max = measure.Max
    };
  }

  public static DerivedAggregateMeasureModel Clone(
    this DerivedAggregateMeasureModel measure
  )
  {
    return new DerivedAggregateMeasureModel
    {
      Avg = measure.Avg,
      Min = measure.Min,
      MinTimestamp = measure.MinTimestamp,
      Max = measure.Max,
      MaxTimestamp = measure.MaxTimestamp
    };
  }
}
