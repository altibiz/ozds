using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxAggregateModel : AggregateModel
{
  [Required]
  public required decimal VoltageL1AnyT0Avg_V { get; set; }

  [Required]
  public required decimal VoltageL2AnyT0Avg_V { get; set; }

  [Required]
  public required decimal VoltageL3AnyT0Avg_V { get; set; }

  [Required]
  public required decimal CurrentL1AnyT0Avg_A { get; set; }

  [Required]
  public required decimal CurrentL2AnyT0Avg_A { get; set; }

  [Required]
  public required decimal CurrentL3AnyT0Avg_A { get; set; }

  [Required]
  public required decimal ActivePowerL1NetT0Avg_W { get; set; }

  [Required]
  public required decimal ActivePowerL2NetT0Avg_W { get; set; }

  [Required]
  public required decimal ActivePowerL3NetT0Avg_W { get; set; }

  [Required]
  public required decimal ReactivePowerTotalNetT0Avg_VAR { get; set; }

  [Required]
  public required decimal ApparentPowerTotalNetT0Avg_VA { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT0Max_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalExportT0Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalExportT0Max_Wh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; }

  [Required]
  public required decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT1Max_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Min_Wh { get; set; }

  [Required]
  public required decimal ActiveEnergyTotalImportT2Max_Wh { get; set; }

  public override TariffMeasure<decimal> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(
            CurrentL1AnyT0Avg_A,
            CurrentL2AnyT0Avg_A,
            CurrentL3AnyT0Avg_A
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure<decimal>(
        new AnyDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(
            VoltageL1AnyT0Avg_V,
            VoltageL2AnyT0Avg_V,
            VoltageL3AnyT0Avg_V
          )
        )
      );
    }
  }

  public override TariffMeasure<decimal> ActivePower_W
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
        base.ActivePower_W,
        new UnaryTariffMeasure<decimal>(
          new NetDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(
              ActivePowerL1NetT0Avg_W,
              ActivePowerL2NetT0Avg_W,
              ActivePowerL3NetT0Avg_W
            )
          )
        )
      ]);
    }
  }

  public override TariffMeasure<decimal> ReactivePower_VAR
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
        base.ReactivePower_VAR,
        new UnaryTariffMeasure<decimal>(
          new NetDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ReactivePowerTotalNetT0Avg_VAR
            )
          )
        )
      ]);
    }
  }

  public override TariffMeasure<decimal> ApparentPower_VA
  {
    get
    {
      return new CompositeTariffMeasure<decimal>(
      [
        base.ApparentPower_VA,
        new UnaryTariffMeasure<decimal>(
          new NetDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ApparentPowerTotalNetT0Avg_VA
            )
          )
        )
      ]);
    }
  }

  public override SpanningMeasure<decimal> ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ActiveEnergyTotalImportT0Min_Wh),
            new SinglePhasicMeasureSum<decimal>(ActiveEnergyTotalExportT0Min_Wh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ActiveEnergyTotalImportT0Max_Wh),
            new SinglePhasicMeasureSum<decimal>(ActiveEnergyTotalExportT0Max_Wh)
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalImportT0Min_VARh),
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalExportT0Min_VARh)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalImportT0Max_VARh),
            new SinglePhasicMeasureSum<decimal>(
              ReactiveEnergyTotalExportT0Max_VARh)
          )
        )
      );
    }
  }

  public override SpanningMeasure<decimal> ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure<decimal>.Null; }
  }
}
