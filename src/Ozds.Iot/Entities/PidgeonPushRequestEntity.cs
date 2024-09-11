using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonPushRequestEntity(
  DateTimeOffset Timestamp,
  PidgeonPushRequestEntity[] Measurements
) : IMessengerPushRequestEntity
{
  IEnumerable<IMeterPushRequestEntity> IMessengerPushRequestEntity.Measurements =>
    Measurements.OfType<IMeterPushRequestEntity>();
}
