using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RedLowNetworkUserCalculationModelEntityConverter :
  ModelEntityConverter<
    RedLowNetworkUserCalculationModel, RedLowNetworkUserCalculationEntity>
{
  protected override RedLowNetworkUserCalculationEntity ToEntity(
    RedLowNetworkUserCalculationModel model)
  {
    return model.ToEntity();
  }

  protected override RedLowNetworkUserCalculationModel ToModel(
    RedLowNetworkUserCalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class RedLowNetworkUserCalculationModelEntityConverterExtensions
{
  public static RedLowNetworkUserCalculationModel ToModel(
    this RedLowNetworkUserCalculationEntity entity)
  {
    return new RedLowNetworkUserCalculationModel
    {
      Id = entity.Id,
      Title = entity.Title,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      MeterId = entity.MeterId,
      NetworkUserMeasurementLocationId =
        entity.NetworkUserMeasurementLocationId,
      UsageNetworkUserCatalogueId = entity.UsageNetworkUserCatalogueId,
      SupplyRegulatoryCatalogueId = entity.SupplyRegulatoryCatalogueId,
      NetworkUserInvoiceId = entity.NetworkUserInvoiceId,
      ArchivedNetworkUserMeasurementLocation =
        entity.ArchivedNetworkUserMeasurementLocation.ToModel(),
      ArchivedSupplyRegulatoryCatalogue =
        entity.ArchivedSupplyRegulatoryCatalogue.ToModel(),
      ConcreteArchivedUsageNetworkUserCatalogue =
        entity.ArchivedUsageNetworkUserCatalogue.ToModel(),
      ArchivedMeter = entity.ArchivedMeter.ToModel(),
      UsageActiveEnergyTotalImportT1 =
        entity.UsageActiveEnergyTotalImportT1.ToModel(),
      UsageActiveEnergyTotalImportT2 =
        entity.UsageActiveEnergyTotalImportT2.ToModel(),
      UsageActivePowerTotalImportT1Peak =
        entity.UsageActivePowerTotalImportT1Peak.ToModel(),
      UsageReactiveEnergyTotalRampedT0 =
        entity.UsageReactiveEnergyTotalRampedT0.ToModel(),
      UsageMeterFee = entity.UsageMeterFee.ToModel(),
      SupplyActiveEnergyTotalImportT1 =
        entity.SupplyActiveEnergyTotalImportT1.ToModel(),
      SupplyActiveEnergyTotalImportT2 =
        entity.SupplyActiveEnergyTotalImportT2.ToModel(),
      SupplyBusinessUsageFee = entity.SupplyBusinessUsageFee.ToModel(),
      SupplyRenewableEnergyFee = entity.SupplyRenewableEnergyFee.ToModel(),
      SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR,
      UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static RedLowNetworkUserCalculationEntity ToEntity(
    this RedLowNetworkUserCalculationModel model)
  {
    return new RedLowNetworkUserCalculationEntity
    {
      Id = model.Id,
      Title = model.Title,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      MeterId = model.MeterId,
      NetworkUserMeasurementLocationId = model.NetworkUserMeasurementLocationId,
      UsageNetworkUserCatalogueId = model.UsageNetworkUserCatalogueId,
      SupplyRegulatoryCatalogueId = model.SupplyRegulatoryCatalogueId,
      NetworkUserInvoiceId = model.NetworkUserInvoiceId,
      ArchivedNetworkUserMeasurementLocation =
        model.ArchivedNetworkUserMeasurementLocation.ToEntity(),
      ArchivedSupplyRegulatoryCatalogue =
        model.ArchivedSupplyRegulatoryCatalogue.ToEntity(),
      ArchivedUsageNetworkUserCatalogue =
        model.ConcreteArchivedUsageNetworkUserCatalogue.ToEntity(),
      ArchivedMeter = model.ArchivedMeter.ToEntity(),
      UsageActiveEnergyTotalImportT1 =
        model.UsageActiveEnergyTotalImportT1.ToEntity(),
      UsageActiveEnergyTotalImportT2 =
        model.UsageActiveEnergyTotalImportT2.ToEntity(),
      UsageActivePowerTotalImportT1Peak =
        model.UsageActivePowerTotalImportT1Peak.ToEntity(),
      UsageReactiveEnergyTotalRampedT0 =
        model.UsageReactiveEnergyTotalRampedT0.ToEntity(),
      UsageMeterFee = model.UsageMeterFee.ToEntity(),
      SupplyActiveEnergyTotalImportT1 =
        model.SupplyActiveEnergyTotalImportT1.ToEntity(),
      SupplyActiveEnergyTotalImportT2 =
        model.SupplyActiveEnergyTotalImportT2.ToEntity(),
      SupplyBusinessUsageFee = model.SupplyBusinessUsageFee.ToEntity(),
      SupplyRenewableEnergyFee = model.SupplyRenewableEnergyFee.ToEntity(),
      SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR,
      UsageFeeTotal_EUR = model.UsageFeeTotal_EUR,
      Total_EUR = model.Total_EUR
    };
  }
}
