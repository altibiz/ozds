using System.Linq.Expressions;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Conversion.Base;
public abstract class AggregateUpserter<TModel, TEntity> : IAggregateUpserter
  where TModel : class, IAggregate
  where TEntity : class, IAggregateEntity
{
  public Type ModelType => typeof(TModel);

  public Type EntityType => typeof(TEntity);

  public IAggregate UpsertModel(IAggregate lhs, IAggregate rhs)
  {
    return UpsertConcreteModel(
      lhs as TModel ?? throw new InvalidOperationException($"lhs is not of type {typeof(TModel).Name}"),
      rhs as TModel ?? throw new InvalidOperationException($"rhs is not of type {typeof(TModel).Name}")
    );
  }

  public Expression<Func<IAggregateEntity, IAggregateEntity, IAggregateEntity>> UpsertEntity
  {
    get
    {
      var castException = new InvalidOperationException($"lhs is not of type {typeof(TEntity).Name}");
      var castExceptionExpression = Expression.Constant(castException);
      var castThrowExpression = Expression.Throw(castExceptionExpression);
      var lhsParamExpression = Expression.Parameter(typeof(IAggregateEntity));
      var lhsCastExpression = Expression.TypeAs(lhsParamExpression, typeof(TEntity));
      var lhsCoalesceExpression = Expression.Coalesce(lhsCastExpression, castThrowExpression);
      var rhsParamExpression = Expression.Parameter(typeof(IAggregateEntity));
      var rhsCastExpression = Expression.TypeAs(rhsParamExpression, typeof(TEntity));
      var rhsCoalesceExpression = Expression.Coalesce(rhsCastExpression, castThrowExpression);
      var callExpression = Expression.Invoke(UpsertConcreteEntity, lhsCoalesceExpression, rhsCoalesceExpression);
      var lambdaExpression = Expression.Lambda<Func<IAggregateEntity, IAggregateEntity, IAggregateEntity>>(
        callExpression,
        lhsParamExpression,
        rhsParamExpression
      );
      return lambdaExpression;
    }
  }

  protected abstract TModel UpsertConcreteModel(TModel lhs, TModel rhs);

  protected abstract Expression<Func<TEntity, TEntity, TEntity>> UpsertConcreteEntity { get; }
}
