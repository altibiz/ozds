using Ozds.Time.Clock;
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
}
