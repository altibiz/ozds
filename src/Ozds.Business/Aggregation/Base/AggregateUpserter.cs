using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Base;

public abstract class AggregateUpserter<TModel> : IAggregateUpserter
  where TModel : class, IAggregate
{
  public bool CanUpsertModel(Type modelType)
  {
    return typeof(TModel).IsAssignableFrom(modelType);
  }

  public IAggregate UpsertModel(IAggregate lhs, IAggregate rhs)
  {
    return UpsertConcreteModel(
      lhs as TModel ?? throw new InvalidOperationException(
        $"Model is not of type {typeof(TModel).Name}."),
      rhs as TModel ?? throw new InvalidOperationException(
        $"Model is not of type {typeof(TModel).Name}.")
    );
  }

  protected abstract TModel UpsertConcreteModel(TModel lhs, TModel rhs);
}
