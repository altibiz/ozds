using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Business.Conversion.Implementations;

public class MeasurementModelRecordConverter
  : ConcreteModelRecordConverter<MeasurementModel, MeasurementRecord>
{
  public override void InitializeRecord(
    MeasurementModel model,
    MeasurementRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.MeterId = model.MeterId;
    record.MeasurementLocationId = model.MeasurementLocationId;
    record.Timestamp = model.Timestamp;
  }

  public override void InitializeModel(
    MeasurementRecord record,
    MeasurementModel model)
  {
    base.InitializeModel(record, model);
    model.MeterId = record.MeterId;
    model.MeasurementLocationId = record.MeasurementLocationId;
    model.Timestamp = record.Timestamp;
  }
}
