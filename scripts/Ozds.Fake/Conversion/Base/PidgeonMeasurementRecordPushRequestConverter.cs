using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Conversion.Base;

public abstract class PidgeonMeasurementRecordPushRequestConverter<TRecord,
  TPushRequest> : MeasurementRecordPushRequestConverter<TRecord,
    TPushRequest>
  where TRecord : IMeasurementRecord
  where TPushRequest : IMeterPushRequestEntity
{
  protected override string MessengerIdPrefix
  {
    get { return "pidgeon"; }
  }
}
