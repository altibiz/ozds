using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticPushRequestMeasurementConverter(
  IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public IMeterPushRequestEntity ToPushRequest(IMeasurement measurement)
  {
    return _serviceProvider
        .GetServices<IPushRequestMeasurementConverter>()
        .FirstOrDefault(
          converter =>
            converter.CanConvert(measurement.MeterId))
        ?.ToPushRequest(measurement)
      ?? throw new InvalidOperationException(
        $"No converter found for measurement {measurement.GetType()}.");
  }

  public IMeasurement ToMeasurement(IMeterPushRequestEntity pushRequest)
  {
    return _serviceProvider
        .GetServices<IPushRequestMeasurementConverter>()
        .FirstOrDefault(converter => converter.CanConvert(pushRequest.MeterId))
        ?.ToMeasurement(pushRequest)
      ?? throw new InvalidOperationException(
        $"No converter found for meter {pushRequest.MeterId}.");
  }

  public TMeasurement ToMeasurement<TMeasurement>(
    IMeterPushRequestEntity pushRequest)
    where TMeasurement : class, IMeasurement
  {
    return ToMeasurement(pushRequest) as TMeasurement
      ?? throw new InvalidOperationException(
        $"No converter found for meter {pushRequest.MeterId}.");
  }
}
