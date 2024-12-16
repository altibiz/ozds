using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Agnostic;

public class AgnosticAggregateMeasureUpserter(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public TModel UpsertModel<TModel>(
    TModel lhs,
    long lhsCount,
    TModel rhs,
    long rhsCount
  )
    where TModel : class, IAggregateMeasure
  {
    return UpsertModelAgnostic(lhs, lhsCount, rhs, rhsCount) as TModel
      ?? throw new InvalidOperationException(
        $"No upserter found for model {typeof(TModel)}.");
  }

  public IAggregateMeasure UpsertModelAgnostic(
    IAggregateMeasure lhs,
    long lhsCount,
    IAggregateMeasure rhs,
    long rhsCount
  )
  {
    return _serviceProvider
        .GetServices<IAggregateMeasureUpserter>()
        .FirstOrDefault(
          upserter =>
            upserter.CanUpsertModel(lhs.GetType()))
        ?.UpsertModel(lhs, lhsCount, rhs, rhsCount)
      ?? throw new InvalidOperationException(
        $"No upserter found for model {lhs.GetType()}.");
  }
}
