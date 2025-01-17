using System.Collections.Concurrent;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation;

public class AggregateUpserter(IServiceProvider serviceProvider)
{
  public TModel UpsertModel<TModel>(TModel lhs, TModel rhs)
    where TModel : IAggregate
  {
    return (TModel)UpsertModelDynamic(lhs, rhs);
  }

  public IAggregate UpsertModelDynamic(IAggregate lhs, IAggregate rhs)
  {
    if (cache.TryGetValue((lhs.GetType(), rhs.GetType()), out var upserter))
    {
      return upserter.Upsert(lhs, rhs);
    }

    upserter = serviceProvider
        .GetServices<IAggregateUpserter>()
        .FirstOrDefault(upserter =>
          upserter.CanUpsert(lhs.GetType())
          && upserter.CanUpsert(rhs.GetType()))
      ?? throw new InvalidOperationException(
        $"No upserter found for models {lhs.GetType()} and {rhs.GetType()}.");

    cache.TryAdd((lhs.GetType(), rhs.GetType()), upserter);

    return upserter.Upsert(lhs, rhs);
  }

  private readonly ConcurrentDictionary<(Type, Type), IAggregateUpserter> cache =
    new();
}
