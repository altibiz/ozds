using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  bool CanConvert(string meterId);

  IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string meterId,
    DateTimeOffset timestamp);

  IMeterPushRequestEntity ToPushRequest(IMeasurement measurement);
}
