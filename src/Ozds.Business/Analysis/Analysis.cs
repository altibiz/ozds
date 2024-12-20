using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Time;

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

public static class AnalysisExtensions
{
  public static Analysis Analysis(
    this IEnumerable<AnalysisBasisModel> models
  )
  {
    var now = DateTimeOffset.UtcNow;
    var startOfLastMonth = now.GetStartOfLastMonth();
    var startOfThisMonth = now.GetStartOfMonth();

    var monthlyConsumption = models
      .SelectMany(x => x.MonthlyAggregates)
      .Consumption()
      .ToList();
    var lastMonthConsumption = monthlyConsumption
      .FirstOrDefault(x => x.Timestamp == startOfLastMonth)
      ?? Consumption.Null;
    var thisMonthConsumption = monthlyConsumption
      .FirstOrDefault(x => x.Timestamp == startOfThisMonth)
      ?? Consumption.Null;

    var monthlyMaxLoad = models
      .SelectMany(x => x.MonthlyAggregates
        .Select(x => x.Load())
        .OrderByDescending(x => x.Timestamp))
      .ToList();
    var load = models
      .Select(x => x.LastMeasurement)
      .Load();

    var expenses = models.SelectMany(x => x.Invoices).Expenses();
    var lastMonthExpenses = expenses
      .Where(x => x.Timestamp >= startOfLastMonth)
      .Where(x => x.Timestamp < startOfThisMonth)
      .Aggregate();

    var monthlyAnalyses = monthlyConsumption
      .Join(
        monthlyMaxLoad,
        x => x.Timestamp.GetStartOfMonth(),
        x => x.Timestamp.GetStartOfMonth(),
        (x, y) => new MonthlyAnalysis(
          x.Timestamp.GetStartOfMonth(),
          y,
          x
        ))
      .OrderByDescending(x => x.StartOfMonth)
      .ToList();

    return new Analysis(
      load,
      lastMonthConsumption,
      thisMonthConsumption,
      monthlyAnalyses,
      lastMonthExpenses
    );
  }

  public static List<LocationAnalysis> AnalysesByLocation(
    this IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.Location.Id)
      .Select(x =>
      {
        return new LocationAnalysis(
          x.First().Location,
          x.Analysis()
        );
      })
      .ToList();
  }

  public static List<NetworkUserAnalysis> AnalysesByNetworkUser(
    this IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .Where(x => x.NetworkUser is { })
      .GroupBy(x => x.NetworkUser!.Id)
      .Select(x =>
      {
        return new NetworkUserAnalysis(
          x.First().Location,
          x.First().NetworkUser!,
          x.Analysis()
        );
      })
      .ToList();
  }

  public static List<MeasurementLocationAnalysis> AnalysesByMeasurementLocation(
    this IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.MeasurementLocation.Id)
      .Select(x =>
      {
        return new MeasurementLocationAnalysis(
          x.First().Location,
          x.First().NetworkUser,
          x.First().MeasurementLocation,
          x.First().Meter,
          x.Analysis()
        );
      })
      .ToList();
  }

  public static List<MeterAnalysis> AnalysesByMeter(
    this IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.Meter.Id)
      .Select(x =>
      {
        return new MeterAnalysis(
          x.First().Location,
          x.First().NetworkUser,
          x.First().MeasurementLocation,
          x.First().Meter,
          x.Analysis()
        );
      })
      .ToList();
  }

  public static MeterAnalysis? AnalysisForMeter(
    this IEnumerable<AnalysisBasisModel> models,
    string id
  )
  {
    var bases = models.Where(x => x.Meter.Id == id);
    var first = bases.FirstOrDefault();
    if (first is null)
    {
      return null;
    }

    return new MeterAnalysis(
      first.Location,
      first.NetworkUser,
      first.MeasurementLocation,
      first.Meter,
      bases.Analysis()
    );
  }
}
