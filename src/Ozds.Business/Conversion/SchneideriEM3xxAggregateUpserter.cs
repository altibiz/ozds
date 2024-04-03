using System.Linq.Expressions;
using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxAggregateUpserter : AggregateUpserter<
  SchneideriEM3xxxAggregateModel, SchneideriEM3xxxAggregateEntity>
{
  protected override
    Expression<Func<SchneideriEM3xxxAggregateEntity,
      SchneideriEM3xxxAggregateEntity, SchneideriEM3xxxAggregateEntity>>
    UpsertConcreteEntity
  {
    get
    {
      return (lhs, rhs) => new SchneideriEM3xxxAggregateEntity
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
        ReactivePowerTotalNetT0Avg_VAR =
          (lhs.ReactivePowerTotalNetT0Avg_VAR * lhs.Count
           + rhs.ReactivePowerTotalNetT0Avg_VAR * rhs.Count)
          / (lhs.Count + rhs.Count),
        ApparentPowerTotalNetT0Avg_VA =
          (lhs.ApparentPowerTotalNetT0Avg_VA * lhs.Count
           + rhs.ApparentPowerTotalNetT0Avg_VA * rhs.Count)
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

  protected override SchneideriEM3xxxAggregateModel UpsertConcreteModel(
    SchneideriEM3xxxAggregateModel lhs, SchneideriEM3xxxAggregateModel rhs)
  {
    return new SchneideriEM3xxxAggregateModel
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
      ReactivePowerTotalNetT0Avg_VAR =
        (lhs.ReactivePowerTotalNetT0Avg_VAR * lhs.Count
         + rhs.ReactivePowerTotalNetT0Avg_VAR * rhs.Count)
        / (lhs.Count + rhs.Count),
      ApparentPowerTotalNetT0Avg_VA =
        (lhs.ApparentPowerTotalNetT0Avg_VA * lhs.Count
         + rhs.ApparentPowerTotalNetT0Avg_VA * rhs.Count)
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
