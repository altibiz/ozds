using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class WhiteLowNetworkUserCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      WhiteLowNetworkUserCatalogueModel,
      NetworkUserCatalogueModel,
      WhiteLowNetworkUserCatalogueEntity,
      NetworkUserCatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    WhiteLowNetworkUserCatalogueModel model,
    WhiteLowNetworkUserCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT1Price_EUR =
      model.ActiveEnergyTotalImportT1Price_EUR;
    entity.ActiveEnergyTotalImportT2Price_EUR =
      model.ActiveEnergyTotalImportT2Price_EUR;
    entity.ReactiveEnergyTotalRampedT0Price_EUR =
      model.ReactiveEnergyTotalRampedT0Price_EUR;
  }

  public override void InitializeModel(
    WhiteLowNetworkUserCatalogueEntity entity,
    WhiteLowNetworkUserCatalogueModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ActiveEnergyTotalImportT1Price_EUR =
      entity.ActiveEnergyTotalImportT1Price_EUR;
    model.ActiveEnergyTotalImportT2Price_EUR =
      entity.ActiveEnergyTotalImportT2Price_EUR;
    model.ReactiveEnergyTotalRampedT0Price_EUR =
      entity.ReactiveEnergyTotalRampedT0Price_EUR;
  }
}
