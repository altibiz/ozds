using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class RedLowNetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      RedLowNetworkUserCalculationModel,
      NetworkUserCalculationModel,
      RedLowNetworkUserCalculationEntity,
      NetworkUserCalculationEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    RedLowNetworkUserCalculationModel model,
    RedLowNetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UsageNetworkUserCatalogueId =
      model.UsageNetworkUserCatalogueId;
    entity.ArchivedUsageNetworkUserCatalogue =
      model.ConcreteArchivedUsageNetworkUserCatalogue is null
        ? null!
        : modelEntityConverter.ToEntity<
            RedLowNetworkUserCatalogueEntity>(
              model.ConcreteArchivedUsageNetworkUserCatalogue);
    entity.UsageActiveEnergyTotalImportT1 =
      model.UsageActiveEnergyTotalImportT1 is null
        ? null!
        : modelEntityConverter.ToEntity<
            UsageActiveEnergyTotalImportT1CalculationItemEntity>(
              model.UsageActiveEnergyTotalImportT1);
    entity.UsageActiveEnergyTotalImportT2 =
      model.UsageActiveEnergyTotalImportT2 is null
        ? null!
        : modelEntityConverter.ToEntity<
            UsageActiveEnergyTotalImportT2CalculationItemEntity>(
              model.UsageActiveEnergyTotalImportT2);
    entity.UsageActivePowerTotalImportT1Peak =
      model.UsageActivePowerTotalImportT1Peak is null
        ? null!
        : modelEntityConverter.ToEntity<
            UsageActivePowerTotalImportT1PeakCalculationItemEntity>(
              model.UsageActivePowerTotalImportT1Peak);
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
            RedLowNetworkUserCatalogueEntity>(
              model.ConcreteArchivedUsageNetworkUserCatalogue);
  }

  public override void InitializeModel(
    RedLowNetworkUserCalculationEntity entity,
    RedLowNetworkUserCalculationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UsageNetworkUserCatalogueId =
      entity.UsageNetworkUserCatalogueId;
    model.ConcreteArchivedUsageNetworkUserCatalogue =
      entity.ArchivedUsageNetworkUserCatalogue is null
        ? null!
        : modelEntityConverter.ToModel<
            RedLowNetworkUserCatalogueModel>(
              entity.ArchivedUsageNetworkUserCatalogue);
    model.UsageActiveEnergyTotalImportT1 =
      entity.UsageActiveEnergyTotalImportT1 is null
        ? null!
        : modelEntityConverter.ToModel<
            UsageActiveEnergyTotalImportT1CalculationItemModel>(
              entity.UsageActiveEnergyTotalImportT1);
    model.UsageActiveEnergyTotalImportT2 =
      entity.UsageActiveEnergyTotalImportT2 is null
        ? null!
        : modelEntityConverter.ToModel<
            UsageActiveEnergyTotalImportT2CalculationItemModel>(
              entity.UsageActiveEnergyTotalImportT2);
    model.UsageActivePowerTotalImportT1Peak =
      entity.UsageActivePowerTotalImportT1Peak is null
        ? null!
        : modelEntityConverter.ToModel<
            UsageActivePowerTotalImportT1PeakCalculationItemModel>(
              entity.UsageActivePowerTotalImportT1Peak);
    model.UsageReactiveEnergyTotalRampedT0 =
      entity.UsageReactiveEnergyTotalRampedT0 is null
        ? null!
        : modelEntityConverter.ToModel<
            UsageReactiveEnergyTotalRampedT0CalculationItemModel>(
              entity.UsageReactiveEnergyTotalRampedT0);
    model.UsageNetworkUserCatalogueId =
      entity.UsageNetworkUserCatalogueId;
    model.ConcreteArchivedUsageNetworkUserCatalogue =
      entity.ArchivedUsageNetworkUserCatalogue is null
        ? null!
        : modelEntityConverter.ToModel<
            RedLowNetworkUserCatalogueModel>(
              entity.ArchivedUsageNetworkUserCatalogue);
  }
}
