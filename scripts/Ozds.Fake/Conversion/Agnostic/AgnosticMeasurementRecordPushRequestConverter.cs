using System.Text.Json.Nodes;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion.Agnostic;

public class AgnosticMeasurementRecordPushRequestConverter
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticMeasurementRecordPushRequestConverter(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }


  public JsonObject ConvertToPushRequest(IMeasurementRecord record)
  {
    var converter = _serviceProvider
      .GetServices<IMeasurementRecordPushRequestConverter>()
      .FirstOrDefault(c => c.CanConvertToPushRequest(record));

    return converter?.ConvertToPushRequest(record)
           ?? throw new InvalidOperationException(
             $"No converter found for {record.GetType()}");
  }
}
