using System.Runtime.CompilerServices;
using Ozds.Time.Entities;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Time.Queries.Implementations;

public class EnumerableQueries : IEnumerableQueries
{
  public IEnumerable<DateTimeOffsetRangeEntity> Split(
    DateTimeOffsetRangeEntity range,
    TimeSpan interval
  )
  {
    var dateTo = range.DateTo;
    var dateFrom = range.DateFrom;
    while (true)
    {
      var date = dateFrom.Add(interval);
      if (date > dateTo)
      {
        date = dateTo;
      }

      yield return new DateTimeOffsetRangeEntity
      {
        DateFrom = dateFrom,
        DateTo = date,
      };

      if (date < dateTo)
      {
        dateFrom = date;
      }
      else
      {
        break;
      }
    }
  }

  public async IAsyncEnumerable<IAsyncEnumerable<T>> Batch<T>(
    IAsyncEnumerable<T> enumerable,
    int size,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = enumerable.GetAsyncEnumerator(cancellationToken);

    static async IAsyncEnumerable<T> Inner(
      IAsyncEnumerator<T> enumerator,
      int size
    )
    {
      var count = 0;
      do
      {
        yield return enumerator.Current;
      } while (++count < size && await enumerator.MoveNextAsync());
    }

    while (await enumerator.MoveNextAsync())
    {
      yield return Inner(enumerator, size);
    }
  }

  public async IAsyncEnumerable<T> Concat<T>(
    IEnumerable<IAsyncEnumerable<T>> enumerables,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    foreach (var enumerable in enumerables)
    {
      await foreach (var item in enumerable.WithCancellation(cancellationToken))
      {
        yield return item;
      }
    }
  }
}
