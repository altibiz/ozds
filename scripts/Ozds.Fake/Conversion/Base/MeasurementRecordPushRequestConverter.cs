using System.Text.Json;
using System.Text.Json.Nodes;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion.Base;

public abstract class MeasurementRecordPushRequestConverter<TRecord, TPushRequest> : IMeasurementRecordPushRequestConverter
  where TRecord : IMeasurementRecord
{
  public bool CanConvertToPushRequest(IMeasurementRecord record) =>
    record is TRecord;

  public JsonObject ConvertToPushRequest(IMeasurementRecord record) =>
    JsonSerializer
      .SerializeToNode(ConvertToPushRequest((TRecord)record)!) as JsonObject
        ?? throw new InvalidOperationException();

  protected abstract TPushRequest ConvertToPushRequest(TRecord record);
}
