using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class PushRequestMeasurementConverter<TPushRequest, TMeasurement> : IPushRequestMeasurementConverter
  where TPushRequest : class
  where TMeasurement : class
{
  public Type PushRequestType => typeof(TPushRequest);

  public Type MeasurementType => typeof(TMeasurement);

  public object ToMeasurement(object pushRequest, string meterId, DateTimeOffset timestamp) =>
    ToMeasurement(
      pushRequest as TPushRequest
        ?? throw new ArgumentNullException(nameof(pushRequest)),
      meterId,
      timestamp
    );

  public abstract TMeasurement ToMeasurement(TPushRequest pushRequest, string meterId, DateTimeOffset timestamp);
}
