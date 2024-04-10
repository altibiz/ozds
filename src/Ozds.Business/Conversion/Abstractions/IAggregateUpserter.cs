using System.Linq.Expressions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IAggregateUpserter
{
  LambdaExpression UpsertEntity { get; }

  bool CanUpsertModel(Type modelType);

  bool CanUpsertEntity(Type entityType);

  IAggregate UpsertModel(IAggregate lhs, IAggregate rhs);
}
