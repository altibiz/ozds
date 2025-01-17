using Ozds.Business.Aggregation.Base;
using Ozds.Business.Aggregation.Implementations.Complex;
using Ozds.Business.Models;

namespace Ozds.Business.Aggregation.Implementations;

public class AbbB2xAggregateUpserter : ConcreteAggregateUpserter<AbbB2xAggregateModel>
{
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
      ReactivePowerL1NetT0_VAR = lhs.ReactivePowerL1NetT0_VAR.Upsert(
        lhs.Count,
        rhs.ReactivePowerL1NetT0_VAR,
        rhs.Count
      ),
      ReactivePowerL2NetT0_VAR = lhs.ReactivePowerL2NetT0_VAR.Upsert(
        lhs.Count,
        rhs.ReactivePowerL2NetT0_VAR,
        rhs.Count
      ),
      ReactivePowerL3NetT0_VAR = lhs.ReactivePowerL3NetT0_VAR.Upsert(
        lhs.Count,
        rhs.ReactivePowerL3NetT0_VAR,
        rhs.Count
      ),
      ActiveEnergyL1ImportT0_Wh = lhs.ActiveEnergyL1ImportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL1ImportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL1ImportT0_W = lhs.DerivedActivePowerL1ImportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL1ImportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyL2ImportT0_Wh = lhs.ActiveEnergyL2ImportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL2ImportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL2ImportT0_W = lhs.DerivedActivePowerL2ImportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL2ImportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyL3ImportT0_Wh = lhs.ActiveEnergyL3ImportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL3ImportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL3ImportT0_W = lhs.DerivedActivePowerL3ImportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL3ImportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyL1ExportT0_Wh = lhs.ActiveEnergyL1ExportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL1ExportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL1ExportT0_W = lhs.DerivedActivePowerL1ExportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL1ExportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL1ExportT0_Wh,
          rhs.ActiveEnergyL1ExportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyL2ExportT0_Wh = lhs.ActiveEnergyL2ExportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL2ExportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL2ExportT0_W = lhs.DerivedActivePowerL2ExportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL2ExportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL2ExportT0_Wh,
          rhs.ActiveEnergyL2ExportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyL3ExportT0_Wh = lhs.ActiveEnergyL3ExportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyL3ExportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerL3ExportT0_W = lhs.DerivedActivePowerL3ExportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL3ExportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyL3ExportT0_Wh,
          rhs.ActiveEnergyL3ExportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL1ImportT0_VARh = lhs.ReactiveEnergyL1ImportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL1ImportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL1ImportT0_VAR = lhs.DerivedReactivePowerL1ImportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL1ImportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL1ImportT0_VARh,
          rhs.ReactiveEnergyL1ImportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL2ImportT0_VARh = lhs.ReactiveEnergyL2ImportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL2ImportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL2ImportT0_VAR = lhs.DerivedReactivePowerL2ImportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL2ImportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL2ImportT0_VARh,
          rhs.ReactiveEnergyL2ImportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL3ImportT0_VARh = lhs.ReactiveEnergyL3ImportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL3ImportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL3ImportT0_VAR = lhs.DerivedReactivePowerL3ImportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL3ImportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL3ImportT0_VARh,
          rhs.ReactiveEnergyL3ImportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL1ExportT0_VARh = lhs.ReactiveEnergyL1ExportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL1ExportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL1ExportT0_VAR = lhs.DerivedReactivePowerL1ExportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL1ExportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL1ExportT0_VARh,
          rhs.ReactiveEnergyL1ExportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL2ExportT0_VARh = lhs.ReactiveEnergyL2ExportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL2ExportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL2ExportT0_VAR = lhs.DerivedReactivePowerL2ExportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL2ExportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL2ExportT0_VARh,
          rhs.ReactiveEnergyL2ExportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyL3ExportT0_VARh = lhs.ReactiveEnergyL3ExportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyL3ExportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerL3ExportT0_VAR = lhs.DerivedReactivePowerL3ExportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL3ExportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyL3ExportT0_VARh,
          rhs.ReactiveEnergyL3ExportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyTotalImportT0_Wh = lhs.ActiveEnergyTotalImportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT0_W = lhs.DerivedActivePowerTotalImportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyTotalImportT0_Wh,
          rhs.ActiveEnergyTotalImportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyTotalExportT0_Wh = lhs.ActiveEnergyTotalExportT0_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalExportT0_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalExportT0_W = lhs.DerivedActivePowerTotalExportT0_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalExportT0_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyTotalExportT0_Wh,
          rhs.ActiveEnergyTotalExportT0_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyTotalImportT0_VARh = lhs.ReactiveEnergyTotalImportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyTotalImportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerTotalImportT0_VAR = lhs.DerivedReactivePowerTotalImportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalImportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyTotalImportT0_VARh,
          rhs.ReactiveEnergyTotalImportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ReactiveEnergyTotalExportT0_VARh = lhs.ReactiveEnergyTotalExportT0_VARh.Upsert(
        lhs.Count,
        rhs.ReactiveEnergyTotalExportT0_VARh,
        rhs.Count
      ),
      DerivedReactivePowerTotalExportT0_VAR = lhs.DerivedReactivePowerTotalExportT0_VAR
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalExportT0_VAR,
          rhs.QuarterHourCount,
          lhs.ReactiveEnergyTotalExportT0_VARh,
          rhs.ReactiveEnergyTotalExportT0_VARh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyTotalImportT1_Wh = lhs.ActiveEnergyTotalImportT1_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT1_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT1_W = lhs.DerivedActivePowerTotalImportT1_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT1_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyTotalImportT1_Wh,
          rhs.ActiveEnergyTotalImportT1_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
      ActiveEnergyTotalImportT2_Wh = lhs.ActiveEnergyTotalImportT2_Wh.Upsert(
        lhs.Count,
        rhs.ActiveEnergyTotalImportT2_Wh,
        rhs.Count
      ),
      DerivedActivePowerTotalImportT2_W = lhs.DerivedActivePowerTotalImportT2_W
        .UpsertDerivedPowerFromEnergy(
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT2_W,
          rhs.QuarterHourCount,
          lhs.ActiveEnergyTotalImportT2_Wh,
          rhs.ActiveEnergyTotalImportT2_Wh,
          lhs.Timestamp,
          lhs.Interval
        ),
    };
  }
}
