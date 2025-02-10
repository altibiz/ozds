using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class WhiteMediumNetworkUserCalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelDocumentEntityConverter<
  WhiteMediumNetworkUserCalculationModel,
  WhiteMediumNetworkUserCalculationEntity>
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    WhiteMediumNetworkUserCalculationModel model,
    WhiteMediumNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UsageActiveEnergyTotalImportT1 = modelDocumentEntityConverter
      .ToEntity<UsageActiveEnergyTotalImportT1CalculationItemEntity>(
        model.UsageActiveEnergyTotalImportT1);
    entity.UsageActiveEnergyTotalImportT2 = modelDocumentEntityConverter
      .ToEntity<UsageActiveEnergyTotalImportT2CalculationItemEntity>(
        model.UsageActiveEnergyTotalImportT2);
    entity.UsageActivePowerTotalImportT1Peak = modelDocumentEntityConverter
      .ToEntity<UsageActivePowerTotalImportT1PeakCalculationItemEntity>(
        model.UsageActivePowerTotalImportT1Peak);
    entity.UsageReactiveEnergyTotalRampedT0 = modelDocumentEntityConverter
      .ToEntity<UsageReactiveEnergyTotalRampedT0CalculationItemEntity>(
        model.UsageReactiveEnergyTotalRampedT0);
  }
}
