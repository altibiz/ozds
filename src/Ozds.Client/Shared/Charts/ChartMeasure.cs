using Ozds.Business.Math;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Shared.Charts;

public enum ChartMeasure
{
  Current,
  Voltage,
  ActivePower,
  ReactivePower,
  ApparentPower,
  ActiveEnergy,
  ReactiveEnergy,
  ApparentEnergy
}

public static class ChartMeasureExtensions
{
  public static decimal ChartValue(
    this IMeasurement measurement,
    ChartMeasure measure,
    PhaseModel? phase)
  {
    var byTariff = measure switch
    {
      ChartMeasure.Current => measurement.Current_A,
      ChartMeasure.Voltage => measurement.Voltage_V,
      ChartMeasure.ActivePower => measurement.ActivePower_W,
      ChartMeasure.ReactivePower => measurement.ReactivePower_VAR,
      ChartMeasure.ApparentPower => measurement.ApparentPower_VA,
      ChartMeasure.ActiveEnergy => measurement.ActiveEnergy_Wh,
      ChartMeasure.ReactiveEnergy => measurement.ReactiveEnergy_VARh,
      ChartMeasure.ApparentEnergy => measurement.ApparentEnergy_VAh,
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };

    var byDuplex = byTariff.TariffUnary();

    var byPhase = byDuplex.DuplexAny();

    var result = phase switch
    {
      PhaseModel.L1 => byPhase.PhaseSplit().ValueL1,
      PhaseModel.L2 => byPhase.PhaseSplit().ValueL2,
      PhaseModel.L3 => byPhase.PhaseSplit().ValueL3,
      _ => byPhase.PhaseSum()
    };

    return result;
  }

  public static string ToTitle(this ChartMeasure measure)
  {
    return measure switch
    {
      ChartMeasure.Current => "Current",
      ChartMeasure.Voltage => "Voltage",
      ChartMeasure.ActivePower => "Active Power",
      ChartMeasure.ReactivePower => "Reactive Power",
      ChartMeasure.ApparentPower => "Apparent Power",
      ChartMeasure.ActiveEnergy => "Active Energy",
      ChartMeasure.ReactiveEnergy => "Reactive Energy",
      ChartMeasure.ApparentEnergy => "Apparent Energy",
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };
  }

  public static TariffMeasure<decimal> GetMeasure(
    this IMeasurement measurement,
    ChartMeasure measure)
  {
    return measure switch
    {
      ChartMeasure.Current => measurement.Current_A,
      ChartMeasure.Voltage => measurement.Voltage_V,
      ChartMeasure.ActivePower => measurement.ActivePower_W,
      ChartMeasure.ReactivePower => measurement.ReactivePower_VAR,
      ChartMeasure.ApparentPower => measurement.ApparentPower_VA,
      ChartMeasure.ActiveEnergy => measurement.ActiveEnergy_Wh,
      ChartMeasure.ReactiveEnergy => measurement.ReactiveEnergy_VARh,
      ChartMeasure.ApparentEnergy => measurement.ApparentEnergy_VAh,
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };
  }
}
