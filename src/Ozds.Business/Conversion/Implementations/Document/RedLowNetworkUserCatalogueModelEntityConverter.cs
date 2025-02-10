using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class RedLowNetworkUserCatalogueModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
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
    entity.ActiveEnergyTotalImportT1Price_EUR = model.ActiveEnergyTotalImportT1Price_EUR;
    entity.ActiveEnergyTotalImportT2Price_EUR = model.ActiveEnergyTotalImportT2Price_EUR;
    entity.ActivePowerTotalImportT1Price_EUR = model.ActivePowerTotalImportT1Price_EUR;
    entity.ReactiveEnergyTotalRampedT0Price_EUR = model.ReactiveEnergyTotalRampedT0Price_EUR;
  }
}
