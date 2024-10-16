using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Time;

namespace Ozds.Business.Analysis;

public record Analysis(
  Load Load,
  Consumption LastMonthConsumption,
  Consumption ThisMonthConsumption,
  Expenses LastMonthExpenses
);

public record LocationAnalysis(
  LocationModel Location,
  Analysis Analysis
);

public record NetworkUserAnalysis(
  NetworkUserModel Location,
  Analysis Analysis
);

public record MeasurementLocationAnalysis(
  MeasurementLocationModel MeasurementLocation,
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

    var consumption = models
      .SelectMany(x => x.MonthlyAggregates)
      .Consumption()
      .ToList();
    var lastMonthConsumption = consumption
      .FirstOrDefault(x => x.Timestamp == startOfLastMonth)
      ?? new Consumption(startOfLastMonth, 0M, 0M, 0M);
    var thisMonthConsumption = consumption
      .FirstOrDefault(x => x.Timestamp == startOfThisMonth)
      ?? new Consumption(startOfLastMonth, 0M, 0M, 0M);

    var load = models
      .Select(x => x.LastMeasurement)
      .Load();

    var expenses = models.SelectMany(x => x.Invoices).Expenses();
    var lastMonthExpenses = expenses
      .Where(x => x.Timestamp >= startOfLastMonth)
      .Where(x => x.Timestamp < startOfThisMonth)
      .Aggregate();

    return new Analysis(
      load,
      lastMonthConsumption,
      thisMonthConsumption,
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
          x.First().MeasurementLocation,
          x.Analysis()
        );
      })
      .ToList();
  }
}
