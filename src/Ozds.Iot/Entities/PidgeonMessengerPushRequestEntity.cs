using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonMessengerPushRequestEntity(
  DateTimeOffset Timestamp,
  IPidgeonMeterPushRequestEntity[] Measurements
) : IMessengerPushRequestEntity
{
  IReadOnlyCollection<IMeterPushRequestEntity> IMessengerPushRequestEntity.Measurements =>
    Measurements;
}
