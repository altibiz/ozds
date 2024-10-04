using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Enums;

public enum MeasureModel
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
    MeasureModel measure,
    PhaseModel? phase)
  {
    var byTariff = measure switch
    {
      MeasureModel.Current => measurement.Current_A,
      MeasureModel.Voltage => measurement.Voltage_V,
      MeasureModel.ActivePower => measurement.ActivePower_W,
      MeasureModel.ReactivePower => measurement.ReactivePower_VAR,
      MeasureModel.ApparentPower => measurement.ApparentPower_VA,
      MeasureModel.ActiveEnergy => measurement.ActiveEnergy_Wh,
      MeasureModel.ReactiveEnergy => measurement.ReactiveEnergy_VARh,
      MeasureModel.ApparentEnergy => measurement.ApparentEnergy_VAh,
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

  public static string ToTitle(this MeasureModel measure)
  {
    return measure switch
    {
      MeasureModel.Current => "Current",
      MeasureModel.Voltage => "Voltage",
      MeasureModel.ActivePower => "Active Power",
      MeasureModel.ReactivePower => "Reactive Power",
      MeasureModel.ApparentPower => "Apparent Power",
      MeasureModel.ActiveEnergy => "Active Energy",
      MeasureModel.ReactiveEnergy => "Reactive Energy",
      MeasureModel.ApparentEnergy => "Apparent Energy",
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };
  }

  public static string ToUnit(this MeasureModel measure)
  {
    return measure switch
    {
      MeasureModel.Current => "A",
      MeasureModel.Voltage => "V",
      MeasureModel.ActivePower => "W",
      MeasureModel.ReactivePower => "VAR",
      MeasureModel.ApparentPower => "VA",
      MeasureModel.ActiveEnergy => "Wh",
      MeasureModel.ReactiveEnergy => "VARh",
      MeasureModel.ApparentEnergy => "VAh",
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };
  }

  public static TariffMeasure<decimal> GetMeasure(
    this IMeasurement measurement,
    MeasureModel measure)
  {
    return measure switch
    {
      MeasureModel.Current => measurement.Current_A,
      MeasureModel.Voltage => measurement.Voltage_V,
      MeasureModel.ActivePower => measurement.ActivePower_W,
      MeasureModel.ReactivePower => measurement.ReactivePower_VAR,
      MeasureModel.ApparentPower => measurement.ApparentPower_VA,
      MeasureModel.ActiveEnergy => measurement.ActiveEnergy_Wh,
      MeasureModel.ReactiveEnergy => measurement.ReactiveEnergy_VARh,
      MeasureModel.ApparentEnergy => measurement.ApparentEnergy_VAh,
      _ => throw new ArgumentOutOfRangeException(nameof(measure), measure, null)
    };
  }
}
