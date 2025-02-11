using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class BlueLowNetworkUserCalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  BlueLowNetworkUserCalculationModel,
  NetworkUserCalculationModel,
  BlueLowNetworkUserCalculationEntity,
  NetworkUserCalculationEntity>(serviceProvider)
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    BlueLowNetworkUserCalculationModel model,
    BlueLowNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UsageActiveEnergyTotalImportT0 = modelDocumentEntityConverter
      .ToEntity<UsageActiveEnergyTotalImportT0CalculationItemEntity>(
        model.UsageActiveEnergyTotalImportT0);
    entity.UsageReactiveEnergyTotalRampedT0 = modelDocumentEntityConverter
      .ToEntity<UsageReactiveEnergyTotalRampedT0CalculationItemEntity>(
        model.UsageReactiveEnergyTotalRampedT0);
    entity.ConcreteUsageNetworkUserCatalogue = modelDocumentEntityConverter
      .ToEntity<BlueLowNetworkUserCatalogueEntity>(
        model.ConcreteArchivedUsageNetworkUserCatalogue);
  }
}
