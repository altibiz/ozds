using Ozds.Business.Queries.Abstractions;
using TimeClockQueries = Ozds.Time.Queries.Abstractions.IClockQueries;

namespace Ozds.Business.Queries;

public class ClockQueries(
  TimeClockQueries timeClockQueries
) : ISingletonQueries
{
  // NOTE: virtual because we want to mock it
  public virtual DateTimeOffset Timestamp()
  {
    return timeClockQueries.Timestamp();
  }

  // NOTE: virtual because we want to mock it
  public virtual DateTimeOffset Now()
  {
    return timeClockQueries.Now();
  }
}
