using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxMeasurementAggregateConverter :
  ConcreteMeasurementAggregateConverter<SchneideriEM3xxxMeasurementModel,
    SchneideriEM3xxxAggregateModel>
{
  protected override SchneideriEM3xxxAggregateModel ToAggregate(
    SchneideriEM3xxxMeasurementModel measurement,
    IntervalModel interval)
  {
    return measurement.ToAggregate(interval);
  }
}

public static class SchneideriEM3xxxMeasurementAggregateConverterExtensions
{
  public static SchneideriEM3xxxAggregateModel ToAggregate(
    this SchneideriEM3xxxMeasurementModel measurement,
    IntervalModel interval)
  {
    return new SchneideriEM3xxxAggregateModel
    {
      MeterId = measurement.MeterId,
      MeasurementLocationId = measurement.MeasurementLocationId,
      Timestamp = measurement.Timestamp,
      Interval = interval,
      Count = 1,
      QuarterHourCount = interval is IntervalModel.QuarterHour ? 1 : 0,
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
      ReactivePowerTotalNetT0_VAR = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ReactivePowerTotalNetT0_VAR,
        Min = measurement.ReactivePowerTotalNetT0_VAR,
        Max = measurement.ReactivePowerTotalNetT0_VAR,
        MinTimestamp = measurement.Timestamp,
        MaxTimestamp = measurement.Timestamp
      },
      ApparentPowerTotalNetT0_VA = new InstantaneousAggregateMeasureModel
      {
        Avg = measurement.ApparentPowerTotalNetT0_VA,
        Min = measurement.ApparentPowerTotalNetT0_VA,
        Max = measurement.ApparentPowerTotalNetT0_VA,
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
        Avg = measurement.ActiveEnergyL1ImportT0_Wh,
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
        Avg = measurement.ActiveEnergyL2ImportT0_Wh,
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
        Avg = measurement.ActiveEnergyL3ImportT0_Wh,
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
        Avg = measurement.ActiveEnergyTotalImportT0_Wh,
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
