using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Analysis;

public record MonthlyAnalysis(
  DateTimeOffset StartOfMonth,
  Load MaxLoad,
  Consumption Consumption
);

public record Analysis(
  Load Load,
  Consumption LastMonthConsumption,
  Consumption ThisMonthConsumption,
  List<MonthlyAnalysis> Monthly,
  Expenses LastMonthExpenses
);

public record LocationAnalysis(
  LocationModel Location,
  Analysis Analysis
);

public record NetworkUserAnalysis(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  Analysis Analysis
);

public record MeasurementLocationAnalysis(
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  Analysis Analysis
);

public record MeterAnalysis(
  LocationModel Location,
  NetworkUserModel? NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  Analysis Analysis
);

public record Consumption(
  DateTimeOffset Timestamp,
  decimal MinActiveEnergy_kWh,
  decimal MaxActiveEnergy_kWh,
  decimal ActiveEnergy_kWh,
  decimal MinReactiveEnergy_kVARh,
  decimal MaxReactiveEnergy_kVARh,
  decimal ReactiveEnergy_kVARh,
  decimal MinApparentEnergy_kVAh,
  decimal MaxApparentEnergy_kVAh,
  decimal ApparentEnergy_kVAh
)
{
  public static readonly Consumption Null = new(
    DateTimeOffset.MinValue,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0
  );
}

public record Expenses(
  DateTimeOffset Timestamp,
  decimal Total_EUR
);

public record Load(
  DateTimeOffset Timestamp,
  decimal ActivePower_kW,
  decimal ReactivePower_kVAR,
  decimal ApparentPower_kVA
);
