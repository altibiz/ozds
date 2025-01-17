using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  Type PushRequestType { get; }

  Type MeasurementType { get; }

  bool CanConvertToPushRequest(IMeasurement measurement);

  bool CanConvertToMeasurement(IMeterPushRequestEntity pushRequest);

  IMeterPushRequestEntity ToPushRequest(IMeasurement measurement);

  IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId
  );
}
