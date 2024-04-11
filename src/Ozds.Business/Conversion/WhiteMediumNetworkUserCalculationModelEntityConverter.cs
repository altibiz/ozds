using Ozds.Business.Conversion.Base;
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
      NetworkUserInvoiceId = entity.NetworkUserInvoiceId,
      MeterId = entity.MeterId,
      MeasurementLocationId = entity.MeasurementLocationId,
      NetworkUserCatalogueId = entity.UsageNetworkUserCatalogueId,
      ArchivedMeasurementLocation =
        entity.ArchivedMeasurementLocation.ToModel(),
      ArchivedNetworkUserCatalogue = entity.ArchivedExpenditureNetworkUserCatalogue.ToModel(),
      ArchivedMeter = entity.ArchivedMeter.ToModel(),
      ActiveEnergyTotalImportT1Min_Wh = entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT1Amount_Wh =
        entity.ActiveEnergyTotalImportT1Amount_Wh,
      ActiveEnergyTotalImportT1Price_EUR =
        entity.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT1Total_EUR =
        entity.ActiveEnergyTotalImportT1Total_EUR,
      ActiveEnergyTotalImportT2Min_Wh = entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = entity.ActiveEnergyTotalImportT2Max_Wh,
      ActiveEnergyTotalImportT2Amount_Wh =
        entity.ActiveEnergyTotalImportT2Amount_Wh,
      ActiveEnergyTotalImportT2Price_EUR =
        entity.ActiveEnergyTotalImportT2Price_EUR,
      ActiveEnergyTotalImportT2Total_EUR =
        entity.ActiveEnergyTotalImportT2Total_EUR,
      ActivePowerTotalImportT1Peak_W = entity.ActivePowerTotalImportT1Peak_W,
      ActivePowerTotalImportT1Amount_W =
        entity.ActivePowerTotalImportT1Amount_W,
      ActivePowerTotalImportT1Price_EUR =
        entity.ActivePowerTotalImportT1Price_EUR,
      ActivePowerTotalImportT1Total_EUR =
        entity.ActivePowerTotalImportT1Total_EUR,
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
      NetworkUserInvoiceId = model.NetworkUserInvoiceId,
      MeterId = model.MeterId,
      MeasurementLocationId = model.MeasurementLocationId,
      UsageNetworkUserCatalogueId = model.NetworkUserCatalogueId,
      ArchivedMeasurementLocation =
        model.ArchivedMeasurementLocation.ToEntity(),
      ArchivedExpenditureNetworkUserCatalogue = model.ArchivedNetworkUserCatalogue.ToEntity(),
      ArchivedMeter = model.ArchivedMeter.ToEntity(),
      ActiveEnergyTotalImportT1Min_Wh = model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT1Amount_Wh =
        model.ActiveEnergyTotalImportT1Amount_Wh,
      ActiveEnergyTotalImportT1Price_EUR =
        model.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT1Total_EUR =
        model.ActiveEnergyTotalImportT1Total_EUR,
      ActiveEnergyTotalImportT2Min_Wh = model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = model.ActiveEnergyTotalImportT2Max_Wh,
      ActiveEnergyTotalImportT2Amount_Wh =
        model.ActiveEnergyTotalImportT2Amount_Wh,
      ActiveEnergyTotalImportT2Price_EUR =
        model.ActiveEnergyTotalImportT2Price_EUR,
      ActiveEnergyTotalImportT2Total_EUR =
        model.ActiveEnergyTotalImportT2Total_EUR,
      ActivePowerTotalImportT1Peak_W = model.ActivePowerTotalImportT1Peak_W,
      ActivePowerTotalImportT1Amount_W = model.ActivePowerTotalImportT1Amount_W,
      ActivePowerTotalImportT1Price_EUR =
        model.ActivePowerTotalImportT1Price_EUR,
      ActivePowerTotalImportT1Total_EUR =
        model.ActivePowerTotalImportT1Total_EUR,
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
