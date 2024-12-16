using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Abstractions;

public interface IAggregateMeasureUpserter
{
  bool CanUpsertModel(Type modelType);

  IAggregateMeasure UpsertModel(
    IAggregateMeasure lhs,
    long lhsCount,
    IAggregateMeasure rhs,
    long rhsCount
  );
}
