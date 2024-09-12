using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Conversion.Agnostic;

public class AgnosticMeasurementRecordPushRequestConverter(
  IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public IMeterPushRequestEntity ConvertToPushRequest(IMeasurementRecord record, string messengerId)
  {
    var converter = _serviceProvider
      .GetServices<IMeasurementRecordPushRequestConverter>()
      .FirstOrDefault(c => c.CanConvertToPushRequest(record, messengerId));

    return converter?.ConvertToPushRequest(record)
      ?? throw new InvalidOperationException(
        $"No converter found for {record.GetType()}");
  }
}
