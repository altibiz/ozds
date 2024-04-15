using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class WhiteLowNetworkUserCalculationModelEntityConverter :
  ModelEntityConverter<
    WhiteLowNetworkUserCalculationModel, WhiteLowNetworkUserCalculationEntity>
{
  protected override WhiteLowNetworkUserCalculationEntity ToEntity(
    WhiteLowNetworkUserCalculationModel model)
  {
    return model.ToEntity();
  }

  protected override WhiteLowNetworkUserCalculationModel ToModel(
    WhiteLowNetworkUserCalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class WhiteLowNetworkUserCalculationModelEntityConverterExtensions
{
  public static WhiteLowNetworkUserCalculationModel ToModel(
    this WhiteLowNetworkUserCalculationEntity entity)
  {
    return new WhiteLowNetworkUserCalculationModel
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
      UsageActiveEnergyTotalImportT1 = entity.UsageActiveEnergyTotalImportT1.ToModel(),
      UsageActiveEnergyTotalImportT2 = entity.UsageActiveEnergyTotalImportT2.ToModel(),
      UsageReactiveEnergyTotalRampedT0 =
        entity.UsageReactiveEnergyTotalRampedT0.ToModel(),
      UsageMeterFee = entity.UsageMeterFee.ToModel(),
      SupplyActiveEnergyTotalImportT1 = entity.SupplyActiveEnergyTotalImportT1.ToModel(),
      SupplyActiveEnergyTotalImportT2 = entity.SupplyActiveEnergyTotalImportT2.ToModel(),
      SupplyBusinessUsageFee = entity.SupplyBusinessUsageFee.ToModel(),
      SupplyRenewableEnergyFee = entity.SupplyRenewableEnergyFee.ToModel(),
    };
  }

  public static WhiteLowNetworkUserCalculationEntity ToEntity(
    this WhiteLowNetworkUserCalculationModel model)
  {
    return new WhiteLowNetworkUserCalculationEntity
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
      UsageActiveEnergyTotalImportT1 = model.UsageActiveEnergyTotalImportT1.ToEntity(),
      UsageActiveEnergyTotalImportT2 = model.UsageActiveEnergyTotalImportT2.ToEntity(),
      UsageMeterFee = model.UsageMeterFee.ToEntity(),
      SupplyActiveEnergyTotalImportT1 = model.SupplyActiveEnergyTotalImportT1.ToEntity(),
      SupplyActiveEnergyTotalImportT2 = model.SupplyActiveEnergyTotalImportT2.ToEntity(),
      SupplyBusinessUsageFee = model.SupplyBusinessUsageFee.ToEntity(),
      SupplyRenewableEnergyFee = model.SupplyRenewableEnergyFee.ToEntity(),
    };
  }
}
