namespace Ozds.Report.Entities;

public abstract class AggregateEntity
{
  public string MeterId { get; set; } = default!;

  public required string MeasurementLocationId { get; set; }

  public required DateTimeOffset Timestamp { get; set; }

  public required IntervalEntity Interval { get; set; }

  public required long Count { get; set; }

  public required long QuarterHourCount { get; set; }
}
