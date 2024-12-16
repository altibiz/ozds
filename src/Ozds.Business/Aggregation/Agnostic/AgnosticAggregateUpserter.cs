using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Agnostic;

public class AgnosticAggregateUpserter(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

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
