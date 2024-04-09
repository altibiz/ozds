using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Models;

public class RedLowCalculationModel : CalculationModel
{
  [Required] public required decimal ActiveEnergyTotalImportT1Min_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT1Max_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT1Amount_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT1Total_EUR { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT2Min_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT2Max_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT2Amount_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT2Total_EUR { get; set; }
  [Required] public required decimal ActivePowerTotalImportT1Peak_W { get; set; }
  [Required] public required decimal ActivePowerTotalImportT1Amount_W { get; set; }
  [Required] public required decimal ActivePowerTotalImportT1Price_EUR { get; set; }
  [Required] public required decimal ActivePowerTotalImportT1Total_EUR { get; set; }
  [Required] public required decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalImportT0Amount_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalExportT0Amount_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalRampedT0Amount_VARh { get; set; }
  [Required] public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
  [Required] public required decimal ReactiveEnergyTotalRampedT0Total_EUR { get; set; }
  [Required] public required decimal MeterFeePrice_EUR { get; set; }

  public static RedLowCalculationModel Calculate(
    string title,
    IMeter meter,
    MeasurementLocationModel measurementLocation,
    RedLowCatalogueModel catalogue,
    string issuedById,
    decimal activeEnergyTotalImportT1Min_Wh,
    decimal activeEnergyTotalImportT1Max_Wh,
    decimal activeEnergyTotalImportT2Min_Wh,
    decimal activeEnergyTotalImportT2Max_Wh,
    decimal activePowerTotalImportT1Peak_W,
    decimal reactiveEnergyTotalImportT0Min_VARh,
    decimal reactiveEnergyTotalImportT0Max_VARh,
    decimal reactiveEnergyTotalExportT0Min_VARh,
    decimal reactiveEnergyTotalExportT0Max_VARh,
    decimal meterFeePrice_EUR
  )
  {
    var initial = new RedLowCalculationModel()
    {
      Id = default!,
      Title = title,
      MeterId = meter.Id,
      CatalogueId = catalogue.Id,
      MeasurementLocationId = measurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = issuedById,
      ArchivedMeter = meter,
      ArchivedMeasurementLocation = measurementLocation,
      ArchivedCatalogue = catalogue,
      ActiveEnergyTotalImportT1Min_Wh = activeEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = activeEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT1Amount_Wh = 0,
      ActiveEnergyTotalImportT1Price_EUR = catalogue.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT1Total_EUR = 0,
      ActiveEnergyTotalImportT2Min_Wh = activeEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = activeEnergyTotalImportT2Max_Wh,
      ActiveEnergyTotalImportT2Amount_Wh = 0,
      ActiveEnergyTotalImportT2Price_EUR = catalogue.ActiveEnergyTotalImportT2Price_EUR,
      ActiveEnergyTotalImportT2Total_EUR = 0,
      ActivePowerTotalImportT1Peak_W = activePowerTotalImportT1Peak_W,
      ActivePowerTotalImportT1Amount_W = 0,
      ActivePowerTotalImportT1Price_EUR = catalogue.ActivePowerTotalImportT1Price_EUR,
      ActivePowerTotalImportT1Total_EUR = 0,
      ReactiveEnergyTotalImportT0Min_VARh = reactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = reactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalImportT0Amount_VARh = 0,
      ReactiveEnergyTotalExportT0Min_VARh = reactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = reactiveEnergyTotalExportT0Max_VARh,
      ReactiveEnergyTotalExportT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Price_EUR = catalogue.ReactiveEnergyTotalRampedT0Price_EUR,
      ReactiveEnergyTotalRampedT0Total_EUR = 0,
      MeterFeePrice_EUR = meterFeePrice_EUR,
    };

    initial.ActiveEnergyTotalImportT1Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffBinary.T1.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT1Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR).TariffBinary.T1.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT2Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffBinary.T2.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT2Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR).TariffBinary.T2.DuplexImport.PhaseSum;
    initial.ActivePowerTotalImportT1Amount_W =
      initial.ActivePowerAmount_W.SpanPeak.TariffBinary.T1.DuplexImport.PhaseSum;
    initial.ActivePowerTotalImportT1Total_EUR =
      initial.ActivePowerAmount_W.SpanPeak.TariffBinary.T1.DuplexImport.PhaseSum;
    initial.ReactiveEnergyTotalImportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexImport.PhaseSum;
    initial.ReactiveEnergyTotalExportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexExport.PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Amount_VARh =
      initial.ReactiveEnergyRampedAmount_Wh.TariffUnary.DuplexAny.PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Total_EUR =
      (initial.ReactiveEnergyRampedAmount_Wh * initial.ReactiveEnergyPrice_EUR).TariffUnary.DuplexAny.PhaseSum;

    return initial;
  }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh =>
    new MinMaxSpanningMeasure<decimal>(
      new BinaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1Min_Wh),
          new NullPhasicMeasure<decimal>()
        ),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2Min_Wh),
          new NullPhasicMeasure<decimal>()
        )
      ),
      new BinaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2Max_Wh),
          new NullPhasicMeasure<decimal>()
        ),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2Max_Wh),
          new NullPhasicMeasure<decimal>()
        )
      )
    );

  public override TariffMeasure<decimal> ActiveEnergyPrice_EUR =>
    new BinaryTariffMeasure<decimal>(
      new ImportExportDuplexMeasure<decimal>(
        new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1Price_EUR),
        new NullPhasicMeasure<decimal>()
      ),
      new ImportExportDuplexMeasure<decimal>(
        new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2Price_EUR),
        new NullPhasicMeasure<decimal>()
      )
    );

  public override SpanningMeasure<decimal> ReactiveEnergyAmount_Wh =>
    new MinMaxSpanningMeasure<decimal>(
      new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalImportT0Min_VARh),
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalExportT0Min_VARh)
        )
      ),
      new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalImportT0Max_VARh),
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalExportT0Max_VARh)
        )
      )
    );

  public override TariffMeasure<decimal> ReactiveEnergyPrice_EUR =>
      new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalRampedT0Price_EUR)
        )
      );

  public override SpanningMeasure<decimal> ActivePowerAmount_W =>
    new PeakSpanningMeasure<decimal>(
      new BinaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActivePowerTotalImportT1Amount_W),
          new NullPhasicMeasure<decimal>()
        ),
        DuplexMeasure<decimal>.Null
      )
    );

  public override TariffMeasure<decimal> ActivePowerPrice_EUR =>
    new UnaryTariffMeasure<decimal>(
      new ImportExportDuplexMeasure<decimal>(
        new SinglePhasicMeasure<decimal>(ActivePowerTotalImportT1Price_EUR),
        new NullPhasicMeasure<decimal>()
      )
    );
}
