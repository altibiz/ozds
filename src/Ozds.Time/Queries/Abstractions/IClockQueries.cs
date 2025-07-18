using Ozds.Time.Entities;

namespace Ozds.Time.Queries.Abstractions;

public interface IClockQueries : IQueries
{
  DateTimeOffset Timestamp();

  DateTimeOffset Now();

  IAsyncEnumerable<DateTimeOffsetRangeEntity> Future(
    TimeSpan interval,
    CancellationToken cancellationToken
  );
}
