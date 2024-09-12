using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Conversion.Abstractions;

public interface IMeasurementRecordPushRequestConverter
{
  public bool CanConvertToPushRequest(
    IMeasurementRecord record,
    string messengerId);

  public IMeterPushRequestEntity
    ConvertToPushRequest(IMeasurementRecord record);
}
