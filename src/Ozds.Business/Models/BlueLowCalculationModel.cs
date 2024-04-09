using System.ComponentModel.DataAnnotations;
using Ozds.Business.Conversion;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Models;

public class BlueLowCalculationModel : CalculationModel
{
  [Required] public required decimal ActiveEnergyTotalImportT0Min_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT0Max_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT0Amount_Wh { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }
  [Required] public required decimal ActiveEnergyTotalImportT0Total_EUR { get; set; }
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

  public static BlueLowCalculationModel Calculate(
    MeterModel meter,
    MeasurementLocationModel measurementLocation,
    CatalogueModel catalogue,
    string issuedById,
    decimal activeEnergyTotalImportT0Min_Wh,
    decimal activeEnergyTotalImportT0Max_Wh,
    decimal activeEnergyTotalImportT0Price_EUR,
    decimal reactiveEnergyTotalImportT0Min_VARh,
    decimal reactiveEnergyTotalImportT0Max_VARh,
    decimal reactiveEnergyTotalExportT0Min_VARh,
    decimal reactiveEnergyTotalExportT0Max_VARh,
    decimal reactiveEnergyTotalRampedT0Price_EUR,
    decimal meterFeePrice_EUR
  )
  {
    var initial = new BlueLowCalculationModel()
    {
      Id = default!,
      Title = $"${catalogue.Title} calculation for {meter.Title} at {measurementLocation.Title}",
      MeterId = meter.Id,
      CatalogueId = catalogue.Id,
      MeasurementLocationId = measurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = issuedById,
      ArchivedMeter = meter,
      ArchivedMeasurementLocation = measurementLocation,
      ArchivedCatalogue = catalogue,
      ActiveEnergyTotalImportT0Min_Wh = activeEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = activeEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalImportT0Amount_Wh = 0,
      ActiveEnergyTotalImportT0Price_EUR = activeEnergyTotalImportT0Price_EUR,
      ActiveEnergyTotalImportT0Total_EUR = 0,
      ReactiveEnergyTotalImportT0Min_VARh = reactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = reactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalImportT0Amount_VARh = 0,
      ReactiveEnergyTotalExportT0Min_VARh = reactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = reactiveEnergyTotalExportT0Max_VARh,
      ReactiveEnergyTotalExportT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Amount_VARh = 0,
      ReactiveEnergyTotalRampedT0Price_EUR = reactiveEnergyTotalRampedT0Price_EUR,
      ReactiveEnergyTotalRampedT0Total_EUR = 0,
      MeterFeePrice_EUR = meterFeePrice_EUR,
    };

    initial.ActiveEnergyTotalImportT0Amount_Wh =
      initial.ActiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexImport.PhaseSum;
    initial.ActiveEnergyTotalImportT0Total_EUR =
      (initial.ActiveEnergyAmount_Wh.SpanDiff * initial.ActiveEnergyPrice_EUR)
        .TariffUnary.DuplexImport.PhaseSum;
    initial.ReactiveEnergyTotalImportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexImport.PhaseSum;
    initial.ReactiveEnergyTotalExportT0Amount_VARh =
      initial.ReactiveEnergyAmount_Wh.SpanDiff.TariffUnary.DuplexExport.PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Amount_VARh =
      initial.ReactiveEnergyRampedAmount_Wh.TariffUnary.DuplexAny.PhaseSum;
    initial.ReactiveEnergyTotalRampedT0Total_EUR =
      (initial.ReactiveEnergyRampedAmount_Wh * initial.ReactiveEnergyPrice_EUR)
        .TariffUnary.DuplexAny.PhaseSum;

    return initial;
  }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh =>
    new MinMaxSpanningMeasure<decimal>(
      new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0Min_Wh),
          new NullPhasicMeasure<decimal>()
        )
      ),
      new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0Max_Wh),
          new NullPhasicMeasure<decimal>()
        )
      )
    );

  public override TariffMeasure<decimal> ActiveEnergyPrice_EUR =>
    new UnaryTariffMeasure<decimal>(
      new ImportExportDuplexMeasure<decimal>(
        new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0Price_EUR),
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
    SpanningMeasure<decimal>.Null;

  public override TariffMeasure<decimal> ActivePowerPrice_EUR =>
    TariffMeasure<decimal>.Null;
}
