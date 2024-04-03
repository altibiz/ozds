using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion;

public class AbbB2xMeasurementAggregateConverter : MeasurementAggregateConverter
  <AbbB2xMeasurementModel, AbbB2xAggregateModel>
{
  protected override AbbB2xAggregateModel ToAggregate(
    AbbB2xMeasurementModel measurement, IntervalModel interval)
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
      Timestamp = measurement.Timestamp,
      Interval = interval,
      Count = 1,
      VoltageL1AnyT0Avg_V = measurement.VoltageL1AnyT0_V,
      VoltageL2AnyT0Avg_V = measurement.VoltageL2AnyT0_V,
      VoltageL3AnyT0Avg_V = measurement.VoltageL3AnyT0_V,
      CurrentL1AnyT0Avg_A = measurement.CurrentL1AnyT0_A,
      CurrentL2AnyT0Avg_A = measurement.CurrentL2AnyT0_A,
      CurrentL3AnyT0Avg_A = measurement.CurrentL3AnyT0_A,
      ActivePowerL1NetT0Avg_W = measurement.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0Avg_W = measurement.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0Avg_W = measurement.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0Avg_VAR = measurement.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0Avg_VAR = measurement.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0Avg_VAR = measurement.ReactivePowerL3NetT0_VAR,
      ActiveEnergyTotalImportT0Min_Wh =
        measurement.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalImportT0Max_Wh =
        measurement.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0Min_Wh =
        measurement.ActiveEnergyTotalExportT0_Wh,
      ActiveEnergyTotalExportT0Max_Wh =
        measurement.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        measurement.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        measurement.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        measurement.ReactiveEnergyTotalExportT0_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        measurement.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1Min_Wh =
        measurement.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT1Max_Wh =
        measurement.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2Min_Wh =
        measurement.ActiveEnergyTotalImportT2_Wh,
      ActiveEnergyTotalImportT2Max_Wh = measurement.ActiveEnergyTotalImportT2_Wh
    };
  }
}
