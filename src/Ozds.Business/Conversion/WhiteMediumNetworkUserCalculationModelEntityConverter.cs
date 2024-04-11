using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class WhiteMediumNetworkUserCalculationModelEntityConverter :
  ModelEntityConverter<
    WhiteMediumNetworkUserCalculationModel,
    WhiteMediumNetworkUserCalculationEntity>
{
  protected override WhiteMediumNetworkUserCalculationEntity ToEntity(
    WhiteMediumNetworkUserCalculationModel model)
  {
    return model.ToEntity();
  }

  protected override WhiteMediumNetworkUserCalculationModel ToModel(
    WhiteMediumNetworkUserCalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class
  WhiteMediumNetworkUserCalculationModelEntityConverterExtensions
{
  public static WhiteMediumNetworkUserCalculationModel ToModel(
    this WhiteMediumNetworkUserCalculationEntity entity)
  {
    return new WhiteMediumNetworkUserCalculationModel
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
      ArchivedMeasurementLocation = entity.ArchivedMeasurementLocation.ToModel(),
      ArchivedSupplyRegulatoryCatalogue = entity.ArchivedSupplyRegulatoryCatalogue.ToModel(),
      ArchivedUsageNetworkUserCatalogue = entity.ArchivedUsageNetworkUserCatalogue.ToModel(),
      ArchivedMeter = entity.ArchivedMeter.ToModel(),
      ActiveEnergyTotalImportT1 = entity.ActiveEnergyTotalImportT1.ToModel(),
      ActiveEnergyTotalImportT2 = entity.ActiveEnergyTotalImportT2.ToModel(),
      ActivePowerTotalImportT1Peak = entity.ActivePowerTotalImportT1Peak.ToModel(),
      ReactiveEnergyTotalRampedT0 = entity.ReactiveEnergyTotalRampedT0.ToModel(),
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR,
    };
  }

  public static WhiteMediumNetworkUserCalculationEntity ToEntity(
    this WhiteMediumNetworkUserCalculationModel model)
  {
    return new WhiteMediumNetworkUserCalculationEntity
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
      ArchivedMeasurementLocation = model.ArchivedMeasurementLocation.ToEntity(),
      ArchivedSupplyRegulatoryCatalogue = model.ArchivedSupplyRegulatoryCatalogue.ToEntity(),
      ArchivedUsageNetworkUserCatalogue = model.ArchivedUsageNetworkUserCatalogue.ToEntity(),
      ArchivedMeter = model.ArchivedMeter.ToEntity(),
      ActiveEnergyTotalImportT1 = model.ActiveEnergyTotalImportT1.ToEntity(),
      ActiveEnergyTotalImportT2 = model.ActiveEnergyTotalImportT2.ToEntity(),
      ActivePowerTotalImportT1Peak = model.ActivePowerTotalImportT1Peak.ToEntity(),
      ReactiveEnergyTotalRampedT0 = model.ReactiveEnergyTotalRampedT0.ToEntity(),
      MeterFeePrice_EUR = model.MeterFeePrice_EUR,
    };
  }
}
