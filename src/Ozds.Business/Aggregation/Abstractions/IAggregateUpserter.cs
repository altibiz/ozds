using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Abstractions;

public interface IAggregateUpserter
{
  bool CanUpsertModel(Type modelType);

  IAggregate UpsertModel(IAggregate lhs, IAggregate rhs);
}
