using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Aggregation.Implementations;

public class CumulativeAggregateMeasureUpserter :
  ConcreteAggregateMeasureUpserter<CumulativeAggregateMeasureModel>
{
  protected override CumulativeAggregateMeasureModel UpsertConcreteModel(
    CumulativeAggregateMeasureModel lhs,
    long lhsCount,
    CumulativeAggregateMeasureModel rhs,
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

public static class CumulativeAggregateMeasureUpserterExtensions
{
#pragma warning disable IDE0060 // Remove unused parameter
  public static CumulativeAggregateMeasureModel Upsert(
    this CumulativeAggregateMeasureModel lhs,
    long lhsCount,
    CumulativeAggregateMeasureModel rhs,
    long rhsCount
  )
#pragma warning restore IDE0060 // Remove unused parameter
  {
    return new CumulativeAggregateMeasureModel
    {
      Min = lhs.Min < rhs.Min ? lhs.Min : rhs.Min,
      Max = lhs.Max > rhs.Max ? lhs.Max : rhs.Max
    };
  }
}
