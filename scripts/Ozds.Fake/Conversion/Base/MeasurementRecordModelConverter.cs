using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion.Base;

public abstract class MeasurementRecordModelConverter<TRecord,
  TModel> : IMeasurementRecordModelConverter
  where TRecord : IMeasurementRecord
  where TModel : IMeasurement
{
  public bool CanConvertToModel(IMeasurementRecord record)
  {
    return record is TRecord;
  }

  public IMeasurement ConvertToModel(IMeasurementRecord record)
  {
    return ConvertToModel((TRecord)record);
  }

  protected abstract TModel ConvertToModel(TRecord record);
}
