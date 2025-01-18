using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class RegulatoryCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      RegulatoryCatalogueModel,
      AuditableModel,
      RegulatoryCatalogueEntity,
      AuditableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    RegulatoryCatalogueModel model,
    RegulatoryCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT1Price_EUR =
      model.ActiveEnergyTotalImportT1Price_EUR;
    entity.ActiveEnergyTotalImportT2Price_EUR =
      model.ActiveEnergyTotalImportT2Price_EUR;
    entity.RenewableEnergyFeePrice_EUR =
      model.RenewableEnergyFeePrice_EUR;
    entity.BusinessUsageFeePrice_EUR =
      model.BusinessUsageFeePrice_EUR;
    entity.TaxRate_Percent = model.TaxRate_Percent;
  }

  public override void InitializeModel(
    RegulatoryCatalogueEntity entity,
    RegulatoryCatalogueModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ActiveEnergyTotalImportT1Price_EUR =
      entity.ActiveEnergyTotalImportT1Price_EUR;
    model.ActiveEnergyTotalImportT2Price_EUR =
      entity.ActiveEnergyTotalImportT2Price_EUR;
    model.RenewableEnergyFeePrice_EUR =
      entity.RenewableEnergyFeePrice_EUR;
    model.BusinessUsageFeePrice_EUR =
      entity.BusinessUsageFeePrice_EUR;
    model.TaxRate_Percent = entity.TaxRate_Percent;
  }
}
