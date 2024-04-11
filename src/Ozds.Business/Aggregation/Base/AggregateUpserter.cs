using System.Linq.Expressions;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Aggregation.Base;

public abstract class AggregateUpserter<TModel, TEntity> : IAggregateUpserter
  where TModel : class, IAggregate
  where TEntity : class, IAggregateEntity
{
  protected abstract Expression<Func<TEntity, TEntity, TEntity>>
    UpsertConcreteEntity { get; }

  public bool CanUpsertModel(Type modelType)
  {
    return typeof(TModel).IsAssignableFrom(modelType);
  }

  public bool CanUpsertEntity(Type entityType)
  {
    return typeof(TEntity).IsAssignableFrom(entityType);
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

  public LambdaExpression UpsertEntity
  {
    get
    {
      // var castException = new InvalidOperationException(
      //   $"Entity is not of type {typeof(TEntity).Name}.");
      // var castExceptionExpression = Expression.Constant(castException);
      // var castThrowExpression = Expression.Throw(castExceptionExpression);
      // var lhsParamExpression = Expression.Parameter(typeof(IAggregateEntity));
      // var lhsCastExpression =
      //   Expression.TypeAs(lhsParamExpression, typeof(TEntity));
      // var lhsCoalesceExpression =
      //   Expression.Coalesce(lhsCastExpression, castThrowExpression);
      // var rhsParamExpression = Expression.Parameter(typeof(IAggregateEntity));
      // var rhsCastExpression =
      //   Expression.TypeAs(rhsParamExpression, typeof(TEntity));
      // var rhsCoalesceExpression =
      //   Expression.Coalesce(rhsCastExpression, castThrowExpression);
      // var callExpression = Expression.Invoke(UpsertConcreteEntity,
      //   lhsCoalesceExpression, rhsCoalesceExpression);
      // var lambdaExpression =
      //   Expression
      //     .Lambda<Func<IAggregateEntity, IAggregateEntity, IAggregateEntity>>(
      //       callExpression,
      //       lhsParamExpression,
      //       rhsParamExpression
      //     );
      var lambdaExpression = UpsertConcreteEntity;
      return lambdaExpression;
    }
  }

  protected abstract TModel UpsertConcreteModel(TModel lhs, TModel rhs);
}
