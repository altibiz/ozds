using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface IAggregateEntity : IReadonlyEntity
{
  public DateTimeOffset Timestamp { get; }

  public long Count { get; }

  public IntervalEntity Interval { get; }

  public string MeterId { get; }
}
