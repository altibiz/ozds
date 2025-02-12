using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Business.Conversion.Implementations;

public class IdentifiableModelRecordConverter
  : ConcreteModelRecordConverter<IdentifiableModel, IdentifiableRecord>
{
  public override void InitializeRecord(
    IdentifiableModel model,
    IdentifiableRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.Title = model.Title;
  }

  public override void InitializeModel(
    IdentifiableRecord record,
    IdentifiableModel model)
  {
    base.InitializeModel(record, model);
    model.Id = "0";
    model.Title = record.Title;
  }
}
