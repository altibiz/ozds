using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  public Type PushRequestType { get; }

  public Type MeasurementType { get; }

  public bool CanConvertToPushRequest(IMeasurement measurement);

  public bool CanConvertToMeasurement(IMeterPushRequestEntity pushRequest);

  public IMeterPushRequestEntity ToPushRequest(IMeasurement measurement);

  public IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId
  );
}
