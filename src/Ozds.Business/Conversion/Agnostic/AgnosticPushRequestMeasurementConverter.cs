using System.Collections.Concurrent;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticPushRequestMeasurementConverter(
  IServiceProvider serviceProvider
)
{
  public TPushRequest ToPushRequest<TPushRequest>(
    IMeasurement measurement
  )
  {
    return (TPushRequest)ToPushRequest(measurement);
  }

  public TMeasurement ToMeasurement<TMeasurement>(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId
  )
    where TMeasurement : IMeasurement
  {
    return (TMeasurement)ToMeasurement(pushRequest, measurementLocationId);
  }

  public IMeterPushRequestEntity ToPushRequest(IMeasurement measurement)
  {
    if (pushRequestCache.TryGetValue(measurement.GetType(), out var converter))
    {
      return converter.ToPushRequest(measurement);
    }

    converter = serviceProvider
        .GetServices<IPushRequestMeasurementConverter>()
        .FirstOrDefault(
          converter =>
            converter.CanConvertToPushRequest(measurement))
      ?? throw new InvalidOperationException(
        $"No converter found for measurement {measurement.GetType()}.");

    pushRequestCache.TryAdd(measurement.GetType(), converter);

    return converter.ToPushRequest(measurement);
  }

  public IMeasurement ToMeasurement(
    IMeterPushRequestEntity pushRequest,
    string measurementLocationId
  )
  {
    if (measurementCache.TryGetValue(pushRequest.GetType(), out var converter))
    {
      return converter.ToMeasurement(pushRequest, measurementLocationId);
    }

    converter = serviceProvider
      .GetServices<IPushRequestMeasurementConverter>()
      .FirstOrDefault(converter => converter
        .CanConvertToMeasurement(pushRequest))
      ?? throw new InvalidOperationException(
        $"No converter found for meter {pushRequest.MeterId}.");

    measurementCache.TryAdd(pushRequest.GetType(), converter);

    return converter.ToMeasurement(pushRequest, measurementLocationId);
  }

  private readonly ConcurrentDictionary<Type, IPushRequestMeasurementConverter>
    pushRequestCache = new();

  private readonly ConcurrentDictionary<Type, IPushRequestMeasurementConverter>
    measurementCache = new();
}
