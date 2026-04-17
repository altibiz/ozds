using Ozds.Business.Analysis.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Analysis;

public record MonthlyAnalysis(
  DateTimeOffset StartOfMonth,
  Load Load,
  Consumption Consumption,
  List<IMeasurement> Measurements
) : IAnalysis;

public record Analysis(
  Load Load,
  Consumption LastMonthConsumption,
  Consumption ThisMonthConsumption,
  List<MonthlyAnalysis> Monthly,
  Expenses LastMonthExpenses
) : IAnalysis;

public record LocationAnalysis(LocationModel Location, Analysis Analysis)
  : IAnalysis;

public record NetworkUserAnalysis(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  Analysis Analysis
) : IAnalysis;

public record MeasurementLocationAnalysis(
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  Analysis Analysis
) : IAnalysis;

public record MeterAnalysis(
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  Analysis Analysis
) : IAnalysis;

public record Consumption(
  DateTimeOffset Timestamp,
  decimal MinActiveEnergy_kWh,
  decimal MaxActiveEnergy_kWh,
  decimal ActiveEnergy_kWh,
  decimal ActiveEnergy_Tariff1_kWh,
  decimal ActiveEnergy_Tariff2_kWh,
  decimal MinReactiveEnergy_kVARh,
  decimal MaxReactiveEnergy_kVARh,
  decimal ReactiveEnergy_kVARh,
  decimal MinApparentEnergy_kVAh,
  decimal MaxApparentEnergy_kVAh,
  decimal ApparentEnergy_kVAh
)
{
  public static readonly Consumption Null = new(
    Timestamp: DateTimeOffset.MinValue,
    MinActiveEnergy_kWh: 0,
    MaxActiveEnergy_kWh: 0,
    ActiveEnergy_kWh: 0,
    ActiveEnergy_Tariff1_kWh: 0,
    ActiveEnergy_Tariff2_kWh: 0,
    MinReactiveEnergy_kVARh: 0,
    MaxReactiveEnergy_kVARh: 0,
    ReactiveEnergy_kVARh: 0,
    MinApparentEnergy_kVAh: 0,
    MaxApparentEnergy_kVAh: 0,
    ApparentEnergy_kVAh: 0
  );
}

public record Expenses(DateTimeOffset Timestamp, decimal Total_EUR);

public record Load(
  DateTimeOffset Timestamp,
  decimal ActivePower_kW,
  decimal ReactivePower_kVAR,
  decimal ApparentPower_kVA
);
