using System.Linq.Expressions;
using System.Reflection;
using Ozds.Data.Entities.Base;

// TODO: cache compilations
// TODO: optimize

namespace Ozds.Business.Models.Abstractions;

public interface IUpsertAggregate<TModel, TEntity> : IAggregate
  where TModel : IUpsertAggregate<TModel, TEntity>
  where TEntity : AggregateEntity
{
  public static abstract UpsertExpressionHolder UpsertExpression { get; }

  public static virtual UpsertHolder Upsert
  {
    get { return new UpsertHolder(TModel.UpsertExpression.Value.Compile()); }
  }

  public static abstract UpsertRowExpressionHolder UpsertRowExpression { get; }

  public static virtual UpsertRowHolder UpsertRow
  {
    get { return new(TModel.UpsertRowExpression.Value.Compile()); }
  }

  public record UpsertExpressionHolder(Expression<Func<TModel, TModel, TModel>> Value);

  public record UpsertHolder(Func<TModel, TModel, TModel> Value);

  public record UpsertRowExpressionHolder(Expression<Func<TModel, TModel, TModel>> Value);

  public record UpsertRowHolder(Func<TModel, TModel, TModel> Value);
}

public static class IUpsertAggregateExtensions
{
  public static IEnumerable<T> UpsertRange<T>(this IEnumerable<T> aggregates)
    where T : IUpsertAggregate<T>
  {
    return aggregates
      .GroupBy(aggregate =>
        (aggregate.MeterId, aggregate.Timestamp, aggregate.Interval))
      .Select(group => group.Aggregate((a, b) => T.Upsert.Value(a, b)));
  }

  public static IEnumerable<IAggregate> UpsertRange(this IEnumerable<IAggregate> aggregates)
  {
    return aggregates
      .GroupBy(aggregate =>
        (aggregate.GetType(), aggregate.MeterId, aggregate.Timestamp, aggregate.Interval))
      .Select(group => group
        .Aggregate((a, b) =>
        {
          var type = a.GetType();
          var getter = type.GetProperty("Upsert", BindingFlags.Static)?.GetGetMethod() ??
                       throw new InvalidOperationException(
                         $"Upsert getter not found in aggregate type {type.FullName}");
          var upsert = getter.Invoke(null, null) ??
                       throw new InvalidOperationException(
                         $"Upsert getter returned null for aggregate type {type.FullName}");
          var valueField = upsert.GetType().GetField("Value") ??
                           throw new InvalidOperationException(
                             $"Value field not found in upsert type {upsert.GetType().FullName}");
          var @delegate = valueField.GetValue(upsert) as Delegate ??
                        throw new InvalidOperationException(
                          $"Upsert method not found in upsert type {upsert.GetType().FullName}");
          return @delegate.Method.Invoke(null, new object[] { a, b }) as IAggregate ??
                 throw new InvalidOperationException(
                   $"Upsert method returned null for aggregate {type.FullName}");
        }
        ));
  }
}
