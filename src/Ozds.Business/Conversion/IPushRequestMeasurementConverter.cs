using System.ComponentModel;
using System.Globalization;

namespace Ozds.Business.Conversion;

public interface IPushRequestMeasurementConverter
{
  Type PushRequestType { get; }

  Type MeasurementType { get; }

  object ToMeasurement(object pushRequest, string meterId, DateTimeOffset timestamp);
}

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
