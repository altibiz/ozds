using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculationModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    CalculationModel,
    FinancialModel,
    CalculationEntity,
    FinancialEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    CalculationModel model,
    CalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.RequestedFromDate = model.RequestedFromDate;
    entity.RequestedToDate = model.RequestedToDate;
    entity.MeteredFromDate = model.MeteredFromDate;
    entity.MeteredToDate = model.MeteredToDate;
  }

  public override void InitializeModel(
    CalculationEntity entity,
    CalculationModel model
  )
  {
    base.InitializeModel(entity, model);

    model.RequestedFromDate = entity.RequestedFromDate;
    model.RequestedToDate = entity.RequestedToDate;
    model.MeteredFromDate = entity.MeteredFromDate;
    model.MeteredToDate = entity.MeteredToDate;
  }
}
