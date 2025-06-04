using System.Runtime.CompilerServices;

namespace Ozds.Fake.Extensions;

public record struct DateTimeOffsetRange(
  DateTimeOffset DateFrom,
  DateTimeOffset DateTo
);

public static class IEnumerableExtensions
{
  public static async IAsyncEnumerable<IAsyncEnumerable<T>> Batch<T>(
    this IAsyncEnumerable<T> enumerable,
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
      } while (
        ++count < size
        && await enumerator.MoveNextAsync());
    }

    while (await enumerator.MoveNextAsync())
    {
      yield return Inner(enumerator, size);
    }
  }

  public static async IAsyncEnumerable<T> Concat<T>(
    this IEnumerable<IAsyncEnumerable<T>> enumerables,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    foreach (var enumerable in enumerables)
    {
      await foreach (var item in enumerable
        .WithCancellation(cancellationToken))
      {
        yield return item;
      }
    }
  }

  public static IEnumerable<DateTimeOffsetRange> Split(
    this DateTimeOffsetRange range,
    TimeSpan interval
  )
  {
    var dateTo = range.DateTo;
    var dateFrom = range.DateFrom;
    while (true)
    {
      var date = dateFrom.Add(interval);
      if (date <= dateTo)
      {
        yield return new DateTimeOffsetRange(dateFrom, date);
        dateFrom = date;
      }
      else
      {
        yield return new DateTimeOffsetRange(dateFrom, dateTo);
        break;
      }
    }
  }
}
