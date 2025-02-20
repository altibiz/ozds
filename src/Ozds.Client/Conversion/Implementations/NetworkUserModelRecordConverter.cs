using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class NetworkUserModelRecordConverter(IServiceProvider serviceProvider)
  : InheritingModelRecordConverter<
    NetworkUserModel,
    IdentifiableModel,
    NetworkUserRecord,
    IdentifiableRecord>(serviceProvider)
{
  private readonly ModelRecordConverter modelRecordConverter =
    serviceProvider.GetRequiredService<ModelRecordConverter>();

  public override void InitializeRecord(
    NetworkUserModel model,
    NetworkUserRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.LocationId = model.LocationId;
    record.LegalPerson = model.LegalPerson is null
      ? null!
      : modelRecordConverter.ToRecord<LegalPersonRecord>(model.LegalPerson);
    record.AltiBizSubProjectCode = model.AltiBizSubProjectCode;
  }

  public override void InitializeModel(
    NetworkUserRecord record,
    NetworkUserModel model
  )
  {
    base.InitializeModel(record, model);
    model.LocationId = record.LocationId;
    model.LegalPerson = record.LegalPerson is null
      ? null!
      : modelRecordConverter.ToModel<LegalPersonModel>(record.LegalPerson);
    model.AltiBizSubProjectCode = record.AltiBizSubProjectCode;
  }
}
