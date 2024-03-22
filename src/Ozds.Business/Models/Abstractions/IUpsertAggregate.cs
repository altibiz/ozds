using System.Linq.Expressions;

namespace Ozds.Business.Models.Abstractions;

public interface IUpsertAggregate<T> : IAggregate where T : IUpsertAggregate<T>
{
  public record UpsertExpressionHolder(Expression<Func<T, T, T>> Value);

  public static abstract UpsertExpressionHolder UpsertExpression { get; }

  public record UpsertHolder(Func<T, T, T> Value);

  public static virtual UpsertHolder Upsert => new(T.UpsertExpression.Value.Compile());
}

public static class IUpsertAggregateExtensions
{
  public static IEnumerable<T> UpsertRange<T>(this IEnumerable<T> aggregates)
    where T : IUpsertAggregate<T> =>
    aggregates
      .GroupBy(aggregate => (aggregate.MeterId, aggregate.Timestamp, aggregate.Interval))
      .Select(group => group.Aggregate((a, b) => T.Upsert.Value(a, b)));
}
