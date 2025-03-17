using System.Runtime.CompilerServices;

namespace Ozds.Fake.Extensions;

// TODO: extract to a separate library

public record struct DateTimeOffsetRange(
  DateTimeOffset DateFrom,
  DateTimeOffset DateTo
);

public static class IEnumerableExtensions
{
  public static IEnumerable<IEnumerable<T>> Batch<T>(
    this IEnumerable<T> enumerable,
    int size
  )
  {
    var enumerator = enumerable.GetEnumerator();

    static IEnumerable<T> Inner(
      IEnumerator<T> enumerator,
      int size
    )
    {
      var count = 0;
      do
      {
        yield return enumerator.Current;
      } while (
        ++count < size
        && enumerator.MoveNext());
    }

    while (enumerator.MoveNext())
    {
      yield return Inner(enumerator, size);
    }
  }

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

  public static IEnumerable<IEnumerable<T>> Zip<T>(
    this IEnumerable<IEnumerable<T>> enumerables
  )
  {
    var enumerators = enumerables
      .Select(enumerable => enumerable.GetEnumerator())
      .ToList();
    while (enumerators.TrueForAll(enumerator => enumerator.MoveNext()))
    {
      yield return enumerators.Select(enumerator => enumerator.Current);
    }
  }

  public static async IAsyncEnumerable<IEnumerable<T>> Zip<T>(
    this IEnumerable<IAsyncEnumerable<T>> enumerables,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerators = enumerables
      .Select(
        enumerable => enumerable
          .GetAsyncEnumerator(cancellationToken))
      .ToList();
    var hasNext = true;
    foreach (var enumerator in enumerators)
    {
      if (!await enumerator.MoveNextAsync())
      {
        hasNext = false;
      }
    }

    while (hasNext)
    {
      yield return enumerators.Select(enumerator => enumerator.Current);
      hasNext = true;
      foreach (var enumerator in enumerators)
      {
        if (!await enumerator.MoveNextAsync())
        {
          hasNext = false;
        }
      }
    }
  }

  public static IEnumerable<T> Concat<T>(
    this IEnumerable<IEnumerable<T>> enumerables
  )
  {
    return enumerables.SelectMany(enumerable => enumerable);
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

  public static IEnumerable<IEnumerable<T>> Branch<T>(
    IEnumerable<T> enumerable,
    int times
  )
  {
    IEnumerable<T> Inner(T item)
    {
      var enumerator = Enumerable.Range(0, times).GetEnumerator();
      while (enumerator.MoveNext())
      {
        yield return item;
      }
    }

    foreach (var item in enumerable)
    {
      yield return Inner(item);
    }
  }

  public static async IAsyncEnumerable<DateTimeOffsetRange> Future(
    this TimeSpan interval,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var dateTo = DateTimeOffset.UtcNow;
    while (true)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        break;
      }

      await Task.Delay(interval, cancellationToken);
      var dateFrom = dateTo;
      dateTo = DateTimeOffset.UtcNow;
      yield return new DateTimeOffsetRange(dateFrom, dateTo);
    }
  }

  public static IEnumerable<DateTimeOffsetRange> Past(
    this TimeSpan interval,
    DateTimeOffset upTo
  )
  {
    var dateTo = DateTimeOffset.UtcNow;
    while (true)
    {
      var dateFrom = dateTo - interval;
      if (dateFrom <= upTo)
      {
        break;
      }

      yield return new DateTimeOffsetRange(dateFrom, dateTo);
      dateTo = dateFrom;
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
