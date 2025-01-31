using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class ConcretePushRequestMeasurementConverter<
  TPushRequest,
  TMeasurement> : InitializingPushRequestMeasurementConverter
  where TMeasurement : IMeasurement
  where TPushRequest : IMeterPushRequestEntity
{
  public abstract string MeterIdPrefix { get; }

  public override Type PushRequestType
  {
    get { return typeof(TPushRequest); }
  }

  public override Type MeasurementType
  {
    get { return typeof(TMeasurement); }
  }

  public abstract void InitializePushRequest(
    TMeasurement measurement,
    TPushRequest pushRequest
  );

  public abstract void InitializeMeasurement(
    TPushRequest pushRequest,
    string measurementLocationId,
    TMeasurement measurement
  );

  public override bool CanConvertToPushRequest(
    IMeasurement measurement
  )
  {
    return measurement.MeterId.StartsWith(MeterIdPrefix);
  }

  public override bool CanConvertToMeasurement(
    IMeterPushRequestEntity pushRequest
  )
  {
    return pushRequest.MeterId.StartsWith(MeterIdPrefix);
  }

  public override IMeterPushRequestEntity BoxPushRequest()
  {
    return Activator.CreateInstance<TPushRequest>();
  }

  public override IMeasurement BoxMeasurement()
  {
    return Activator.CreateInstance<TMeasurement>();
  }

  public override void InitializePushRequest(
    IMeasurement measurement,
    IMeterPushRequestEntity pushRequest)
  {
    InitializePushRequest(
      (TMeasurement)measurement,
      (TPushRequest)pushRequest);
  }

  public override void InitializeMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId,
    IMeasurement measurement)
  {
    InitializeMeasurement(
      (TPushRequest)pushRequest,
      measurementLocationId,
      (TMeasurement)measurement);
  }
}
