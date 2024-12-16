using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Base;

public abstract class AggregateMeasureUpserter<TModel> :
  IAggregateMeasureUpserter
  where TModel : class, IAggregateMeasure
{
  public bool CanUpsertModel(Type modelType)
  {
    return typeof(TModel).IsAssignableFrom(modelType);
  }

  public IAggregateMeasure UpsertModel(
    IAggregateMeasure lhs,
    long lhsCount,
    IAggregateMeasure rhs,
    long rhsCount
  )
  {
    return UpsertConcreteModel(
      lhs as TModel ?? throw new InvalidOperationException(
        $"Model is not of type {typeof(TModel).Name}."),
      lhsCount,
      rhs as TModel ?? throw new InvalidOperationException(
        $"Model is not of type {typeof(TModel).Name}."),
      rhsCount
    );
  }

  protected abstract TModel UpsertConcreteModel(
    TModel lhs,
    long lhsCount,
    TModel rhs,
    long rhsCount
  );
}
