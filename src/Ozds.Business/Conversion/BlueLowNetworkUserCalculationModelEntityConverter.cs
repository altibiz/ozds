using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class BlueLowNetworkUserCalculationModelEntityConverter :
  ModelEntityConverter<
    BlueLowNetworkUserCalculationModel, BlueLowNetworkUserCalculationEntity>
{
  protected override BlueLowNetworkUserCalculationEntity ToEntity(
    BlueLowNetworkUserCalculationModel model)
  {
    return model.ToEntity();
  }

  protected override BlueLowNetworkUserCalculationModel ToModel(
    BlueLowNetworkUserCalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class BlueLowNetworkUserCalculationModelEntityConverterExtensions
{
  public static BlueLowNetworkUserCalculationModel ToModel(
    this BlueLowNetworkUserCalculationEntity entity)
  {
    return new BlueLowNetworkUserCalculationModel
    {
      Id = entity.Id,
      Title = entity.Title,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      MeterId = entity.MeterId,
      MeasurementLocationId = entity.MeasurementLocationId,
      UsageNetworkUserCatalogueId = entity.UsageNetworkUserCatalogueId,
      SupplyRegulatoryCatalogueId = entity.SupplyRegulatoryCatalogueId,
      NetworkUserInvoiceId = entity.NetworkUserInvoiceId,
      ArchivedMeasurementLocation =
        entity.ArchivedMeasurementLocation.ToModel(),
      ArchivedSupplyRegulatoryCatalogue =
        entity.ArchivedSupplyRegulatoryCatalogue.ToModel(),
      ArchivedUsageNetworkUserCatalogue =
        entity.ArchivedUsageNetworkUserCatalogue.ToModel(),
      ArchivedMeter = entity.ArchivedMeter.ToModel(),
      UsageActiveEnergyTotalImportT0 = entity.UsageActiveEnergyTotalImportT0.ToModel(),
      UsageReactiveEnergyTotalRampedT0 = entity.UsageReactiveEnergyTotalRampedT0.ToModel(),
      UsageMeterFee = entity.UsageMeterFee.ToModel(),
      SupplyActiveEnergyTotalImportT1 = entity.SupplyActiveEnergyTotalImportT1.ToModel(),
      SupplyActiveEnergyTotalImportT2 = entity.SupplyActiveEnergyTotalImportT2.ToModel(),
      SupplyBusinessUsageFee = entity.SupplyBusinessUsageFee.ToModel(),
      SupplyRenewableEnergyFee = entity.SupplyRenewableEnergyFee.ToModel(),
      UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR,
      SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR,
      Total_EUR = entity.Total_EUR,
    };
  }

  public static BlueLowNetworkUserCalculationEntity ToEntity(
    this BlueLowNetworkUserCalculationModel model)
  {
    return new BlueLowNetworkUserCalculationEntity
    {
      Id = model.Id,
      Title = model.Title,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      MeterId = model.MeterId,
      MeasurementLocationId = model.MeasurementLocationId,
      UsageNetworkUserCatalogueId = model.UsageNetworkUserCatalogueId,
      SupplyRegulatoryCatalogueId = model.SupplyRegulatoryCatalogueId,
      NetworkUserInvoiceId = model.NetworkUserInvoiceId,
      ArchivedMeasurementLocation =
        model.ArchivedMeasurementLocation.ToEntity(),
      ArchivedSupplyRegulatoryCatalogue =
        model.ArchivedSupplyRegulatoryCatalogue.ToEntity(),
      ArchivedUsageNetworkUserCatalogue =
        model.ArchivedUsageNetworkUserCatalogue.ToEntity(),
      ArchivedMeter = model.ArchivedMeter.ToEntity(),
      UsageActiveEnergyTotalImportT0 = model.UsageActiveEnergyTotalImportT0.ToEntity(),
      UsageReactiveEnergyTotalRampedT0 = model.UsageReactiveEnergyTotalRampedT0.ToEntity(),
      UsageMeterFee = model.UsageMeterFee.ToEntity(),
      SupplyActiveEnergyTotalImportT1 = model.SupplyActiveEnergyTotalImportT1.ToEntity(),
      SupplyActiveEnergyTotalImportT2 = model.SupplyActiveEnergyTotalImportT2.ToEntity(),
      SupplyBusinessUsageFee = model.SupplyBusinessUsageFee.ToEntity(),
      SupplyRenewableEnergyFee = model.SupplyRenewableEnergyFee.ToEntity(),
      UsageFeeTotal_EUR = model.UsageFeeTotal_EUR,
      SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR,
      Total_EUR = model.Total_EUR,
    };
  }
}
