using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class ConcretePushRequestMeasurementConverter<
  TPushRequest,
  TMeasurement> : IPushRequestMeasurementConverter
  where TMeasurement : IMeasurement
  where TPushRequest : IMeterPushRequestEntity
{
  public abstract string MeterIdPrefix { get; }

  public abstract TPushRequest ToPushRequest(TMeasurement measurement);

  public abstract TMeasurement ToMeasurement(
    TPushRequest pushRequest,
    string measurementLocationId
  );

  public virtual Type PushRequestType => typeof(TPushRequest);

  public virtual Type MeasurementType => typeof(TMeasurement);

  public virtual bool CanConvertToPushRequest(
    IMeasurement measurement
  )
  {
    return measurement.MeterId.StartsWith(MeterIdPrefix);
  }

  public virtual bool CanConvertToMeasurement(
    IMeterPushRequestEntity pushRequest
  )
  {
    return pushRequest.MeterId.StartsWith(MeterIdPrefix);
  }

  public virtual IMeterPushRequestEntity ToPushRequest(IMeasurement measurement)
  {
    return ToPushRequest((TMeasurement)measurement);
  }

  public virtual IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId
  )
  {
    return ToMeasurement((TPushRequest)pushRequest, measurementLocationId);
  }
}
