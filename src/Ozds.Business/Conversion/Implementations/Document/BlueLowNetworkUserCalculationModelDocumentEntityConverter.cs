using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class BlueLowNetworkUserCalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelDocumentEntityConverter<
  BlueLowNetworkUserCalculationModel,
  BlueLowNetworkUserCalculationEntity>
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
  }
}
