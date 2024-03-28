using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

using IUpsertAggregate = IUpsertAggregate<AbbB2xAggregateModel>;

public class AbbB2xAggregateModel : AggregateModel, IUpsertAggregate
{
  [Required]
  public required float VoltageL1AnyT0Avg_V { get; init; }
  [Required]
  public required float VoltageL2AnyT0Avg_V { get; init; }
  [Required]
  public required float VoltageL3AnyT0Avg_V { get; init; }
  [Required]
  public required float CurrentL1AnyT0Avg_A { get; init; }
  [Required]
  public required float CurrentL2AnyT0Avg_A { get; init; }
  [Required]
  public required float CurrentL3AnyT0Avg_A { get; init; }
  [Required]
  public required float ActivePowerL1NetT0Avg_W { get; init; }
  [Required]
  public required float ActivePowerL2NetT0Avg_W { get; init; }
  [Required]
  public required float ActivePowerL3NetT0Avg_W { get; init; }
  [Required]
  public required float ReactivePowerL1NetT0Avg_VAR { get; init; }
  [Required]
  public required float ReactivePowerL2NetT0Avg_VAR { get; init; }
  [Required]
  public required float ReactivePowerL3NetT0Avg_VAR { get; init; }
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

  private static readonly Lazy<IUpsertAggregate.UpsertExpressionHolder>
    _upsertExpression =
      new(() =>
        new IUpsertAggregate.UpsertExpressionHolder((lhs, rhs) =>
          new AbbB2xAggregateModel()
          {
            MeterId = lhs.MeterId,
            Timestamp = lhs.Timestamp,
            Interval = lhs.Interval,
            Count = lhs.Count + rhs.Count,
            VoltageL1AnyT0Avg_V = (lhs.VoltageL1AnyT0Avg_V * lhs.Count
             + rhs.VoltageL1AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
            VoltageL2AnyT0Avg_V = (lhs.VoltageL2AnyT0Avg_V * lhs.Count
             + rhs.VoltageL2AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
            VoltageL3AnyT0Avg_V = (lhs.VoltageL3AnyT0Avg_V * lhs.Count
             + rhs.VoltageL3AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
            CurrentL1AnyT0Avg_A = (lhs.CurrentL1AnyT0Avg_A * lhs.Count
             + rhs.CurrentL1AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
            CurrentL2AnyT0Avg_A = (lhs.CurrentL2AnyT0Avg_A * lhs.Count
             + rhs.CurrentL2AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
            CurrentL3AnyT0Avg_A = (lhs.CurrentL3AnyT0Avg_A * lhs.Count
             + rhs.CurrentL3AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
            ActivePowerL1NetT0Avg_W = (lhs.ActivePowerL1NetT0Avg_W * lhs.Count
             + rhs.ActivePowerL1NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
            ActivePowerL2NetT0Avg_W = (lhs.ActivePowerL2NetT0Avg_W * lhs.Count
             + rhs.ActivePowerL2NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
            ActivePowerL3NetT0Avg_W = (lhs.ActivePowerL3NetT0Avg_W * lhs.Count
             + rhs.ActivePowerL3NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
            ReactivePowerL1NetT0Avg_VAR = (lhs.ReactivePowerL1NetT0Avg_VAR * lhs.Count
             + rhs.ReactivePowerL1NetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
            ReactivePowerL2NetT0Avg_VAR = (lhs.ReactivePowerL2NetT0Avg_VAR * lhs.Count
             + rhs.ReactivePowerL2NetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
            ReactivePowerL3NetT0Avg_VAR = (lhs.ReactivePowerL3NetT0Avg_VAR * lhs.Count
             + rhs.ReactivePowerL3NetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
            ActiveEnergyTotalImportT0Min_Wh = lhs.ActiveEnergyTotalImportT0Min_Wh >
            rhs.ActiveEnergyTotalImportT0Min_Wh
              ? rhs.ActiveEnergyTotalImportT0Min_Wh
              : lhs.ActiveEnergyTotalImportT0Min_Wh,
            ActiveEnergyTotalImportT0Max_Wh = lhs.ActiveEnergyTotalImportT0Max_Wh <
            rhs.ActiveEnergyTotalImportT0Max_Wh
              ? rhs.ActiveEnergyTotalImportT0Max_Wh
              : lhs.ActiveEnergyTotalImportT0Max_Wh,
            ActiveEnergyTotalExportT0Min_Wh = lhs.ActiveEnergyTotalExportT0Min_Wh >
            rhs.ActiveEnergyTotalExportT0Min_Wh
              ? rhs.ActiveEnergyTotalExportT0Min_Wh
              : lhs.ActiveEnergyTotalExportT0Min_Wh,
            ActiveEnergyTotalExportT0Max_Wh = lhs.ActiveEnergyTotalExportT0Max_Wh <
            rhs.ActiveEnergyTotalExportT0Max_Wh
              ? rhs.ActiveEnergyTotalExportT0Max_Wh
              : lhs.ActiveEnergyTotalExportT0Max_Wh,
            ReactiveEnergyTotalImportT0Min_VARh = lhs.ReactiveEnergyTotalImportT0Min_VARh >
            rhs.ReactiveEnergyTotalImportT0Min_VARh
              ? rhs.ReactiveEnergyTotalImportT0Min_VARh
              : lhs.ReactiveEnergyTotalImportT0Min_VARh,
            ReactiveEnergyTotalImportT0Max_VARh = lhs.ReactiveEnergyTotalImportT0Max_VARh <
            rhs.ReactiveEnergyTotalImportT0Max_VARh
              ? rhs.ReactiveEnergyTotalImportT0Max_VARh
              : lhs.ReactiveEnergyTotalImportT0Max_VARh,
            ReactiveEnergyTotalExportT0Min_VARh = lhs.ReactiveEnergyTotalExportT0Min_VARh >
            rhs.ReactiveEnergyTotalExportT0Min_VARh
              ? rhs.ReactiveEnergyTotalExportT0Min_VARh
              : lhs.ReactiveEnergyTotalExportT0Min_VARh,
            ReactiveEnergyTotalExportT0Max_VARh = lhs.ReactiveEnergyTotalExportT0Max_VARh <
            rhs.ReactiveEnergyTotalExportT0Max_VARh
              ? rhs.ReactiveEnergyTotalExportT0Max_VARh
              : lhs.ReactiveEnergyTotalExportT0Max_VARh,
            ActiveEnergyTotalImportT1Min_Wh = lhs.ActiveEnergyTotalImportT1Min_Wh >
            rhs.ActiveEnergyTotalImportT1Min_Wh
              ? rhs.ActiveEnergyTotalImportT1Min_Wh
              : lhs.ActiveEnergyTotalImportT1Min_Wh,
            ActiveEnergyTotalImportT1Max_Wh = lhs.ActiveEnergyTotalImportT1Max_Wh <
            rhs.ActiveEnergyTotalImportT1Max_Wh
              ? rhs.ActiveEnergyTotalImportT1Max_Wh
              : lhs.ActiveEnergyTotalImportT1Max_Wh,
            ActiveEnergyTotalImportT2Min_Wh = lhs.ActiveEnergyTotalImportT2Min_Wh >
            rhs.ActiveEnergyTotalImportT2Min_Wh
              ? rhs.ActiveEnergyTotalImportT2Min_Wh
              : lhs.ActiveEnergyTotalImportT2Min_Wh,
            ActiveEnergyTotalImportT2Max_Wh = lhs.ActiveEnergyTotalImportT2Max_Wh <
            rhs.ActiveEnergyTotalImportT2Max_Wh
              ? rhs.ActiveEnergyTotalImportT2Max_Wh
              : lhs.ActiveEnergyTotalImportT2Max_Wh
          }
        )
      );

  private static readonly Lazy<IUpsertAggregate.UpsertHolder> _upsert =
    new(() =>
      new IUpsertAggregate.UpsertHolder(_upsertExpression.Value.Value
        .Compile()));

  static IUpsertAggregate.UpsertExpressionHolder IUpsertAggregate.
    UpsertExpression
  {
    get { return _upsertExpression.Value; }
  }

  static IUpsertAggregate.UpsertHolder IUpsertAggregate.Upsert
  {
    get { return _upsert.Value; }
  }

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
    return new AbbB2xAggregateModel()
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
      ReactiveEnergyTotalImportT0Min_VARh = entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh = entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = entity.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = entity.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}
