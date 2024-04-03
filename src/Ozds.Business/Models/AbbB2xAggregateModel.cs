using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class AbbB2xAggregateModel : AggregateModel
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

  [Required] public required float ReactivePowerL1NetT0Avg_VAR { get; init; }

  [Required] public required float ReactivePowerL2NetT0Avg_VAR { get; init; }

  [Required] public required float ReactivePowerL3NetT0Avg_VAR { get; init; }

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
            new TriPhasicMeasure(
              ReactivePowerL1NetT0Avg_VAR,
              ReactivePowerL2NetT0Avg_VAR,
              ReactivePowerL3NetT0Avg_VAR
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

public static class AbbB2xAggregateModelExtensions
{
  public static AbbB2xAggregateEntity ToEntity(this AbbB2xAggregateModel model)
  {
    return new AbbB2xAggregateEntity
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      Interval = model.Interval.ToEntity(),
      Count = model.Count,
      VoltageL1AnyT0Avg_V = model.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = model.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = model.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = model.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = model.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = model.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = model.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = model.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = model.ActivePowerL3NetT0Avg_W,
      ReactivePowerL1NetT0Avg_VAR = model.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = model.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = model.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = model.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = model.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        model.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = model.ActiveEnergyTotalImportT2Max_Wh
    };
  }

  public static AbbB2xAggregateModel ToModel(this AbbB2xAggregateEntity entity)
  {
    return new AbbB2xAggregateModel
    {
      MeterId = entity.MeterId,
      Timestamp = entity.Timestamp,
      Interval = entity.Interval.ToModel(),
      Count = entity.Count,
      VoltageL1AnyT0Avg_V = entity.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = entity.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = entity.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = entity.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = entity.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = entity.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = entity.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = entity.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = entity.ActivePowerL3NetT0Avg_W,
      ReactivePowerL1NetT0Avg_VAR = entity.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = entity.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = entity.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = entity.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = entity.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = entity.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = entity.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        entity.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = entity.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}
