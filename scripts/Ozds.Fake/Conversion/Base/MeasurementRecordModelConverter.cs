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

  public bool CanConvertToRecord(IMeasurement measurement)
  {
    return measurement is TModel;
  }

  public IMeasurement ConvertToModel(IMeasurementRecord record)
  {
    return ConvertToModel((TRecord)record);
  }

  public IMeasurementRecord ConvertToRecord(IMeasurement measurement)
  {
    return ConvertToRecord((TModel)measurement);
  }

  protected abstract TModel ConvertToModel(TRecord record);

  protected abstract TRecord ConvertToRecord(TModel model);
}
