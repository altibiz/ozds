using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Aggregation.Complex;

public class InstantaneousAggregateMeasureUpserter :
  AggregateMeasureUpserter<InstantaneousAggregateMeasureModel>
{
  protected override InstantaneousAggregateMeasureModel UpsertConcreteModel(
    InstantaneousAggregateMeasureModel lhs,
    long lhsCount,
    InstantaneousAggregateMeasureModel rhs,
    long rhsCount
  )
  {
    return InstantaneousAggregateMeasureUpserterExtensions.Upsert(
      lhs,
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
      Avg = (lhsCount + rhsCount == 0)
        ? 0
        : (lhs.Avg * lhsCount + rhs.Avg * rhsCount) / (lhsCount + rhsCount),
      Min = lhs.Min < rhs.Min ? lhs.Min : rhs.Min,
      Max = lhs.Max > rhs.Max ? lhs.Max : rhs.Max,
      MinTimestamp = lhs.Min < rhs.Min ? lhs.MinTimestamp : rhs.MinTimestamp,
      MaxTimestamp = lhs.Max > rhs.Max ? lhs.MaxTimestamp : rhs.MaxTimestamp
    };
  }
}
