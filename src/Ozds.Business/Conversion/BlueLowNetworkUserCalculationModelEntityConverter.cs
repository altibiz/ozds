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
      ActiveEnergyTotalImportT0 = entity.ActiveEnergyTotalImportT0.ToModel(),
      ReactiveEnergyTotalRampedT0 =
        entity.ReactiveEnergyTotalRampedT0.ToModel(),
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
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
      ActiveEnergyTotalImportT0 = model.ActiveEnergyTotalImportT0.ToEntity(),
      ReactiveEnergyTotalRampedT0 =
        model.ReactiveEnergyTotalRampedT0.ToEntity(),
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
