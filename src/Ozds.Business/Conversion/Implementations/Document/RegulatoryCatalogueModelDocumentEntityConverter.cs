using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class RegulatoryCatalogueModelDocumentEntityConverter
  : ConcreteModelDocumentEntityConverter<
      RegulatoryCatalogueModel,
      RegulatoryCatalogueEntity>
{
  public override void InitializeEntity(
    RegulatoryCatalogueModel model,
    RegulatoryCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT1Price_EUR = model.ActiveEnergyTotalImportT1Price_EUR;
    entity.ActiveEnergyTotalImportT2Price_EUR = model.ActiveEnergyTotalImportT2Price_EUR;
    entity.RenewableEnergyFeePrice_EUR = model.RenewableEnergyFeePrice_EUR;
    entity.BusinessUsageFeePrice_EUR = model.BusinessUsageFeePrice_EUR;
    entity.TaxRate_Percent = model.TaxRate_Percent;
  }
}
