namespace Ozds.Iot.Entities.Abstractions;

public interface IMessengerPushRequestEntity
{
  public DateTimeOffset Timestamp { get; }

  public IEnumerable<IMeterPushRequestEntity> Measurements { get; }
}
