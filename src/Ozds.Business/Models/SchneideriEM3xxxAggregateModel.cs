using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxAggregateModel : AggregateModel
{
  [Required] public required float VoltageL1AnyT0Avg_V { get; init; }

  [Required] public required float VoltageL2AnyT0Avg_V { get; init; }

  [Required] public required float VoltageL3AnyT0Avg_V { get; init; }

  [Required] public required float CurrentL1AnyT0Avg_A { get; init; }

  [Required] public required float CurrentL2AnyT0Avg_A { get; init; }

  [Required] public required float CurrentL3AnyT0Avg_A { get; init; }

  [Required] public required float ActivePowerL1NetT0Avg_W { get; init; }

  [Required] public required float ActivePowerL2NetT0Avg_W { get; init; }

  [Required] public required float ActivePowerL3NetT0Avg_W { get; init; }

  [Required] public required float ReactivePowerTotalNetT0Avg_VAR { get; init; }

  [Required] public required float ApparentPowerTotalNetT0Avg_VA { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT0Min_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT0Max_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalExportT0Min_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalExportT0Max_Wh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalImportT0Min_VARh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalImportT0Max_VARh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalExportT0Min_VARh { get; init; }

  [Required]
  public required float ReactiveEnergyTotalExportT0Max_VARh { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT1Min_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT1Max_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT2Min_Wh { get; init; }

  [Required]
  public required float ActiveEnergyTotalImportT2Max_Wh { get; init; }

  public override TariffMeasure Current_A
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            CurrentL1AnyT0Avg_A,
            CurrentL2AnyT0Avg_A,
            CurrentL3AnyT0Avg_A
          )
        )
      );
    }
  }

  public override TariffMeasure Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            VoltageL1AnyT0Avg_V,
            VoltageL2AnyT0Avg_V,
            VoltageL3AnyT0Avg_V
          )
        )
      );
    }
  }

  public override TariffMeasure ActivePower_W
  {
    get
    {
      return new CompositeTariffMeasure(new List<TariffMeasure>
      {
        base.ActivePower_W,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new TriPhasicMeasure(
              ActivePowerL1NetT0Avg_W,
              ActivePowerL2NetT0Avg_W,
              ActivePowerL3NetT0Avg_W
            )
          )
        )
      });
    }
  }

  public override TariffMeasure ReactivePower_VAR
  {
    get
    {
      return new CompositeTariffMeasure(new List<TariffMeasure>
      {
        base.ReactivePower_VAR,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new SinglePhasicMeasure(
              ReactivePowerTotalNetT0Avg_VAR
            )
          )
        )
      });
    }
  }

  public override TariffMeasure ApparentPower_VA
  {
    get
    {
      return new CompositeTariffMeasure(new List<TariffMeasure>
      {
        base.ApparentPower_VA,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new SinglePhasicMeasure(
              ApparentPowerTotalNetT0Avg_VA
            )
          )
        )
      });
    }
  }

  public override SpanningMeasure ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure(
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyTotalImportT0Min_Wh),
            new SinglePhasicMeasure(ActiveEnergyTotalExportT0Min_Wh)
          )
        ),
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyTotalImportT0Max_Wh),
            new SinglePhasicMeasure(ActiveEnergyTotalExportT0Max_Wh)
          )
        )
      );
    }
  }

  public override SpanningMeasure ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure(
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ReactiveEnergyTotalImportT0Min_VARh),
            new SinglePhasicMeasure(ReactiveEnergyTotalExportT0Min_VARh)
          )
        ),
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ReactiveEnergyTotalImportT0Max_VARh),
            new SinglePhasicMeasure(ReactiveEnergyTotalExportT0Max_VARh)
          )
        )
      );
    }
  }

  public override SpanningMeasure ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure.Null; }
  }
}
