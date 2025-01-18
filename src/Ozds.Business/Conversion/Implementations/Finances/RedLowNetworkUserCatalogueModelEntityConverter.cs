using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class RedLowNetworkUserCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      RedLowNetworkUserCatalogueModel,
      NetworkUserCatalogueModel,
      RedLowNetworkUserCatalogueEntity,
      NetworkUserCatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    RedLowNetworkUserCatalogueModel model,
    RedLowNetworkUserCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT1Price_EUR =
      model.ActiveEnergyTotalImportT1Price_EUR;
    entity.ActiveEnergyTotalImportT2Price_EUR =
      model.ActiveEnergyTotalImportT2Price_EUR;
    entity.ActivePowerTotalImportT1Price_EUR =
      model.ActivePowerTotalImportT1Price_EUR;
    entity.ReactiveEnergyTotalRampedT0Price_EUR =
      model.ReactiveEnergyTotalRampedT0Price_EUR;
    entity.MeterFeePrice_EUR = model.MeterFeePrice_EUR;
  }

  public override void InitializeModel(
    RedLowNetworkUserCatalogueEntity entity,
    RedLowNetworkUserCatalogueModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ActiveEnergyTotalImportT1Price_EUR =
      entity.ActiveEnergyTotalImportT1Price_EUR;
    model.ActiveEnergyTotalImportT2Price_EUR =
      entity.ActiveEnergyTotalImportT2Price_EUR;
    model.ActivePowerTotalImportT1Price_EUR =
      entity.ActivePowerTotalImportT1Price_EUR;
    model.ReactiveEnergyTotalRampedT0Price_EUR =
      entity.ReactiveEnergyTotalRampedT0Price_EUR;
    model.MeterFeePrice_EUR = entity.MeterFeePrice_EUR;
  }
}
