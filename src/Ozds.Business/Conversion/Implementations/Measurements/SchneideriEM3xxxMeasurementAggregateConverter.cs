using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class SchneideriEM3xxxMeasurementAggregateConverter
  : ConcreteMeasurementAggregateConverter<
    SchneideriEM3xxxMeasurementModel,
    SchneideriEM3xxxAggregateModel>
{
  public override void Initialize(
    SchneideriEM3xxxAggregateModel aggregate,
    SchneideriEM3xxxMeasurementModel measurement,
    IntervalModel interval)
  {
    aggregate.MeterId = measurement.MeterId;
    aggregate.MeasurementLocationId = measurement.MeasurementLocationId;
    aggregate.Timestamp = measurement.Timestamp;
    aggregate.Interval = interval;
    aggregate.Count = 1;
    aggregate.QuarterHourCount = interval is IntervalModel.QuarterHour ? 1 : 0;
    aggregate.VoltageL1AnyT0_V = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.VoltageL1AnyT0_V,
      Min = measurement.VoltageL1AnyT0_V,
      Max = measurement.VoltageL1AnyT0_V,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.VoltageL2AnyT0_V = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.VoltageL2AnyT0_V,
      Min = measurement.VoltageL2AnyT0_V,
      Max = measurement.VoltageL2AnyT0_V,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.VoltageL3AnyT0_V = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.VoltageL3AnyT0_V,
      Min = measurement.VoltageL3AnyT0_V,
      Max = measurement.VoltageL3AnyT0_V,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.CurrentL1AnyT0_A = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.CurrentL1AnyT0_A,
      Min = measurement.CurrentL1AnyT0_A,
      Max = measurement.CurrentL1AnyT0_A,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.CurrentL2AnyT0_A = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.CurrentL2AnyT0_A,
      Min = measurement.CurrentL2AnyT0_A,
      Max = measurement.CurrentL2AnyT0_A,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.CurrentL3AnyT0_A = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.CurrentL3AnyT0_A,
      Min = measurement.CurrentL3AnyT0_A,
      Max = measurement.CurrentL3AnyT0_A,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.ActivePowerL1NetT0_W = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.ActivePowerL1NetT0_W,
      Min = measurement.ActivePowerL1NetT0_W,
      Max = measurement.ActivePowerL1NetT0_W,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.ActivePowerL2NetT0_W = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.ActivePowerL2NetT0_W,
      Min = measurement.ActivePowerL2NetT0_W,
      Max = measurement.ActivePowerL2NetT0_W,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.ActivePowerL3NetT0_W = new InstantaneousAggregateMeasureModel
    {
      Avg = measurement.ActivePowerL3NetT0_W,
      Min = measurement.ActivePowerL3NetT0_W,
      Max = measurement.ActivePowerL3NetT0_W,
      MinTimestamp = measurement.Timestamp,
      MaxTimestamp = measurement.Timestamp
    };
    aggregate.ReactivePowerTotalNetT0_VAR =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ReactivePowerTotalNetT0_VAR,
        Min = measurement.ReactivePowerTotalNetT0_VAR,
        Max = measurement.ReactivePowerTotalNetT0_VAR,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ApparentPowerTotalNetT0_VA =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ApparentPowerTotalNetT0_VA,
        Min = measurement.ApparentPowerTotalNetT0_VA,
        Max = measurement.ApparentPowerTotalNetT0_VA,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyL1ImportT0_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyL1ImportT0_Wh,
      Max = measurement.ActiveEnergyL1ImportT0_Wh
    };
    aggregate.DerivedActivePowerL1ImportT0_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActiveEnergyL1ImportT0_Wh,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyL2ImportT0_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyL2ImportT0_Wh,
      Max = measurement.ActiveEnergyL2ImportT0_Wh
    };
    aggregate.DerivedActivePowerL2ImportT0_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActiveEnergyL2ImportT0_Wh,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyL3ImportT0_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyL3ImportT0_Wh,
      Max = measurement.ActiveEnergyL3ImportT0_Wh
    };
    aggregate.DerivedActivePowerL3ImportT0_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActiveEnergyL3ImportT0_Wh,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyTotalImportT0_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyTotalImportT0_Wh,
      Max = measurement.ActiveEnergyTotalImportT0_Wh
    };
    aggregate.DerivedActivePowerTotalImportT0_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ActiveEnergyTotalImportT0_Wh,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyTotalExportT0_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyTotalExportT0_Wh,
      Max = measurement.ActiveEnergyTotalExportT0_Wh
    };
    aggregate.DerivedActivePowerTotalExportT0_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ReactiveEnergyTotalImportT0_VARh =
      new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyTotalImportT0_VARh,
        Max = measurement.ReactiveEnergyTotalImportT0_VARh
      };
    aggregate.DerivedReactivePowerTotalImportT0_VAR =
      new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ReactiveEnergyTotalExportT0_VARh =
      new CumulativeAggregateMeasureModel
      {
        Min = measurement.ReactiveEnergyTotalExportT0_VARh,
        Max = measurement.ReactiveEnergyTotalExportT0_VARh
      };
    aggregate.DerivedReactivePowerTotalExportT0_VAR =
      new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyTotalImportT1_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyTotalImportT1_Wh,
      Max = measurement.ActiveEnergyTotalImportT1_Wh
    };
    aggregate.DerivedActivePowerTotalImportT1_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
    aggregate.ActiveEnergyTotalImportT2_Wh = new CumulativeAggregateMeasureModel
    {
      Min = measurement.ActiveEnergyTotalImportT2_Wh,
      Max = measurement.ActiveEnergyTotalImportT2_Wh
    };
    aggregate.DerivedActivePowerTotalImportT2_W =
      new InstantaneousAggregateMeasureModel
      {
        Avg = 0M,
        Min = 0M,
        Max = 0M,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      };
  }
}
