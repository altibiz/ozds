using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class BlueLowNetworkUserCalculationModel : NetworkUserCalculationModel
{
  [Required]
  public required decimal ActiveEnergyTotalImportT0Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Max_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Amount_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Total_EUR { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0Amount_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0Amount_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalRampedT0Amount_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalRampedT0Total_EUR { get; set; }

  [Required] public required decimal MeterFeePrice_EUR { get; set; }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
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
    }
  }

  public override TariffMeasure<decimal> ActiveEnergyPrice_EUR
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT0Price_EUR),
          new NullPhasicMeasure<decimal>()
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ReactiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalImportT0Min_VARh),
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalExportT0Min_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalImportT0Max_VARh),
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalExportT0Max_VARh)
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ReactiveEnergyPrice_EUR
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ReactiveEnergyTotalRampedT0Price_EUR)
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ActivePowerAmount_W
  {
    get { return SpanningMeasure<decimal>.Null; }
  }

  public override TariffMeasure<decimal> ActivePowerPrice_EUR
  {
    get { return TariffMeasure<decimal>.Null; }
  }
}
