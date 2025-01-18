using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonMessengerPushRequestEntity : IMessengerPushRequestEntity
{
  public DateTimeOffset Timestamp { get; set; } = default!;

  public IPidgeonMeterPushRequestEntity[] Measurements { get; set; } = default!;

  IReadOnlyCollection<IMeterPushRequestEntity> IMessengerPushRequestEntity.
    Measurements
  {
    get { return Measurements; }
  }
}
