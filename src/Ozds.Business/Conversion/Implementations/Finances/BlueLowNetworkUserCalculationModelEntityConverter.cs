using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class BlueLowNetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  BlueLowNetworkUserCalculationModel,
  NetworkUserCalculationModel,
  BlueLowNetworkUserCalculationEntity,
  NetworkUserCalculationEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    BlueLowNetworkUserCalculationModel model,
    BlueLowNetworkUserCalculationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.UsageActiveEnergyTotalImportT0 =
      model.UsageActiveEnergyTotalImportT0 is null
        ? null!
        : modelEntityConverter.ToEntity<
          UsageActiveEnergyTotalImportT0CalculationItemEntity>(
          model.UsageActiveEnergyTotalImportT0);
    entity.UsageReactiveEnergyTotalRampedT0 =
      model.UsageReactiveEnergyTotalRampedT0 is null
        ? null!
        : modelEntityConverter.ToEntity<
          UsageReactiveEnergyTotalRampedT0CalculationItemEntity>(
          model.UsageReactiveEnergyTotalRampedT0);
    entity.UsageNetworkUserCatalogueId =
      model.UsageNetworkUserCatalogueId;
    entity.ArchivedUsageNetworkUserCatalogue =
      model.ConcreteArchivedUsageNetworkUserCatalogue is null
        ? null!
        : modelEntityConverter.ToEntity<
          BlueLowNetworkUserCatalogueEntity>(
          model.ConcreteArchivedUsageNetworkUserCatalogue);
    if (entity.ArchivedUsageNetworkUserCatalogue
      is { } archivedUsageNetworkUserCatalogue)
    {
      archivedUsageNetworkUserCatalogue.Kind =
        archivedUsageNetworkUserCatalogue.GetType().Name;
    }
  }

  public override void InitializeModel(
    BlueLowNetworkUserCalculationEntity entity,
    BlueLowNetworkUserCalculationModel model)
  {
    base.InitializeModel(entity, model);
    model.UsageActiveEnergyTotalImportT0 =
      entity.UsageActiveEnergyTotalImportT0 is null
        ? null!
        : modelEntityConverter
          .ToModel<UsageActiveEnergyTotalImportT0CalculationItemModel>(
            entity.UsageActiveEnergyTotalImportT0);
    model.UsageReactiveEnergyTotalRampedT0 =
      entity.UsageReactiveEnergyTotalRampedT0 is null
        ? null!
        : modelEntityConverter
          .ToModel<UsageReactiveEnergyTotalRampedT0CalculationItemModel>(
            entity.UsageReactiveEnergyTotalRampedT0);
    model.UsageNetworkUserCatalogueId =
      entity.UsageNetworkUserCatalogueId;
    model.ConcreteArchivedUsageNetworkUserCatalogue =
      entity.ArchivedUsageNetworkUserCatalogue is null
        ? null!
        : modelEntityConverter.ToModel<
          BlueLowNetworkUserCatalogueModel>(
          entity.ArchivedUsageNetworkUserCatalogue);
  }
}
