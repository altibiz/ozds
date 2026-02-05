using Ozds.Business.Models.Complex;
using Ozds.Business.Queries.Abstractions;
using Ozds.Time.Entities;
using TimeEnumerableQueries = Ozds.Time.Queries.Abstractions.IEnumerableQueries;

namespace Ozds.Business.Queries;

public class EnumerableQueries(TimeEnumerableQueries timeEnumerableQueries)
  : ISingletonQueries
{
  public virtual IEnumerable<DateTimeOffsetRangeModel> Split(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    int times
  )
  {
    var entity = new DateTimeOffsetRangeEntity
    {
      DateFrom = dateFrom,
      DateTo = dateTo,
    };
    return timeEnumerableQueries
      .Split(entity, times)
      .Select(x => new DateTimeOffsetRangeModel
      {
        DateFrom = x.DateFrom,
        DateTo = x.DateTo,
      });
  }

  public virtual IEnumerable<DateTimeOffsetRangeModel> Split(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    TimeSpan interval
  )
  {
    var entity = new DateTimeOffsetRangeEntity
    {
      DateFrom = dateFrom,
      DateTo = dateTo,
    };
    return timeEnumerableQueries
      .Split(entity, interval)
      .Select(x => new DateTimeOffsetRangeModel
      {
        DateFrom = x.DateFrom,
        DateTo = x.DateTo,
      });
  }

  public virtual IEnumerable<DateTimeOffsetRangeModel> Split(
    DateTimeOffsetRangeModel range,
    int times
  )
  {
    var entity = new DateTimeOffsetRangeEntity
    {
      DateFrom = range.DateFrom,
      DateTo = range.DateTo,
    };
    return timeEnumerableQueries
      .Split(entity, times)
      .Select(x => new DateTimeOffsetRangeModel
      {
        DateFrom = x.DateFrom,
        DateTo = x.DateTo,
      });
  }

  public virtual IEnumerable<DateTimeOffsetRangeModel> Split(
    DateTimeOffsetRangeModel range,
    TimeSpan interval
  )
  {
    var entity = new DateTimeOffsetRangeEntity
    {
      DateFrom = range.DateFrom,
      DateTo = range.DateTo,
    };
    return timeEnumerableQueries
      .Split(entity, interval)
      .Select(x => new DateTimeOffsetRangeModel
      {
        DateFrom = x.DateFrom,
        DateTo = x.DateTo,
      });
  }

  public IAsyncEnumerable<IAsyncEnumerable<T>> Batch<T>(
    IAsyncEnumerable<T> enumerable,
    int size,
    CancellationToken cancellationToken
  )
  {
    return timeEnumerableQueries.Batch(enumerable, size, cancellationToken);
  }

  public IAsyncEnumerable<T> Concat<T>(
    IEnumerable<IAsyncEnumerable<T>> enumerables,
    CancellationToken cancellationToken
  )
  {
    return timeEnumerableQueries.Concat(enumerables, cancellationToken);
  }
}
