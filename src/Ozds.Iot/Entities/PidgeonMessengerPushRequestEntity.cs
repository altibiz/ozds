using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonMessengerPushRequestEntity : IMessengerPushRequestEntity
{
  public IPidgeonMeterPushRequestEntity[] Measurements { get; set; } = default!;
  public DateTimeOffset Timestamp { get; set; } = default!;

  IReadOnlyCollection<IMeterPushRequestEntity> IMessengerPushRequestEntity.
    Measurements
  {
    get { return Measurements; }
  }
}
