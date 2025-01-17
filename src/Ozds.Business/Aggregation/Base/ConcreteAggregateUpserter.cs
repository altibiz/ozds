using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Aggregation.Base;

public abstract class ConcreteAggregateUpserter<TModel> : IAggregateUpserter
  where TModel : class, IAggregate
{
  public Type ModelType { get; } = typeof(TModel);

  public bool CanUpsert(Type type)
  {
    return typeof(TModel).IsAssignableFrom(type);
  }

  public IAggregate Upsert(IAggregate lhs, IAggregate rhs)
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
