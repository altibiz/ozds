using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities.Base;

public abstract record PidgeonMeterPushRequestEntity(
  string MeterId,
  DateTimeOffset Timestamp
) : IMeterPushRequestEntity;
