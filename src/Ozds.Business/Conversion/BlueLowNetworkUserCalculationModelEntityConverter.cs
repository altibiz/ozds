using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class BlueLowNetworkUserCalculationModelEntityConverter : ModelEntityConverter<
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
      ToDate = entity.ToDate,
      FromDate = entity.FromDate,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      MeterId = entity.MeterId,
      MeasurementLocationId = entity.MeasurementLocationId,
      CatalogueId = entity.CatalogueId,
      NetworkUserInvoiceId = entity.NetworkUserInvoiceId,
      ArchivedMeasurementLocation =
        entity.ArchivedMeasurementLocation.ToModel(),
      ArchivedCatalogue = entity.ArchivedCatalogue.ToModel(),
      ArchivedMeter = entity.ArchivedMeter.ToModel(),
      ActiveEnergyTotalImportT0Min_Wh = entity.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = entity.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalImportT0Amount_Wh =
        entity.ActiveEnergyTotalImportT0Amount_Wh,
      ActiveEnergyTotalImportT0Price_EUR =
        entity.ActiveEnergyTotalImportT0Price_EUR,
      ActiveEnergyTotalImportT0Total_EUR =
        entity.ActiveEnergyTotalImportT0Total_EUR,
      ReactiveEnergyTotalImportT0Min_VARh =
        entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalImportT0Amount_VARh =
        entity.ReactiveEnergyTotalImportT0Amount_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        entity.ReactiveEnergyTotalExportT0Max_VARh,
      ReactiveEnergyTotalExportT0Amount_VARh =
        entity.ReactiveEnergyTotalExportT0Amount_VARh,
      ReactiveEnergyTotalRampedT0Amount_VARh =
        entity.ReactiveEnergyTotalRampedT0Amount_VARh,
      ReactiveEnergyTotalRampedT0Price_EUR =
        entity.ReactiveEnergyTotalRampedT0Price_EUR,
      ReactiveEnergyTotalRampedT0Total_EUR =
        entity.ReactiveEnergyTotalRampedT0Total_EUR,
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
      ToDate = model.ToDate,
      FromDate = model.FromDate,
      NetworkUserInvoiceId = model.NetworkUserInvoiceId,
      MeterId = model.MeterId,
      MeasurementLocationId = model.MeasurementLocationId,
      CatalogueId = model.CatalogueId,
      ArchivedMeasurementLocation =
        model.ArchivedMeasurementLocation.ToEntity(),
      ArchivedCatalogue = model.ArchivedCatalogue.ToEntity(),
      ArchivedMeter = model.ArchivedMeter.ToEntity(),
      ActiveEnergyTotalImportT0Min_Wh = model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalImportT0Amount_Wh =
        model.ActiveEnergyTotalImportT0Amount_Wh,
      ActiveEnergyTotalImportT0Price_EUR =
        model.ActiveEnergyTotalImportT0Price_EUR,
      ActiveEnergyTotalImportT0Total_EUR =
        model.ActiveEnergyTotalImportT0Total_EUR,
      ReactiveEnergyTotalImportT0Min_VARh =
        model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalImportT0Amount_VARh =
        model.ReactiveEnergyTotalImportT0Amount_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        model.ReactiveEnergyTotalExportT0Max_VARh,
      ReactiveEnergyTotalExportT0Amount_VARh =
        model.ReactiveEnergyTotalExportT0Amount_VARh,
      ReactiveEnergyTotalRampedT0Amount_VARh =
        model.ReactiveEnergyTotalRampedT0Amount_VARh,
      ReactiveEnergyTotalRampedT0Price_EUR =
        model.ReactiveEnergyTotalRampedT0Price_EUR,
      ReactiveEnergyTotalRampedT0Total_EUR =
        model.ReactiveEnergyTotalRampedT0Total_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
