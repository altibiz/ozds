using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class WhiteMediumNetworkUserCatalogueModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  WhiteMediumNetworkUserCatalogueModel,
  NetworkUserCatalogueModel,
  WhiteMediumNetworkUserCatalogueEntity,
  NetworkUserCatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    WhiteMediumNetworkUserCatalogueModel model,
    WhiteMediumNetworkUserCatalogueEntity entity
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
  }
}
