using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Business.Aggregation.Implementations;

public class DerivedAggregateMeasureUpserter
  : ConcreteAggregateMeasureUpserter<DerivedAggregateMeasureModel>
{
  protected override DerivedAggregateMeasureModel UpsertConcreteModel(
    DerivedAggregateMeasureModel lhs,
    long lhsCount,
    DerivedAggregateMeasureModel rhs,
    long rhsCount
  )
  {
    return lhs.Upsert(lhsCount, rhs, rhsCount);
  }
}

public static class DerivedAggregateMeasureUpserterExtensions
{
  public static DerivedAggregateMeasureModel Upsert(
    this DerivedAggregateMeasureModel lhs,
    long lhsCount,
    DerivedAggregateMeasureModel rhs,
    long rhsCount
  )
  {
    return new DerivedAggregateMeasureModel
    {
      Avg =
        lhsCount + rhsCount == 0
          ? 0
          : (lhs.Avg * lhsCount + rhs.Avg * rhsCount) / (lhsCount + rhsCount),
      Min = lhs.Min < rhs.Min ? lhs.Min : rhs.Min,
      Max = lhs.Max > rhs.Max ? lhs.Max : rhs.Max,
      MinTimestamp = lhs.Min < rhs.Min ? lhs.MinTimestamp : rhs.MinTimestamp,
      MaxTimestamp = lhs.Max > rhs.Max ? lhs.MaxTimestamp : rhs.MaxTimestamp,
    };
  }

  public static DerivedAggregateMeasureModel UpsertDerivedPowerFromEnergy(
    this DerivedAggregateMeasureModel lhsDerivedPower,
    long lhsQuarterHourCount,
    DerivedAggregateMeasureModel rhsDerivedPower,
    long rhsQuarterHourCount,
    CumulativeAggregateMeasureModel lhsEnergy,
    CumulativeAggregateMeasureModel rhsEnergy,
    DateTimeOffset timestamp,
    IntervalModel interval,
    TimeQueries time
  )
  {
    if (interval is not IntervalModel.QuarterHour)
    {
      return lhsDerivedPower.Upsert(
        lhsQuarterHourCount,
        rhsDerivedPower,
        rhsQuarterHourCount
      );
    }

    var minEnergy =
      lhsEnergy.Min < rhsEnergy.Min ? lhsEnergy.Min : rhsEnergy.Min;
    var maxEnergy =
      lhsEnergy.Max > rhsEnergy.Max ? lhsEnergy.Max : rhsEnergy.Max;
    var power =
      (maxEnergy - minEnergy)
      / (decimal)time.IntervalTimeSpan(interval, timestamp).TotalHours;

    return new DerivedAggregateMeasureModel
    {
      Avg = power,
      Max = power,
      Min = power,
      MinTimestamp = timestamp,
      MaxTimestamp = timestamp,
    };
  }
}
