namespace Ozds.Business.Pushing.Requests;

public enum MeterRegisterType
{
  Irradiance,
  Frequency,
  Current,
  ReactivePower,
  Pressure,
  Power,
  VolumetricFlow,
  MassFlow,
  Resistance,
  ApparentPower,
  TotalHarmonicDistortion,
  Temperature,
  Voltage,
  Numeric,
  Monetary,
  Angle,
  RelativeHumidity,
  Speed,
  Charge
}

public static class MeterRegisterTypeExtensions
{
  public static MeterRegisterUnit Unit(this MeterRegisterType type)
  {
    return type switch
    {
      MeterRegisterType.Irradiance => MeterRegisterUnit.WattsPerSquareMeter,
      MeterRegisterType.Frequency => MeterRegisterUnit.Hertz,
      MeterRegisterType.Current => MeterRegisterUnit.Ampere,
      MeterRegisterType.ReactivePower => MeterRegisterUnit.VoltAmpereReactive,
      MeterRegisterType.Pressure => MeterRegisterUnit.Pascal,
      MeterRegisterType.Power => MeterRegisterUnit.Watt,
      MeterRegisterType.VolumetricFlow => MeterRegisterUnit.Mm3ps,
      MeterRegisterType.MassFlow => MeterRegisterUnit.GramPerSecond,
      MeterRegisterType.Resistance => MeterRegisterUnit.Ohm,
      MeterRegisterType.ApparentPower => MeterRegisterUnit.VoltAmpere,
      MeterRegisterType.TotalHarmonicDistortion => MeterRegisterUnit.Percent,
      MeterRegisterType.Temperature => MeterRegisterUnit.Celsius,
      MeterRegisterType.Voltage => MeterRegisterUnit.Volt,
      MeterRegisterType.Numeric => MeterRegisterUnit.None,
      MeterRegisterType.Monetary => MeterRegisterUnit.CurrencyPerSecond,
      MeterRegisterType.Angle => MeterRegisterUnit.Degrees,
      MeterRegisterType.RelativeHumidity => MeterRegisterUnit.Percent,
      MeterRegisterType.Speed => MeterRegisterUnit.MeterPerSecond,
      MeterRegisterType.Charge => MeterRegisterUnit.AmpereHours,
      var _ => default
    };
  }
}

public static class MeterRegisterTypeString
{
  public const string Irradiance = "Ee";
  public const string Frequency = "F";
  public const string Current = "I";
  public const string ReactivePower = "PQ";
  public const string Pressure = "Pa";
  public const string Power = "P";
  public const string VolumetricFlow = "Qv";
  public const string MassFlow = "Q";
  public const string Resistance = "R";
  public const string ApparentPower = "S";
  public const string TotalHarmonicDistortion = "THD";
  public const string Temperature = "T";
  public const string Voltage = "V";
  public const string Numeric = "#";
  public const string Monetary = "$";
  public const string Angle = "a";
  public const string RelativeHumidity = "h";
  public const string Speed = "v";
  public const string Charge = "Qe";

  public static MeterRegisterType? ToMeterRegisterType(
    this string egaugeRegisterTypeString
  )
  {
    return egaugeRegisterTypeString switch
    {
      Irradiance => MeterRegisterType.Irradiance,
      Frequency => MeterRegisterType.Frequency,
      Current => MeterRegisterType.Current,
      ReactivePower => MeterRegisterType.ReactivePower,
      Pressure => MeterRegisterType.Pressure,
      Power => MeterRegisterType.Power,
      VolumetricFlow => MeterRegisterType.VolumetricFlow,
      MassFlow => MeterRegisterType.MassFlow,
      Resistance => MeterRegisterType.Resistance,
      ApparentPower => MeterRegisterType.ApparentPower,
      TotalHarmonicDistortion => MeterRegisterType.TotalHarmonicDistortion,
      Temperature => MeterRegisterType.Temperature,
      Voltage => MeterRegisterType.Voltage,
      Numeric => MeterRegisterType.Numeric,
      Monetary => MeterRegisterType.Monetary,
      Angle => MeterRegisterType.Angle,
      RelativeHumidity => MeterRegisterType.RelativeHumidity,
      Speed => MeterRegisterType.Speed,
      Charge => MeterRegisterType.Charge,
      var _ => default
    };
  }
}
