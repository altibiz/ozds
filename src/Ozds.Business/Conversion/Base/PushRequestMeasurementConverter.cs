using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class PushRequestMeasurementConverter<TPushRequest,
  TMeasurement> : IPushRequestMeasurementConverter
  where TMeasurement : class, IMeasurement
{
  protected abstract string MeterIdPrefix { get; }

  public bool CanConvert(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  private static readonly JsonSerializerOptions serializationOptions =
    new()
    {
      PropertyNameCaseInsensitive = true,
      NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

  public IMeasurement ToMeasurement(JsonObject pushRequest, string meterId,
    DateTimeOffset timestamp)
  {
    return ToMeasurement(
      pushRequest.Deserialize<TPushRequest>(serializationOptions)
      ?? throw new ArgumentNullException(nameof(pushRequest)),
      meterId,
      timestamp
    );
  }

  public JsonObject ToPushRequest(IMeasurement measurement)
  {
    return JsonSerializer.SerializeToNode(
             ToPushRequest(measurement as TMeasurement
                           ?? throw new ArgumentNullException(
                             nameof(measurement))), serializationOptions) as JsonObject
           ?? throw new ArgumentNullException(nameof(measurement));
  }

  protected abstract TMeasurement ToMeasurement(TPushRequest pushRequest,
    string meterId, DateTimeOffset timestamp);

  protected abstract TPushRequest ToPushRequest(TMeasurement measurement);
}
