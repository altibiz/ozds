using Ozds.Business.Aggregation.Base;
using Ozds.Business.Aggregation.Complex;
using Ozds.Business.Models;

namespace Ozds.Business.Aggregation;

public class SchneideriEM3xxxAggregateUpserter : AggregateUpserter<
  SchneideriEM3xxxAggregateModel>
{
  protected override SchneideriEM3xxxAggregateModel UpsertConcreteModel(
    SchneideriEM3xxxAggregateModel lhs,
    SchneideriEM3xxxAggregateModel rhs)
  {
    return new SchneideriEM3xxxAggregateModel
    {
      MeterId = lhs.MeterId,
      MeasurementLocationId = lhs.MeasurementLocationId,
      Timestamp = lhs.Timestamp,
      Interval = lhs.Interval,
      Count = lhs.Count + rhs.Count,
      QuarterHourCount = lhs.QuarterHourCount + rhs.QuarterHourCount,
      VoltageL1AnyT0_V = lhs.VoltageL1AnyT0_V.Upsert(
        lhs.Count,
        rhs.VoltageL1AnyT0_V,
        rhs.Count
      ),
      VoltageL2AnyT0_V = lhs.VoltageL2AnyT0_V.Upsert(
        lhs.Count,
        rhs.VoltageL2AnyT0_V,
        rhs.Count
      ),
      VoltageL3AnyT0_V = lhs.VoltageL3AnyT0_V.Upsert(
        lhs.Count,
        rhs.VoltageL3AnyT0_V,
        rhs.Count
      ),
      CurrentL1AnyT0_A = lhs.CurrentL1AnyT0_A.Upsert(
        lhs.Count,
        rhs.CurrentL1AnyT0_A,
        rhs.Count
      ),
      CurrentL2AnyT0_A = lhs.CurrentL2AnyT0_A.Upsert(
        lhs.Count,
        rhs.CurrentL2AnyT0_A,
        rhs.Count
      ),
      CurrentL3AnyT0_A = lhs.CurrentL3AnyT0_A.Upsert(
        lhs.Count,
        rhs.CurrentL3AnyT0_A,
        rhs.Count
      ),
      ActivePowerL1NetT0_W = lhs.ActivePowerL1NetT0_W.Upsert(
        lhs.Count,
        rhs.ActivePowerL1NetT0_W,
        rhs.Count
      ),
      ActivePowerL2NetT0_W = lhs.ActivePowerL2NetT0_W.Upsert(
        lhs.Count,
        rhs.ActivePowerL2NetT0_W,
        rhs.Count
      ),
      ActivePowerL3NetT0_W = lhs.ActivePowerL3NetT0_W.Upsert(
        lhs.Count,
        rhs.ActivePowerL3NetT0_W,
        rhs.Count
      ),
      ReactivePowerTotalNetT0_VAR = lhs.ReactivePowerTotalNetT0_VAR.Upsert(
        lhs.Count,
        rhs.ReactivePowerTotalNetT0_VAR,
        rhs.Count
      ),
      ApparentPowerTotalNetT0_VA = lhs.ApparentPowerTotalNetT0_VA.Upsert(
        lhs.Count,
        rhs.ApparentPowerTotalNetT0_VA,
        rhs.Count
      ),
      ActiveEnergyTotalImportT0_Wh = lhs.ActiveEnergyTotalImportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT0_W = lhs.DerivedActivePowerTotalImportT0_W.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedActivePowerTotalImportT0_W,
        rhs.QuarterHourCount
      ),
      ActiveEnergyTotalExportT0_Wh = lhs.ActiveEnergyTotalExportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalExportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalExportT0_W = lhs.DerivedActivePowerTotalExportT0_W.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedActivePowerTotalExportT0_W,
        rhs.QuarterHourCount
      ),
      ReactiveEnergyTotalImportT0_VARh = lhs.ReactiveEnergyTotalImportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyTotalImportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerTotalImportT0_VAR = lhs.DerivedReactivePowerTotalImportT0_VAR.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedReactivePowerTotalImportT0_VAR,
        rhs.QuarterHourCount
      ),
      ReactiveEnergyTotalExportT0_VARh = lhs.ReactiveEnergyTotalExportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyTotalExportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerTotalExportT0_VAR = lhs.DerivedReactivePowerTotalExportT0_VAR.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedReactivePowerTotalExportT0_VAR,
        rhs.QuarterHourCount
      ),
      ActiveEnergyTotalImportT1_Wh = lhs.ActiveEnergyTotalImportT1_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT1_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT1_W = lhs.DerivedActivePowerTotalImportT1_W.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedActivePowerTotalImportT1_W,
        rhs.QuarterHourCount
      ),
      ActiveEnergyTotalImportT2_Wh = lhs.ActiveEnergyTotalImportT2_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT2_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT2_W = lhs.DerivedActivePowerTotalImportT2_W.Upsert(
        lhs.QuarterHourCount,
        rhs.DerivedActivePowerTotalImportT2_W,
        rhs.QuarterHourCount
      )
    };
  }
}
