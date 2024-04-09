using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion;

public class
  BlueLowNetworkUserCalculationCalculator : NetworkUserCalculationCalculator<
  BlueLowCatalogueModel>
{
  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    BlueLowCatalogueModel catalogue,
    NetworkUserNetworkUserCalculationBasisModel calculationBasis
  )
  {
    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var initial = new BlueLowNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"${catalogue.Title} calculation for {calculationBasis.NetworkUser.Title} at {calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.ToDate,
      FromDate = calculationBasis.FromDate,
      NetworkUserInvoiceId = calculationBasis.NetworkUser.Id,
      CatalogueId = catalogue.Id,
      MeasurementLocationId = calculationBasis.MeasurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = default!,
      ArchivedMeter = calculationBasis.Meter,
      ArchivedMeasurementLocation = calculationBasis.MeasurementLocation,
      ArchivedCatalogue = catalogue,
      ActiveEnergyTotalImportT0Min_Wh = (decimal)calculationBasis.Aggregates
        .FirstOrDefault()!.ActiveEnergy_Wh.TariffUnary.DuplexImport.PhaseSum,
      ActiveEnergyTotalImportT0Max_Wh = (decimal)calculationBasis.Aggregates
        .LastOrDefault()!.ActiveEnergy_Wh.TariffUnary.DuplexImport.PhaseSum,
      ActiveEnergyTotalImportT0Amount_Wh = 0,
      ActiveEnergyTotalImportT0Price_EUR =
        catalogue.ActiveEnergyTotalImportT0Price_EUR,
      ActiveEnergyTotalImportT0Total_EUR = 0,
      ReactiveEnergyTotalImportT0Min_VARh = (decimal)calculationBasis.Aggregates
        .FirstOrDefault()!.ReactiveEnergy_VARh.TariffUnary.DuplexImport
        .PhaseSum,
      ReactiveEnergyTotalImportT0Max_VARh = (decimal)calculationBasis.Aggregates
        .LastOrDefault()!.ReactiveEnergy_VARh.TariffUnary.DuplexImport.PhaseSum,
      ReactiveEnergyTotalImportT0Amount_VARh = 0,
      ReactiveEnergyTotalExportT0Min_VARh = (decimal)calculationBasis.Aggregates
        .FirstOrDefault()!.ReactiveEnergy_VARh.TariffUnary.DuplexExport
        .PhaseSum,
      ReactiveEnergyTotalExportT0Max_VARh = (decimal)calculationBasis.Aggregates
        .LastOrDefault()!.ReactiveEnergy_VARh.TariffUnary.DuplexExport.PhaseSum,
      ReactiveEnergyTotalExportT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Price_EUR =
        catalogue.ReactiveEnergyTotalRampedT0Price_EUR,
      ReactiveEnergyTotalRampedT0Total_EUR = 0,
      MeterFeePrice_EUR = catalogue.MeterFeePrice_EUR
    };

    initial.ActiveEnergyTotalImportT0Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT0Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR)
      .TariffUnary.DuplexImport.PhaseSum;
    initial.ReactiveEnergyTotalImportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexImport
        .PhaseSum;
    initial.ReactiveEnergyTotalExportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexExport
        .PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Amount_VARh =
      initial.ReactiveEnergyRampedAmount_Wh.TariffUnary.DuplexAny.PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Total_EUR =
      (initial.ReactiveEnergyRampedAmount_Wh * initial.ReactiveEnergyPrice_EUR)
      .TariffUnary.DuplexAny.PhaseSum;

    return initial;
  }
}
