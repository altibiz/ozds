using System.Text.Json.Serialization;
using Ozds.Iot.Converters;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonPushRequestEntity(
  DateTimeOffset Timestamp,
  IPidgeonMeterPushRequestEntity[] Measurements
) : IMessengerPushRequestEntity
{
  IEnumerable<IMeterPushRequestEntity> IMessengerPushRequestEntity.Measurements =>
    Measurements.OfType<IMeterPushRequestEntity>();
}
