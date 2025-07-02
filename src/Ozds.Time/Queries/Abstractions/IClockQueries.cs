namespace Ozds.Time.Queries.Abstractions;

public interface IClockQueries : IQueries
{
  DateTimeOffset Timestamp();

  DateTimeOffset Now();
}
