namespace Ozds.Business.Conversion.Abstractions;

public interface IPushRequestMeasurementConverter
{
  Type PushRequestType { get; }

  Type MeasurementType { get; }

  object ToMeasurement(object pushRequest, string meterId, DateTimeOffset timestamp);
}
