using System.Text.Json.Nodes;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  bool CanConvert(string meterId);

  IMeasurement ToMeasurement(JsonObject pushRequest, string meterId,
    DateTimeOffset timestamp);

  JsonObject ToPushRequest(IMeasurement measurement);
}
