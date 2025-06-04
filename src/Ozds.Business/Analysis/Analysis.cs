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
