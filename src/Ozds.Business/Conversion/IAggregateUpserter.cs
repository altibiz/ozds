using System.Linq.Expressions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public interface IAggregateUpserter
{
  public Type ModelType { get; }

  public Type EntityType { get; }

  AggregateModel UpsertModel(AggregateModel lhs, AggregateModel rhs);

  Expression<Func<AggregateEntity, AggregateEntity, AggregateEntity>> UpsertEntity { get; }
}

public abstract class AggregateUpserter<TModel, TEntity> : IAggregateUpserter
  where TModel : AggregateModel
  where TEntity : AggregateEntity
{
  public Type ModelType => typeof(TModel);

  public Type EntityType => typeof(TEntity);

  public AggregateModel UpsertModel(AggregateModel lhs, AggregateModel rhs)
  {
    return UpsertConcreteModel((TModel)lhs, (TModel)rhs);
  }

  public Expression<Func<AggregateEntity, AggregateEntity, AggregateEntity>> UpsertEntity
  {
    get { return UpsertConcreteEntity as Expression<Func<AggregateEntity, AggregateEntity, AggregateEntity>>; }
  }

  protected abstract TModel UpsertConcreteModel(TModel lhs, TModel rhs);

  protected abstract Expression<Func<TEntity, TEntity, TEntity>> UpsertConcreteEntity { get; }
}

public abstract class AggregateUpserter
