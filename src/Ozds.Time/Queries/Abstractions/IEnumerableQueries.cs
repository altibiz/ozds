using Ozds.Time.Entities;

namespace Ozds.Time.Queries.Abstractions;

public interface IEnumerableQueries : IQueries
{
  public IEnumerable<DateTimeOffsetRangeEntity> Split(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    int times
  )
  {
    return Split(dateFrom, dateTo, (dateTo - dateFrom) / times);
  }

  public IEnumerable<DateTimeOffsetRangeEntity> Split(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    TimeSpan interval
  )
  {
    return Split(
      new DateTimeOffsetRangeEntity { DateFrom = dateFrom, DateTo = dateTo },
      interval
    );
  }

  public IEnumerable<DateTimeOffsetRangeEntity> Split(
    DateTimeOffsetRangeEntity range,
    int times
  )
  {
    return Split(range, (range.DateTo - range.DateFrom) / times);
  }

  public IEnumerable<DateTimeOffsetRangeEntity> Split(
    DateTimeOffsetRangeEntity range,
    TimeSpan interval
  );

  public IAsyncEnumerable<IAsyncEnumerable<T>> Batch<T>(
    IAsyncEnumerable<T> enumerable,
    int size,
    CancellationToken cancellationToken
  );

  public IAsyncEnumerable<T> Concat<T>(
    IEnumerable<IAsyncEnumerable<T>> enumerables,
    CancellationToken cancellationToken
  );
}
