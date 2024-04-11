using Ozds.Business.Conversion.Base;
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

  public static WhiteLowNetworkUserCalculationEntity ToEntity(
    this WhiteLowNetworkUserCalculationModel model)
  {
    return new WhiteLowNetworkUserCalculationEntity
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
