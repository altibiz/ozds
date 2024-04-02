using System.Text.Json.Nodes;

namespace Ozds.Business.Iot;

public record MessengerPushRequest(
  DateTimeOffset Timestamp,
  MessengerPushRequestMeasurement[] Measurements
);

public record MessengerPushRequestMeasurement(
  string MeterId,
  DateTimeOffset Timestamp,
  JsonObject Data
);
