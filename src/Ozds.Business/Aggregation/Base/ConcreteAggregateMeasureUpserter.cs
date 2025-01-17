using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Base;

public abstract class ConcreteAggregateMeasureUpserter<TModel> :
  IAggregateMeasureUpserter
  where TModel : class, IAggregateMeasure
{
  public Type ModelType { get; } = typeof(TModel);

  public bool CanUpsert(Type type)
  {
    return typeof(TModel).IsAssignableFrom(type);
  }

  public IAggregateMeasure Upsert(
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
