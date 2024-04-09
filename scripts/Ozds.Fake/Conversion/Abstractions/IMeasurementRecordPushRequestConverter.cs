using System.Text.Json.Nodes;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion.Abstractions;

public interface IMeasurementRecordPushRequestConverter
{
  public bool CanConvertToPushRequest(IMeasurementRecord record);

  public JsonObject ConvertToPushRequest(IMeasurementRecord record);
}
