namespace Ozds.Iot.Entities.Abstractions;

public interface IMeterPushRequestEntity
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }
}
