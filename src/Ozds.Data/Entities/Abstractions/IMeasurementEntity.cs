namespace Ozds.Data.Entities.Abstractions;

public interface IMeasurementEntity : IReadonlyEntity
{
  public DateTimeOffset Timestamp { get; }

  public string MeterId { get; }
}
