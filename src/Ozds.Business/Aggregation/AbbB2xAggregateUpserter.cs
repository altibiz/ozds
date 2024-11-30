using System.Linq.Expressions;
using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Aggregation;

public class AbbB2xAggregateUpserter : AggregateUpserter<AbbB2xAggregateModel,
  AbbB2xAggregateEntity>
{
  protected override
    Expression<Func<AbbB2xAggregateEntity, AbbB2xAggregateEntity,
      AbbB2xAggregateEntity>> UpsertConcreteEntity
  {
    get
    {
      return (lhs, rhs) => new AbbB2xAggregateEntity
      {
        MeterId = lhs.MeterId,
        MeasurementLocationId = lhs.MeasurementLocationId,
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
            + rhs.ActivePowerL1NetT0Avg_W *
            rhs.Count)
          / (lhs.Count + rhs.Count),
        ActivePowerL2NetT0Avg_W = (lhs.ActivePowerL2NetT0Avg_W * lhs.Count
            + rhs.ActivePowerL2NetT0Avg_W *
            rhs.Count)
          / (lhs.Count + rhs.Count),
        ActivePowerL3NetT0Avg_W = (lhs.ActivePowerL3NetT0Avg_W * lhs.Count
            + rhs.ActivePowerL3NetT0Avg_W *
            rhs.Count)
          / (lhs.Count + rhs.Count),
        ReactivePowerL1NetT0Avg_VAR =
          (lhs.ReactivePowerL1NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL1NetT0Avg_VAR * rhs.Count)
          / (lhs.Count + rhs.Count),
        ReactivePowerL2NetT0Avg_VAR =
          (lhs.ReactivePowerL2NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL2NetT0Avg_VAR * rhs.Count)
          / (lhs.Count + rhs.Count),
        ReactivePowerL3NetT0Avg_VAR =
          (lhs.ReactivePowerL3NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL3NetT0Avg_VAR * rhs.Count)
          / (lhs.Count + rhs.Count),
        ActiveEnergyTotalImportT0Min_Wh =
          lhs.ActiveEnergyTotalImportT0Min_Wh >
          rhs.ActiveEnergyTotalImportT0Min_Wh
            ? rhs.ActiveEnergyTotalImportT0Min_Wh
            : lhs.ActiveEnergyTotalImportT0Min_Wh,
        ActiveEnergyTotalImportT0Max_Wh =
          lhs.ActiveEnergyTotalImportT0Max_Wh <
          rhs.ActiveEnergyTotalImportT0Max_Wh
            ? rhs.ActiveEnergyTotalImportT0Max_Wh
            : lhs.ActiveEnergyTotalImportT0Max_Wh,
        ActiveEnergyTotalExportT0Min_Wh =
          lhs.ActiveEnergyTotalExportT0Min_Wh >
          rhs.ActiveEnergyTotalExportT0Min_Wh
            ? rhs.ActiveEnergyTotalExportT0Min_Wh
            : lhs.ActiveEnergyTotalExportT0Min_Wh,
        ActiveEnergyTotalExportT0Max_Wh =
          lhs.ActiveEnergyTotalExportT0Max_Wh <
          rhs.ActiveEnergyTotalExportT0Max_Wh
            ? rhs.ActiveEnergyTotalExportT0Max_Wh
            : lhs.ActiveEnergyTotalExportT0Max_Wh,
        ReactiveEnergyTotalImportT0Min_VARh =
          lhs.ReactiveEnergyTotalImportT0Min_VARh >
          rhs.ReactiveEnergyTotalImportT0Min_VARh
            ? rhs.ReactiveEnergyTotalImportT0Min_VARh
            : lhs.ReactiveEnergyTotalImportT0Min_VARh,
        ReactiveEnergyTotalImportT0Max_VARh =
          lhs.ReactiveEnergyTotalImportT0Max_VARh <
          rhs.ReactiveEnergyTotalImportT0Max_VARh
            ? rhs.ReactiveEnergyTotalImportT0Max_VARh
            : lhs.ReactiveEnergyTotalImportT0Max_VARh,
        ReactiveEnergyTotalExportT0Min_VARh =
          lhs.ReactiveEnergyTotalExportT0Min_VARh >
          rhs.ReactiveEnergyTotalExportT0Min_VARh
            ? rhs.ReactiveEnergyTotalExportT0Min_VARh
            : lhs.ReactiveEnergyTotalExportT0Min_VARh,
        ReactiveEnergyTotalExportT0Max_VARh =
          lhs.ReactiveEnergyTotalExportT0Max_VARh <
          rhs.ReactiveEnergyTotalExportT0Max_VARh
            ? rhs.ReactiveEnergyTotalExportT0Max_VARh
            : lhs.ReactiveEnergyTotalExportT0Max_VARh,
        ActiveEnergyTotalImportT1Min_Wh =
          lhs.ActiveEnergyTotalImportT1Min_Wh >
          rhs.ActiveEnergyTotalImportT1Min_Wh
            ? rhs.ActiveEnergyTotalImportT1Min_Wh
            : lhs.ActiveEnergyTotalImportT1Min_Wh,
        ActiveEnergyTotalImportT1Max_Wh =
          lhs.ActiveEnergyTotalImportT1Max_Wh <
          rhs.ActiveEnergyTotalImportT1Max_Wh
            ? rhs.ActiveEnergyTotalImportT1Max_Wh
            : lhs.ActiveEnergyTotalImportT1Max_Wh,
        ActiveEnergyTotalImportT2Min_Wh =
          lhs.ActiveEnergyTotalImportT2Min_Wh >
          rhs.ActiveEnergyTotalImportT2Min_Wh
            ? rhs.ActiveEnergyTotalImportT2Min_Wh
            : lhs.ActiveEnergyTotalImportT2Min_Wh,
        ActiveEnergyTotalImportT2Max_Wh =
          lhs.ActiveEnergyTotalImportT2Max_Wh <
          rhs.ActiveEnergyTotalImportT2Max_Wh
            ? rhs.ActiveEnergyTotalImportT2Max_Wh
            : lhs.ActiveEnergyTotalImportT2Max_Wh
      };
    }
  }

  protected override AbbB2xAggregateModel UpsertConcreteModel(
    AbbB2xAggregateModel lhs,
    AbbB2xAggregateModel rhs)
  {
    return new AbbB2xAggregateModel
    {
      MeterId = lhs.MeterId,
      MeasurementLocationId = lhs.MeasurementLocationId,
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
          + rhs.ActivePowerL1NetT0Avg_W *
          rhs.Count)
        / (lhs.Count + rhs.Count),
      ActivePowerL2NetT0Avg_W = (lhs.ActivePowerL2NetT0Avg_W * lhs.Count
          + rhs.ActivePowerL2NetT0Avg_W *
          rhs.Count)
        / (lhs.Count + rhs.Count),
      ActivePowerL3NetT0Avg_W = (lhs.ActivePowerL3NetT0Avg_W * lhs.Count
          + rhs.ActivePowerL3NetT0Avg_W *
          rhs.Count)
        / (lhs.Count + rhs.Count),
      ReactivePowerL1NetT0Avg_VAR =
        (lhs.ReactivePowerL1NetT0Avg_VAR * lhs.Count
          + rhs.ReactivePowerL1NetT0Avg_VAR * rhs.Count)
        / (lhs.Count + rhs.Count),
      ReactivePowerL2NetT0Avg_VAR =
        (lhs.ReactivePowerL2NetT0Avg_VAR * lhs.Count
          + rhs.ReactivePowerL2NetT0Avg_VAR * rhs.Count)
        / (lhs.Count + rhs.Count),
      ReactivePowerL3NetT0Avg_VAR =
        (lhs.ReactivePowerL3NetT0Avg_VAR * lhs.Count
          + rhs.ReactivePowerL3NetT0Avg_VAR * rhs.Count)
        / (lhs.Count + rhs.Count),
      ActiveEnergyTotalImportT0Min_Wh =
        lhs.ActiveEnergyTotalImportT0Min_Wh >
        rhs.ActiveEnergyTotalImportT0Min_Wh
          ? rhs.ActiveEnergyTotalImportT0Min_Wh
          : lhs.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh =
        lhs.ActiveEnergyTotalImportT0Max_Wh <
        rhs.ActiveEnergyTotalImportT0Max_Wh
          ? rhs.ActiveEnergyTotalImportT0Max_Wh
          : lhs.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh =
        lhs.ActiveEnergyTotalExportT0Min_Wh >
        rhs.ActiveEnergyTotalExportT0Min_Wh
          ? rhs.ActiveEnergyTotalExportT0Min_Wh
          : lhs.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh =
        lhs.ActiveEnergyTotalExportT0Max_Wh <
        rhs.ActiveEnergyTotalExportT0Max_Wh
          ? rhs.ActiveEnergyTotalExportT0Max_Wh
          : lhs.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        lhs.ReactiveEnergyTotalImportT0Min_VARh >
        rhs.ReactiveEnergyTotalImportT0Min_VARh
          ? rhs.ReactiveEnergyTotalImportT0Min_VARh
          : lhs.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        lhs.ReactiveEnergyTotalImportT0Max_VARh <
        rhs.ReactiveEnergyTotalImportT0Max_VARh
          ? rhs.ReactiveEnergyTotalImportT0Max_VARh
          : lhs.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        lhs.ReactiveEnergyTotalExportT0Min_VARh >
        rhs.ReactiveEnergyTotalExportT0Min_VARh
          ? rhs.ReactiveEnergyTotalExportT0Min_VARh
          : lhs.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        lhs.ReactiveEnergyTotalExportT0Max_VARh <
        rhs.ReactiveEnergyTotalExportT0Max_VARh
          ? rhs.ReactiveEnergyTotalExportT0Max_VARh
          : lhs.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh =
        lhs.ActiveEnergyTotalImportT1Min_Wh >
        rhs.ActiveEnergyTotalImportT1Min_Wh
          ? rhs.ActiveEnergyTotalImportT1Min_Wh
          : lhs.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh =
        lhs.ActiveEnergyTotalImportT1Max_Wh <
        rhs.ActiveEnergyTotalImportT1Max_Wh
          ? rhs.ActiveEnergyTotalImportT1Max_Wh
          : lhs.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh =
        lhs.ActiveEnergyTotalImportT2Min_Wh >
        rhs.ActiveEnergyTotalImportT2Min_Wh
          ? rhs.ActiveEnergyTotalImportT2Min_Wh
          : lhs.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh =
        lhs.ActiveEnergyTotalImportT2Max_Wh <
        rhs.ActiveEnergyTotalImportT2Max_Wh
          ? rhs.ActiveEnergyTotalImportT2Max_Wh
          : lhs.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}
