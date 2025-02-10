using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class BlueLowNetworkUserCatalogueModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  BlueLowNetworkUserCatalogueModel,
  NetworkUserCatalogueModel,
  BlueLowNetworkUserCatalogueEntity,
  NetworkUserCatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    BlueLowNetworkUserCatalogueModel model,
    BlueLowNetworkUserCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT0Price_EUR = model.ActiveEnergyTotalImportT0Price_EUR;
    entity.ReactiveEnergyTotalRampedT0Price_EUR = model.ReactiveEnergyTotalRampedT0Price_EUR;
  }
}
