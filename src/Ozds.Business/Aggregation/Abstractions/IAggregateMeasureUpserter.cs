using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Abstractions;

public interface IAggregateMeasureUpserter
{
  public Type ModelType { get; }

  public bool CanUpsert(Type type);

  public IAggregateMeasure Upsert(
    IAggregateMeasure lhs,
    long lhsCount,
    IAggregateMeasure rhs,
    long rhsCount
  );
}
