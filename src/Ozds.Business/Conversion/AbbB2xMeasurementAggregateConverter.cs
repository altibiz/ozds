using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion;

public class AbbB2xMeasurementAggregateConverter : MeasurementAggregateConverter
  <AbbB2xMeasurementModel, AbbB2xAggregateModel>
{
  protected override AbbB2xAggregateModel ToAggregate(
    AbbB2xMeasurementModel measurement,
    IntervalModel interval)
  {
    return measurement.ToAggregate(interval);
  }
}

public static class AbbB2xMeasurementAggregateConverterExtensions
{
  public static AbbB2xAggregateModel ToAggregate(
    this AbbB2xMeasurementModel measurement,
    IntervalModel interval)
  {
    return new AbbB2xAggregateModel
    {
      MeterId = measurement.MeterId,
      MeasurementLocationId = measurement.MeasurementLocationId,
      Timestamp = measurement.Timestamp,
      Interval = interval,
      Count = 1,
      QuarterHourCount = 0,
      VoltageL1AnyT0_V = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.VoltageL1AnyT0_V,
        Min = measurement.VoltageL1AnyT0_V,
        Max = measurement.VoltageL1AnyT0_V,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      VoltageL2AnyT0_V = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.VoltageL2AnyT0_V,
        Min = measurement.VoltageL2AnyT0_V,
        Max = measurement.VoltageL2AnyT0_V,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      VoltageL3AnyT0_V = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.VoltageL3AnyT0_V,
        Min = measurement.VoltageL3AnyT0_V,
        Max = measurement.VoltageL3AnyT0_V,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      CurrentL1AnyT0_A = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.CurrentL1AnyT0_A,
        Min = measurement.CurrentL1AnyT0_A,
        Max = measurement.CurrentL1AnyT0_A,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      CurrentL2AnyT0_A = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.CurrentL2AnyT0_A,
        Min = measurement.CurrentL2AnyT0_A,
        Max = measurement.CurrentL2AnyT0_A,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      CurrentL3AnyT0_A = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.CurrentL3AnyT0_A,
        Min = measurement.CurrentL3AnyT0_A,
        Max = measurement.CurrentL3AnyT0_A,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActivePowerL1NetT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActivePowerL1NetT0_W,
        Min = measurement.ActivePowerL1NetT0_W,
        Max = measurement.ActivePowerL1NetT0_W,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActivePowerL2NetT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActivePowerL2NetT0_W,
        Min = measurement.ActivePowerL2NetT0_W,
        Max = measurement.ActivePowerL2NetT0_W,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActivePowerL3NetT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActivePowerL3NetT0_W,
        Min = measurement.ActivePowerL3NetT0_W,
        Max = measurement.ActivePowerL3NetT0_W,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactivePowerL1NetT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ReactivePowerL1NetT0_VAR,
        Min = measurement.ReactivePowerL1NetT0_VAR,
        Max = measurement.ReactivePowerL1NetT0_VAR,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactivePowerL2NetT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ReactivePowerL2NetT0_VAR,
        Min = measurement.ReactivePowerL2NetT0_VAR,
        Max = measurement.ReactivePowerL2NetT0_VAR,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactivePowerL3NetT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ReactivePowerL3NetT0_VAR,
        Min = measurement.ReactivePowerL3NetT0_VAR,
        Max = measurement.ReactivePowerL3NetT0_VAR,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL1ImportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL1ImportT0_Wh,
        Max = measurement.ActiveEnergyL1ImportT0_Wh
      },
      DerivedActivePowerL1ImportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL2ImportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL2ImportT0_Wh,
        Max = measurement.ActiveEnergyL2ImportT0_Wh
      },
      DerivedActivePowerL2ImportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL3ImportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL3ImportT0_Wh,
        Max = measurement.ActiveEnergyL3ImportT0_Wh
      },
      DerivedActivePowerL3ImportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL1ExportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL1ExportT0_Wh,
        Max = measurement.ActiveEnergyL1ExportT0_Wh
      },
      DerivedActivePowerL1ExportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL2ExportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL2ExportT0_Wh,
        Max = measurement.ActiveEnergyL2ExportT0_Wh
      },
      DerivedActivePowerL2ExportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyL3ExportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyL3ExportT0_Wh,
        Max = measurement.ActiveEnergyL3ExportT0_Wh
      },
      DerivedActivePowerL3ExportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL1ImportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL1ImportT0_VARh,
        Max = measurement.ReactiveEnergyL1ImportT0_VARh
      },
      DerivedReactivePowerL1ImportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL2ImportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL2ImportT0_VARh,
        Max = measurement.ReactiveEnergyL2ImportT0_VARh
      },
      DerivedReactivePowerL2ImportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL3ImportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL3ImportT0_VARh,
        Max = measurement.ReactiveEnergyL3ImportT0_VARh
      },
      DerivedReactivePowerL3ImportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL1ExportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL1ExportT0_VARh,
        Max = measurement.ReactiveEnergyL1ExportT0_VARh
      },
      DerivedReactivePowerL1ExportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL2ExportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL2ExportT0_VARh,
        Max = measurement.ReactiveEnergyL2ExportT0_VARh
      },
      DerivedReactivePowerL2ExportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyL3ExportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyL3ExportT0_VARh,
        Max = measurement.ReactiveEnergyL3ExportT0_VARh
      },
      DerivedReactivePowerL3ExportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyTotalImportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyTotalImportT0_Wh,
        Max = measurement.ActiveEnergyTotalImportT0_Wh
      },
      DerivedActivePowerTotalImportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyTotalExportT0_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyTotalExportT0_Wh,
        Max = measurement.ActiveEnergyTotalExportT0_Wh
      },
      DerivedActivePowerTotalExportT0_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyTotalImportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyTotalImportT0_VARh,
        Max = measurement.ReactiveEnergyTotalImportT0_VARh
      },
      DerivedReactivePowerTotalImportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ReactiveEnergyTotalExportT0_VARh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyTotalExportT0_VARh,
        Max = measurement.ReactiveEnergyTotalExportT0_VARh
      },
      DerivedReactivePowerTotalExportT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyTotalImportT1_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyTotalImportT1_Wh,
        Max = measurement.ActiveEnergyTotalImportT1_Wh
      },
      DerivedActivePowerTotalImportT1_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ActiveEnergyTotalImportT2_Wh = new CumulativeAggregateMeasureModel
      {
        Min = measurement.ActiveEnergyTotalImportT2_Wh,
        Max = measurement.ActiveEnergyTotalImportT2_Wh
      },
      DerivedActivePowerTotalImportT2_W = new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
    };
  }
}
