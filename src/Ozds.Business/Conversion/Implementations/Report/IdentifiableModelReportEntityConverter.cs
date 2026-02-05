using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations;

public class IdentifiableModelReportEntityConverter
  : ConcreteModelReportEntityConverter<IdentifiableModel, IdentifiableEntity>
{
  public override void InitializeEntity(
    IdentifiableModel model,
    IdentifiableEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Title = model.Title;
  }

  public override void InitializeModel(
    IdentifiableEntity entity,
    IdentifiableModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Id = "0";
    model.Title = entity.Title;
  }
}
