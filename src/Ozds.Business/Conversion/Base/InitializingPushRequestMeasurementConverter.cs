using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class InitializingPushRequestMeasurementConverter
  : IPushRequestMeasurementConverter
{
  public abstract Type PushRequestType { get; }

  public abstract Type MeasurementType { get; }

  public abstract bool CanConvertToMeasurement(
    IMeterPushRequestEntity pushRequest);

  public abstract bool CanConvertToPushRequest(IMeasurement measurement);

  public abstract IMeterPushRequestEntity BoxPushRequest();

  public abstract IMeasurement BoxMeasurement();

  public abstract void InitializePushRequest(
    IMeasurement measurement,
    IMeterPushRequestEntity pushRequest);

  public abstract void InitializeMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId,
    IMeasurement measurement);

  public IMeterPushRequestEntity ToPushRequest(
    IMeasurement measurement)
  {
    var pushRequest = BoxPushRequest();
    InitializePushRequest(measurement, pushRequest);
    return pushRequest;
  }

  public IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId)
  {
    var measurement = BoxMeasurement();
    InitializeMeasurement(pushRequest, measurementLocationId, measurement);
    return measurement;
  }
}
