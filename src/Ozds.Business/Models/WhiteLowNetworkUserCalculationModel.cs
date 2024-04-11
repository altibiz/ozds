using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models;

public class WhiteLowNetworkUserCalculationModel : NetworkUserCalculationModel
{
  [Required]
  public required ActiveEnergyTotalImportT1CalculationItemModel
    ActiveEnergyTotalImportT1 { get; set; } = default!;

  [Required]
  public required ActiveEnergyTotalImportT2CalculationItemModel
    ActiveEnergyTotalImportT2 { get; set; } = default!;

  [Required]
  public required ReactiveEnergyTotalRampedT0CalculationItemModel
    ReactiveEnergyTotalRampedT0 { get; set; } = default!;

  [Required] public required decimal MeterFeePrice_EUR { get; set; }

  public override string Kind
  {
    get { return "White Low Voltage"; }
  }

  public override SpanningMeasure<decimal> ActiveEnergyAmount_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1.Min_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2.Min_Wh),
            new NullPhasicMeasure<decimal>()
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2.Max_Wh),
            new NullPhasicMeasure<decimal>()
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2.Max_Wh),
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
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT1.Price_EUR),
          new NullPhasicMeasure<decimal>()
        ),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(ActiveEnergyTotalImportT2.Price_EUR),
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
              ReactiveEnergyTotalRampedT0.ImportMin_VARh),
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalRampedT0.ExportMin_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalRampedT0.ImportMax_VARh),
            new SinglePhasicMeasure<decimal>(
              ReactiveEnergyTotalRampedT0.ExportMax_VARh)
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
          new SinglePhasicMeasure<decimal>(
            ReactiveEnergyTotalRampedT0.Price_EUR)
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
