using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class NetworkUserModelReportEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelReportEntityConverter<
    NetworkUserModel,
    IdentifiableModel,
    NetworkUserEntity,
    IdentifiableEntity
  >(serviceProvider)
{
  private readonly ModelReportEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelReportEntityConverter>();

  public override void InitializeEntity(
    NetworkUserModel model,
    NetworkUserEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.LocationId = model.LocationId;
    entity.LegalPerson = model.LegalPerson is null
      ? null!
      : modelEntityConverter.ToEntity<LegalPersonEntity>(model.LegalPerson);
    entity.AltiBizSubProjectCode = model.AltiBizSubProjectCode;
  }

  public override void InitializeModel(
    NetworkUserEntity entity,
    NetworkUserModel model
  )
  {
    base.InitializeModel(entity, model);
    model.LocationId = entity.LocationId;
    model.LegalPerson = entity.LegalPerson is null
      ? null!
      : modelEntityConverter.ToModel<LegalPersonModel>(entity.LegalPerson);
    model.AltiBizSubProjectCode = entity.AltiBizSubProjectCode;
  }
}
