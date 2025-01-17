using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Abstractions;

public interface IAggregateUpserter
{
  public Type ModelType { get; }

  bool CanUpsert(Type type);

  IAggregate Upsert(IAggregate lhs, IAggregate rhs);
}
