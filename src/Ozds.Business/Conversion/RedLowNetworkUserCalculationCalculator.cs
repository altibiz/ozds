using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion;

public class
  RedLowNetworkUserCalculationCalculator : NetworkUserCalculationCalculator<RedLowCatalogueModel>
{
  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    RedLowCatalogueModel catalogue,
    NetworkUserNetworkUserCalculationBasisModel calculationBasis
  )
  {
    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var initial = new RedLowNetworkUserCalculationModel
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
      ActiveEnergyTotalImportT1Min_Wh = (decimal)calculationBasis.Aggregates
        .FirstOrDefault()!.ActiveEnergy_Wh.TariffBinary.T1.DuplexImport
        .PhaseSum,
      ActiveEnergyTotalImportT1Max_Wh = (decimal)calculationBasis.Aggregates
        .LastOrDefault()!.ActiveEnergy_Wh.TariffBinary.T2.DuplexImport.PhaseSum,
      ActiveEnergyTotalImportT1Amount_Wh = 0,
      ActiveEnergyTotalImportT1Price_EUR =
        catalogue.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT1Total_EUR = 0,
      ActiveEnergyTotalImportT2Min_Wh = (decimal)calculationBasis.Aggregates
        .FirstOrDefault()!.ActiveEnergy_Wh.TariffBinary.T2.DuplexImport
        .PhaseSum,
      ActiveEnergyTotalImportT2Max_Wh = (decimal)calculationBasis.Aggregates
        .LastOrDefault()!.ActiveEnergy_Wh.TariffBinary.T2.DuplexImport.PhaseSum,
      ActiveEnergyTotalImportT2Amount_Wh = 0,
      ActiveEnergyTotalImportT2Price_EUR =
        catalogue.ActiveEnergyTotalImportT2Price_EUR,
      ActiveEnergyTotalImportT2Total_EUR = 0,
      ActivePowerTotalImportT1Peak_W = (decimal)aggregates
        .Select(aggregate =>
          aggregate.ActivePower_W.TariffBinary.T1.DuplexImport.PhaseSum)
        .Order()
        .FirstOrDefault(),
      ActivePowerTotalImportT1Amount_W = 0,
      ActivePowerTotalImportT1Price_EUR =
        catalogue.ActivePowerTotalImportT1Price_EUR,
      ActivePowerTotalImportT1Total_EUR = 0,
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

    initial.ActiveEnergyTotalImportT1Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffBinary.T1.DuplexImport
        .PhaseSum;
    initial.ActiveEnergyTotalImportT1Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR)
      .TariffBinary.T1.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT2Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffBinary.T2.DuplexImport
        .PhaseSum;
    initial.ActiveEnergyTotalImportT2Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR)
      .TariffBinary.T2.DuplexImport.PhaseSum;
    initial.ActivePowerTotalImportT1Amount_W =
      initial.ActivePowerAmount_W.SpanPeak.TariffBinary.T1.DuplexImport
        .PhaseSum;
    initial.ActivePowerTotalImportT1Total_EUR =
      initial.ActivePowerAmount_W.SpanPeak.TariffBinary.T1.DuplexImport
        .PhaseSum;
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
