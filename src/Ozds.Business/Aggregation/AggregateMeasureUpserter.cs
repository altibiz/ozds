using System.Collections.Concurrent;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation;

public class AggregateMeasureUpserter(IServiceProvider serviceProvider)
{
  private readonly
    ConcurrentDictionary<(Type, Type), IAggregateMeasureUpserter> cache = new();

  public TModel UpsertModel<TModel>(
    TModel lhs,
    long lhsCount,
    TModel rhs,
    long rhsCount
  )
    where TModel : class, IAggregateMeasure
  {
    return (TModel)UpsertModelDynamic(lhs, lhsCount, rhs, rhsCount);
  }

  public IAggregateMeasure UpsertModelDynamic(
    IAggregateMeasure lhs,
    long lhsCount,
    IAggregateMeasure rhs,
    long rhsCount
  )
  {
    if (cache.TryGetValue((lhs.GetType(), rhs.GetType()), out var upserter))
    {
      return upserter.Upsert(lhs, lhsCount, rhs, rhsCount);
    }

    upserter = serviceProvider
        .GetServices<IAggregateMeasureUpserter>()
        .FirstOrDefault(
          upserter =>
            upserter.CanUpsert(lhs.GetType())
            && upserter.CanUpsert(rhs.GetType()))
      ?? throw new InvalidOperationException(
        $"No upserter found for models {lhs.GetType()} and {rhs.GetType()}.");

    cache.TryAdd((lhs.GetType(), rhs.GetType()), upserter);

    return upserter.Upsert(lhs, lhsCount, rhs, rhsCount);
  }
}
