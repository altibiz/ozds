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

public static class MeasureExtensions
{
  public static decimal ChartValue(
    this IMeasurement measurement,
    MeasureModel measure,
    TariffModel? tariff = null,
    DuplexModel? duplex = null,
    PhaseModel? phase = null
  )
  {
    var byTariff = measurement.GetMeasure(measure);
    var byDuplex = byTariff.GetMeasure(tariff, measure);
    var byPhase = byDuplex.GetMeasure(duplex, measure);
    var result = byPhase.GetMeasure(phase, measure);
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
      MeasureModel.ActivePower => "kW",
      MeasureModel.ReactivePower => "VAR",
      MeasureModel.ApparentPower => "VA",
      MeasureModel.ActiveEnergy => "kWh",
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
