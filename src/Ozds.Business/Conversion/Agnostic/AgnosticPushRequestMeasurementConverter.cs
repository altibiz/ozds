using System.Text.Json.Nodes;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticPushRequestMeasurementConverter
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticPushRequestMeasurementConverter(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public JsonObject ToPushRequest(IMeasurement measurement)
  {
    return _serviceProvider
             .GetServices<IPushRequestMeasurementConverter>()
             .FirstOrDefault(converter =>
               converter.CanConvert(measurement.MeterId))
             ?.ToPushRequest(measurement)
           ?? throw new InvalidOperationException(
             $"No converter found for measurement {measurement.GetType()}.");
  }

  public IMeasurement ToMeasurement(JsonObject pushRequest, string meterId,
    DateTimeOffset timestamp)
  {
    return _serviceProvider
             .GetServices<IPushRequestMeasurementConverter>()
             .FirstOrDefault(converter => converter.CanConvert(meterId))
             ?.ToMeasurement(pushRequest, meterId, timestamp)
           ?? throw new InvalidOperationException(
             $"No converter found for meter {meterId}.");
  }

  public TMeasurement ToMeasurement<TMeasurement>(JsonObject pushRequest,
    string meterId, DateTimeOffset timestamp)
    where TMeasurement : class, IMeasurement
  {
    return ToMeasurement(pushRequest, meterId, timestamp) as TMeasurement
           ?? throw new InvalidOperationException(
             $"No converter found for meter {meterId}.");
  }
}
