using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class AbbB2xAggregateModel : AggregateModel
{
  [Required]
  public required float VoltageL1AnyT0Avg_V { get; set; }

  [Required]
  public required float VoltageL2AnyT0Avg_V { get; set; }

  [Required]
  public required float VoltageL3AnyT0Avg_V { get; set; }

  [Required]
  public required float CurrentL1AnyT0Avg_A { get; set; }

  [Required]
  public required float CurrentL2AnyT0Avg_A { get; set; }

  [Required]
  public required float CurrentL3AnyT0Avg_A { get; set; }

  [Required]
  public required float ActivePowerL1NetT0Avg_W { get; set; }

  [Required]
  public required float ActivePowerL2NetT0Avg_W { get; set; }

  [Required]
  public required float ActivePowerL3NetT0Avg_W { get; set; }

  [Required]
  public required float ReactivePowerL1NetT0Avg_VAR { get; set; }

  [Required]
  public required float ReactivePowerL2NetT0Avg_VAR { get; set; }

  [Required]
  public required float ReactivePowerL3NetT0Avg_VAR { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT0Min_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT0Max_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalExportT0Min_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalExportT0Max_Wh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalImportT0Min_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalImportT0Max_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalExportT0Min_VARh { get; set; }

  [Required]
  public required float ReactiveEnergyTotalExportT0Max_VARh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT1Min_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT1Max_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT2Min_Wh { get; set; }

  [Required]
  public required float ActiveEnergyTotalImportT2Max_Wh { get; set; }

  public override TariffMeasure<float> Current_A
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new AnyDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            CurrentL1AnyT0Avg_A,
            CurrentL2AnyT0Avg_A,
            CurrentL3AnyT0Avg_A
          )
        )
      );
    }
  }

  public override TariffMeasure<float> Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure<float>(
        new AnyDuplexMeasure<float>(
          new TriPhasicMeasure<float>(
            VoltageL1AnyT0Avg_V,
            VoltageL2AnyT0Avg_V,
            VoltageL3AnyT0Avg_V
          )
        )
      );
    }
  }

  public override TariffMeasure<float> ActivePower_W
  {
    get
    {
      return new CompositeTariffMeasure<float>(
      [
        base.ActivePower_W,
        new UnaryTariffMeasure<float>(
          new NetDuplexMeasure<float>(
            new TriPhasicMeasure<float>(
              ActivePowerL1NetT0Avg_W,
              ActivePowerL2NetT0Avg_W,
              ActivePowerL3NetT0Avg_W
            )
          )
        )
      ]);
    }
  }

  public override TariffMeasure<float> ReactivePower_VAR
  {
    get
    {
      return new CompositeTariffMeasure<float>(
      [
        base.ReactivePower_VAR,
        new UnaryTariffMeasure<float>(
          new NetDuplexMeasure<float>(
            new TriPhasicMeasure<float>(
              ReactivePowerL1NetT0Avg_VAR,
              ReactivePowerL2NetT0Avg_VAR,
              ReactivePowerL3NetT0Avg_VAR
            )
          )
        )
      ]);
    }
  }

  public override SpanningMeasure<float> ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<float>(
        new CompositeTariffMeasure<float>(
        [
          new BinaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT1Min_Wh),
              new NullPhasicMeasure<float>()
            ),
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT2Min_Wh),
              new NullPhasicMeasure<float>()
            )
          ),

          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT0Min_Wh),
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalExportT0Min_Wh)
            )
          )
        ]),
        new CompositeTariffMeasure<float>(
        [
          new BinaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT1Max_Wh),
              new NullPhasicMeasure<float>()
            ),
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT2Max_Wh),
              new NullPhasicMeasure<float>()
            )
          ),

          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalImportT0Max_Wh),
              new SinglePhasicMeasureSum<float>(ActiveEnergyTotalExportT0Max_Wh)
            )
          )
        ])
      );
    }
  }

  public override SpanningMeasure<float> ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure<float>(
        new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasureSum<float>(ReactiveEnergyTotalImportT0Min_VARh),
            new SinglePhasicMeasureSum<float>(ReactiveEnergyTotalExportT0Min_VARh)
          )
        ),
        new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasureSum<float>(ReactiveEnergyTotalImportT0Max_VARh),
            new SinglePhasicMeasureSum<float>(ReactiveEnergyTotalExportT0Max_VARh)
          )
        )
      );
    }
  }

  public override SpanningMeasure<float> ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure<float>.Null; }
  }
}
