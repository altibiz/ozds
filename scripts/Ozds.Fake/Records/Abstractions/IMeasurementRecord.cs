namespace Ozds.Fake.Records.Abstractions;

public interface IMeasurementRecord
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }
}
