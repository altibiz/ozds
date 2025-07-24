using System.Runtime.CompilerServices;
using Ozds.Time.Clock;
using Ozds.Time.Entities;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Time.Queries.Implementations;

public class ClockQueries(
  ClockWinder winder
) : IClockQueries
{
  public DateTimeOffset Timestamp()
  {
    return DateTimeOffset.UtcNow + winder.Offset;
  }

  public DateTimeOffset Now()
  {
    return DateTimeOffset.UtcNow;
  }

  public async IAsyncEnumerable<DateTimeOffsetRangeEntity> Future(
    TimeSpan interval,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var dateTo = Now();
    while (true)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        break;
      }

      await Task.Delay(interval, cancellationToken);
      var dateFrom = dateTo;
      dateTo = Now();
      yield return new DateTimeOffsetRangeEntity
      {
        DateFrom = dateFrom,
        DateTo = dateTo
      };
    }
  }
}
