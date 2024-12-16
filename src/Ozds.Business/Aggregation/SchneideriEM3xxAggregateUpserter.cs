using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Aggregation;

public class SchneideriEM3xxxAggregateUpserter : AggregateUpserter<
  SchneideriEM3xxxAggregateModel>
{
  protected override SchneideriEM3xxxAggregateModel UpsertConcreteModel(
    SchneideriEM3xxxAggregateModel lhs,
    SchneideriEM3xxxAggregateModel rhs)
  {
    return new SchneideriEM3xxxAggregateModel
    {
      MeterId = lhs.MeterId,
      MeasurementLocationId = lhs.MeasurementLocationId,
      Timestamp = lhs.Timestamp,
      Interval = lhs.Interval,
      Count = lhs.Count + rhs.Count,
      VoltageL1AnyT0Avg_V = lhs.VoltageL1AnyT0Avg_V.Upsert(
        lhs.Count,
        rhs.VoltageL1AnyT0Avg_V,
        rhs.Count
      ),
      VoltageL2AnyT0Avg_V = lhs.VoltageL2AnyT0Avg_V.Upsert(
        lhs.Count,
        rhs.VoltageL2AnyT0Avg_V,
        rhs.Count
      ),
      VoltageL3AnyT0Avg_V = lhs.VoltageL3AnyT0Avg_V.Upsert(
        lhs.Count,
        rhs.VoltageL3AnyT0Avg_V,
        rhs.Count
      ),
    };
  }
}
