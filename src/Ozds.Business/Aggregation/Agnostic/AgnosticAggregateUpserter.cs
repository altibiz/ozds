using System.Linq.Expressions;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Aggregation.Agnostic;

public class AgnosticAggregateUpserter(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public Expression<Func<TEntity, TEntity, TEntity>> UpsertEntity<TEntity>()
    where TEntity : class, IAggregateEntity
  {
    return UpsertEntityAgnostic(typeof(TEntity)) as
        Expression<Func<TEntity, TEntity, TEntity>>
      ?? throw new InvalidOperationException(
        $"No upserter found for entity {typeof(TEntity)}.");
  }

  public LambdaExpression UpsertEntityAgnostic(Type entityType)
  {
    var upserters = _serviceProvider.GetServices<IAggregateUpserter>();
    var upserter =
      upserters.FirstOrDefault(
        upserter =>
          upserter.CanUpsertEntity(entityType));
    return upserter?.UpsertEntity
      ?? throw new InvalidOperationException(
        $"No upserter found for entity {entityType}.");
  }

  public TModel UpsertModel<TModel>(TModel lhs, TModel rhs)
    where TModel : class, IAggregate
  {
    return UpsertModelAgnostic(lhs, rhs) as TModel
      ?? throw new InvalidOperationException(
        $"No upserter found for model {typeof(TModel)}.");
  }

  public IAggregate UpsertModelAgnostic(IAggregate lhs, IAggregate rhs)
  {
    var upserters = _serviceProvider.GetServices<IAggregateUpserter>();
    var upserter = upserters.FirstOrDefault(
      upserter =>
        upserter.CanUpsertModel(lhs.GetType()) &&
        upserter.CanUpsertModel(rhs.GetType()));
    return upserter?.UpsertModel(lhs, rhs)
      ?? throw new InvalidOperationException(
        $"No upserter found for models {lhs.GetType()} and {rhs.GetType()}.");
  }
}
