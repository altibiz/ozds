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
  ApparentEnergy,
}

public static class MeasureExtensions
{
  public static decimal ChartValue(
    this IMeasurement measurement,
    MeasureModel measure,
    OrderOfMagnitudeModel? orderOfMagnitude = null,
    TariffModel? tariff = null,
    DuplexModel? duplex = null,
    AggregationModel? aggregation = null,
    PhaseModel? phase = null
  )
  {
    var byTariff = measurement.GetMeasure(measure, orderOfMagnitude);
    var byDuplex = byTariff.GetMeasure(tariff, measure);
    var byPhase = byDuplex.GetMeasure(duplex, measure);
    var byAggregation = byPhase.GetMeasure(aggregation, measure);
    var result = byAggregation.GetMeasure(phase, measure);
    return result;
  }

  public static decimal RegisterValue(
    this IMeasurement measurement,
    RegisterModel register
  )
  {
    var byTariff = measurement.GetMeasure(
      register.Measure,
      register.OrderOfMagnitude
    );
    var byDuplex = byTariff.GetMeasure(register.Tariff, register.Measure);
    var byPhase = byDuplex.GetMeasure(register.Duplex, register.Measure);
    var byAggregation = byPhase.GetMeasure(
      register.Aggregation,
      register.Measure
    );
    var result = byAggregation.GetMeasure(register.Phase, register.Measure);
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
      _ => throw new ArgumentOutOfRangeException(
        nameof(measure),
        measure,
        null
      ),
    };
  }

  public static string ToUnit(
    this MeasureModel measure,
    OrderOfMagnitudeModel? orderOfMagnitude = null
  )
  {
    orderOfMagnitude ??= measure.DefaultOrderOfMagnitude();

    var unit = measure switch
    {
      MeasureModel.Current => "A",
      MeasureModel.Voltage => "V",
      MeasureModel.ActivePower => "W",
      MeasureModel.ReactivePower => "VAR",
      MeasureModel.ApparentPower => "VA",
      MeasureModel.ActiveEnergy => "Wh",
      MeasureModel.ReactiveEnergy => "VARh",
      MeasureModel.ApparentEnergy => "VAh",
      _ => throw new ArgumentOutOfRangeException(
        nameof(measure),
        measure,
        null
      ),
    };

    return $"{orderOfMagnitude.ToPrefix()}{unit}";
  }

  public static string ToUnitTitle(
    this MeasureModel measure,
    OrderOfMagnitudeModel? orderOfMagnitude = null
  )
  {
    orderOfMagnitude ??= measure.DefaultOrderOfMagnitude();

    var unit = measure switch
    {
      MeasureModel.Current => "Amperes",
      MeasureModel.Voltage => "Volts",
      MeasureModel.ActivePower => "Watts",
      MeasureModel.ReactivePower => "Volt-Amperes Reactive",
      MeasureModel.ApparentPower => "Volt-Amperes Apparent",
      MeasureModel.ActiveEnergy => "Watt-Hours",
      MeasureModel.ReactiveEnergy => "Volt-Amperes Reactive Hours",
      MeasureModel.ApparentEnergy => "Volt-Amperes Apparent Hours",
      _ => throw new ArgumentOutOfRangeException(
        nameof(measure),
        measure,
        null
      ),
    };

    var orderOfMagnitudeTitlePrefix = orderOfMagnitude.ToTitle();
    if (orderOfMagnitudeTitlePrefix.Length > 0)
    {
      orderOfMagnitudeTitlePrefix += " ";
    }

    return $"{orderOfMagnitudeTitlePrefix}{unit}";
  }

  public static TariffMeasure<decimal> GetMeasure(
    this IMeasurement measurement,
    MeasureModel measure,
    OrderOfMagnitudeModel? orderOfMagnitude = null
  )
  {
    orderOfMagnitude ??= measure.DefaultOrderOfMagnitude();

    var byMeasure = measure switch
    {
      MeasureModel.Current => measurement.Current_A,
      MeasureModel.Voltage => measurement.Voltage_V,
      MeasureModel.ActivePower => measurement.ActivePower_W,
      MeasureModel.ReactivePower => measurement.ReactivePower_VAR,
      MeasureModel.ApparentPower => measurement.ApparentPower_VA,
      MeasureModel.ActiveEnergy => measurement.ActiveEnergy_Wh,
      MeasureModel.ReactiveEnergy => measurement.ReactiveEnergy_VARh,
      MeasureModel.ApparentEnergy => measurement.ApparentEnergy_VAh,
      _ => throw new ArgumentOutOfRangeException(
        nameof(measure),
        measure,
        null
      ),
    };

    var multiplier = orderOfMagnitude.ToMultiplier();

    return byMeasure.Multiply(multiplier);
  }

  public static OrderOfMagnitudeModel? DefaultOrderOfMagnitude(
    this MeasureModel measure
  )
  {
    return
      measure
        is MeasureModel.ActivePower
          or MeasureModel.ReactivePower
          or MeasureModel.ApparentPower
          or MeasureModel.ActiveEnergy
          or MeasureModel.ReactiveEnergy
          or MeasureModel.ApparentEnergy
      ? OrderOfMagnitudeModel.Kilo
      : null;
  }
}
