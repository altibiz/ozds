using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Conversion.Base;

public abstract class MeasurementRecordPushRequestConverter<TRecord,
  TPushRequest> : IMeasurementRecordPushRequestConverter
  where TRecord : IMeasurementRecord
  where TPushRequest : IMeterPushRequestEntity
{
  public bool CanConvertToPushRequest(IMeasurementRecord record, string messengerId)
  {
    return record is TRecord && messengerId.StartsWith(MessengerIdPrefix);
  }

  protected abstract string MessengerIdPrefix { get; }

  public IMeterPushRequestEntity ConvertToPushRequest(IMeasurementRecord record)
  {
    return ConvertToPushRequest((TRecord)record);
  }

  protected abstract TPushRequest ConvertToPushRequest(TRecord record);
}
