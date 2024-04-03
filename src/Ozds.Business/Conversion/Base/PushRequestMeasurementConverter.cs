using System.Text.Json;
using System.Text.Json.Nodes;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class PushRequestMeasurementConverter<TPushRequest, TMeasurement> : IPushRequestMeasurementConverter
  where TMeasurement : class, IMeasurement
{
  protected abstract string MeterIdPrefix { get; }

  protected abstract TMeasurement ToMeasurement(TPushRequest pushRequest, string meterId, DateTimeOffset timestamp);

  public bool CanConvertToMeasurement(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  public IMeasurement ToMeasurement(JsonObject pushRequest, string meterId, DateTimeOffset timestamp) =>
    ToMeasurement(
      JsonSerializer.Deserialize<TPushRequest>(pushRequest)
        ?? throw new ArgumentNullException(nameof(pushRequest)),
      meterId,
      timestamp
    );
}
