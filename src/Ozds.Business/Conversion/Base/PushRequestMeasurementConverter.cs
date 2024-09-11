using System.Text.Json;
using System.Text.Json.Serialization;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class PushRequestMeasurementConverter<TPushRequest,
  TMeasurement> : IPushRequestMeasurementConverter
  where TMeasurement : class, IMeasurement
  where TPushRequest : class, IMeterPushRequestEntity
{
  protected abstract string MeterIdPrefix { get; }

  public bool CanConvert(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  public IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string meterId,
    DateTimeOffset timestamp)
  {
    return ToMeasurement(
      (TPushRequest)pushRequest
        ?? throw new ArgumentNullException(nameof(pushRequest)),
      meterId,
      timestamp
    );
  }

  public IMeterPushRequestEntity ToPushRequest(IMeasurement measurement)
  {
    return ToPushRequest(
      measurement as TMeasurement
        ?? throw new ArgumentNullException(nameof(measurement)));
  }

  protected abstract TMeasurement ToMeasurement(
    TPushRequest pushRequest,
    string meterId,
    DateTimeOffset timestamp);

  protected abstract TPushRequest ToPushRequest(TMeasurement measurement);
}

internal static class PushRequestMeasurementConverterOptions
{
  public static readonly JsonSerializerOptions Options =
    new()
    {
      PropertyNameCaseInsensitive = true,
      NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
}
