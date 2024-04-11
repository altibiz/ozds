using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class WhiteLowNetworkUserCalculationModel : NetworkUserCalculationModel
{
  [Required]
  public required decimal ActiveEnergyTotalImportT1Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Max_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Amount_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Total_EUR { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Max_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Amount_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Total_EUR { get; set; }

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

  public override string Kind => "White Low Voltage";

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
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
    }
  }

  public override TariffMeasure<decimal> ActiveEnergyPrice_EUR
  {
    get
    {
      return new BinaryTariffMeasure<decimal>(
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1Price_EUR),
          new NullPhasicMeasure<decimal>()
        ),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2Price_EUR),
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
