using System.Linq.Expressions;

namespace Ozds.Business.Math;

public interface IUpsertAggregate<T> : IAggregate where T : IUpsertAggregate<T>
{
  public static abstract Expression<Func<T, T, T>> UpsertExpression { get; }

  public static Func<T, T, T> Upsert => T.UpsertExpression.Compile();
}

public static class IAggregateExtensions
{
  public static IEnumerable<T> UpsertRange<T>(this IEnumerable<T> aggregates)
    where T : IUpsertAggregate<T> =>
    aggregates
      .GroupBy(aggregate => (aggregate.Source, aggregate.Timestamp, aggregate.TimeSpan))
      .Select(group => group.Aggregate((a, b) => IUpsertAggregate<T>.Upsert(a, b)));
}
