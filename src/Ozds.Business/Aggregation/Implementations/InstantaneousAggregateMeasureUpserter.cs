using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Aggregation.Implementations;

public class InstantaneousAggregateMeasureUpserter :
  ConcreteAggregateMeasureUpserter<InstantaneousAggregateMeasureModel>
{
  protected override InstantaneousAggregateMeasureModel UpsertConcreteModel(
    InstantaneousAggregateMeasureModel lhs,
    long lhsCount,
    InstantaneousAggregateMeasureModel rhs,
    long rhsCount
  )
  {
    return lhs.Upsert(
      lhsCount,
      rhs,
      rhsCount
    );
  }
}

public static class InstantaneousAggregateMeasureUpserterExtensions
{
  public static InstantaneousAggregateMeasureModel Upsert(
    this InstantaneousAggregateMeasureModel lhs,
    long lhsCount,
    InstantaneousAggregateMeasureModel rhs,
    long rhsCount
  )
  {
    return new InstantaneousAggregateMeasureModel
    {
      Avg = lhsCount + rhsCount == 0
        ? 0
        : (lhs.Avg * lhsCount + rhs.Avg * rhsCount) / (lhsCount + rhsCount),
      Min = lhs.Min < rhs.Min ? lhs.Min : rhs.Min,
      Max = lhs.Max > rhs.Max ? lhs.Max : rhs.Max,
      MinTimestamp = lhs.Min < rhs.Min ? lhs.MinTimestamp : rhs.MinTimestamp,
      MaxTimestamp = lhs.Max > rhs.Max ? lhs.MaxTimestamp : rhs.MaxTimestamp
    };
  }

  public static InstantaneousAggregateMeasureModel UpsertDerivedPowerFromEnergy(
    this InstantaneousAggregateMeasureModel lhsDerivedPower,
    long lhsQuarterHourCount,
    InstantaneousAggregateMeasureModel rhsDerivedPower,
    long rhsQuarterHourCount,
    CumulativeAggregateMeasureModel lhsEnergy,
    CumulativeAggregateMeasureModel rhsEnergy,
    DateTimeOffset timestamp,
    IntervalModel interval
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
    var power = (maxEnergy - minEnergy)
      / (decimal)interval.ToTimeSpan(timestamp).TotalHours;

    return new InstantaneousAggregateMeasureModel
    {
      Avg = power,
      Max = power,
      Min = power,
      MinTimestamp = timestamp,
      MaxTimestamp = timestamp
    };
  }
}
