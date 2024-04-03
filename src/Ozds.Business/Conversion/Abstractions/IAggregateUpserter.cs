using System.Linq.Expressions;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IAggregateUpserter
{
  bool CanUpsertModel(Type modelType);

  bool CanUpsertEntity(Type entityType);

  IAggregate UpsertModel(IAggregate lhs, IAggregate rhs);

  Expression<Func<IAggregateEntity, IAggregateEntity, IAggregateEntity>> UpsertEntity { get; }
}
