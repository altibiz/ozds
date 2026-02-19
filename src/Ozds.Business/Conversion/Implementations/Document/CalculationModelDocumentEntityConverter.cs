using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class CalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelDocumentEntityConverter<
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
}
