using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class BlueLowNetworkUserCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      BlueLowNetworkUserCatalogueModel,
      NetworkUserCatalogueModel,
      BlueLowNetworkUserCatalogueEntity,
      NetworkUserCatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    BlueLowNetworkUserCatalogueModel model,
    BlueLowNetworkUserCatalogueEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT0Price_EUR =
      model.ActiveEnergyTotalImportT0Price_EUR;
    entity.ReactiveEnergyTotalRampedT0Price_EUR =
      model.ReactiveEnergyTotalRampedT0Price_EUR;
  }

  public override void InitializeModel(
    BlueLowNetworkUserCatalogueEntity entity,
    BlueLowNetworkUserCatalogueModel model)
  {
    base.InitializeModel(entity, model);
    model.ActiveEnergyTotalImportT0Price_EUR =
      entity.ActiveEnergyTotalImportT0Price_EUR;
    model.ReactiveEnergyTotalRampedT0Price_EUR =
      entity.ReactiveEnergyTotalRampedT0Price_EUR;
  }
}
