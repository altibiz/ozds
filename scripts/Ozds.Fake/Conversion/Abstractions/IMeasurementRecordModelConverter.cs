using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion.Abstractions;

public interface IMeasurementRecordModelConverter
{
  public bool CanConvertToModel(IMeasurementRecord record);

  public IMeasurement ConvertToModel(IMeasurementRecord record);

  public bool CanConvertToRecord(IMeasurement measurement);

  public IMeasurementRecord ConvertToRecord(IMeasurement measurement);
}
