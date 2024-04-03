using System.Text.Json.Nodes;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  bool CanConvertToMeasurement(string meterId);

  IMeasurement ToMeasurement(JsonObject pushRequest, string meterId, DateTimeOffset timestamp);
}
