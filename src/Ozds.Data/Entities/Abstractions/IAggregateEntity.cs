using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface IAggregateEntity : IMeasurementEntity
{
  public long Count { get; }

  public long QuarterHourCount { get; }

  public IntervalEntity Interval { get; }
}
