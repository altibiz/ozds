using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;

namespace Ozds.Business.Analysis;

public class Analyzer(
  ClockQueries clockQueries,
  TimeQueries timeQueries
)
{
  public List<LocationAnalysis> AnalyzeByLocation(
    IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.Location.Id)
      .Select(
        x =>
        {
          return new LocationAnalysis(
            x.First().Location,
            Analyze(x)
          );
        })
      .ToList();
  }

  public List<NetworkUserAnalysis> AnalyzeByNetworkUser(
    IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .Where(x => x.NetworkUser is not null)
      .GroupBy(x => x.NetworkUser!.Id)
      .Select(
        x =>
        {
          return new NetworkUserAnalysis(
            x.First().Location,
            x.First().NetworkUser!,
            Analyze(x)
          );
        })
      .ToList();
  }

  public List<MeasurementLocationAnalysis> AnalyzeByMeasurementLocation(
    IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.MeasurementLocation.Id)
      .Select(
        x =>
        {
          return new MeasurementLocationAnalysis(
            x.First().Location,
            x.First().NetworkUser,
            x.First().MeasurementLocation,
            x.First().Meter,
            Analyze(x)
          );
        })
      .ToList();
  }

  public List<MeterAnalysis> AnalyzeByMeter(
    IEnumerable<AnalysisBasisModel> models
  )
  {
    return models
      .GroupBy(x => x.Meter.Id)
      .Select(
        x =>
        {
          return new MeterAnalysis(
            x.First().Location,
            x.First().NetworkUser,
            x.First().MeasurementLocation,
            x.First().Meter,
            Analyze(x)
          );
        })
      .ToList();
  }

  public MeterAnalysis? AnalyzeForMeter(
    IEnumerable<AnalysisBasisModel> models,
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
      Analyze(bases)
    );
  }

  public Analysis Analyze(
    IEnumerable<AnalysisBasisModel> models
  )
  {
    var now = clockQueries.Now();
    var startOfLastMonth = timeQueries.GetStartOfLastMonth(now);
    var startOfThisMonth = timeQueries.GetStartOfMonth(now);

    var monthlyAggregates = models
      .SelectMany(x => x.MonthlyAggregates)
      .GroupBy(x => x.Timestamp)
      .Select(x => x.ToList())
      .ToList();

    var monthlyConsumption = AnalyzeConsumption(
        models
          .SelectMany(x => x.MonthlyAggregates))
      .ToList();
    var lastMonthConsumption = monthlyConsumption
        .FirstOrDefault(x => x.Timestamp == startOfLastMonth)
      ?? Consumption.Null;
    var thisMonthConsumption = monthlyConsumption
        .FirstOrDefault(x => x.Timestamp == startOfThisMonth)
      ?? Consumption.Null;

    var monthlyLoad = monthlyAggregates
      .Select(x => AnalyzeLoad(x))
      .ToList();
    var load = AnalyzeLoad(
      models
        .Select(x => x.LastMeasurement)
        .OfType<IMeasurement>());

    var expenses = AnalyzeExpenses(models.SelectMany(x => x.Invoices));
    var lastMonthExpenses = AggregateExpenses(
      expenses
        .Where(x => x.Timestamp >= startOfLastMonth)
        .Where(x => x.Timestamp < startOfThisMonth));

    var monthlyAnalyses = monthlyConsumption
      .Join(
        monthlyLoad,
        x => timeQueries.GetStartOfMonth(x.Timestamp),
        x => timeQueries.GetStartOfMonth(x.Timestamp),
        (consumption, load) => new { consumption, load })
      .Join(
        monthlyAggregates,
        x => timeQueries.GetStartOfMonth(x.consumption.Timestamp),
        x => timeQueries.GetStartOfMonth(x.First().Timestamp),
        (x, aggregates) => new
        {
          x.consumption,
          x.load,
          aggregates
        })
      .Select(
        x =>
          new MonthlyAnalysis(
            timeQueries.GetStartOfMonth(x.consumption.Timestamp),
            x.load,
            x.consumption,
            x.aggregates.Cast<IMeasurement>().ToList()
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

  public List<Consumption> AnalyzeConsumption(
    IEnumerable<IAggregate> models
  )
  {
    return models
      .GroupBy(x => x.Timestamp)
      .Select(
        x => new Consumption(
          x.Key,
          x
            .Select(
              x => x.ActiveEnergy_Wh
                .TariffUnary()
                .DuplexImport()
                .AggregateMin()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Min() / 1000M,
          x
            .Select(
              x => x.ActiveEnergy_Wh
                .TariffUnary()
                .DuplexImport()
                .AggregateMax()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Max() / 1000M,
          x
            .Select(
              x => x.ActiveEnergy_Wh
                .TariffUnary()
                .DuplexImport()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Sum() / 1000M,
          x
            .Select(
              x => x.ReactiveEnergy_VARh
                .TariffUnary()
                .DuplexImport()
                .AggregateMin()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Min() / 1000M,
          x
            .Select(
              x => x.ReactiveEnergy_VARh
                .TariffUnary()
                .DuplexImport()
                .AggregateMax()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Max() / 1000M,
          x
            .Select(
              x => x.ReactiveEnergy_VARh
                .TariffUnary()
                .DuplexImport()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Sum() / 1000M,
          x
            .Select(
              x => x.ApparentEnergy_VAh
                .TariffUnary()
                .DuplexImport()
                .AggregateMin()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Min() / 1000M,
          x
            .Select(
              x => x.ApparentEnergy_VAh
                .TariffUnary()
                .DuplexImport()
                .AggregateMax()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Max() / 1000M,
          x
            .Select(
              x => x.ApparentEnergy_VAh
                .TariffUnary()
                .DuplexImport()
                .PhaseSum())
            .DefaultIfEmpty(0M)
            .Sum() / 1000M
        ))
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }

  public List<Expenses> AnalyzeExpenses(
    IEnumerable<ICalculation> models
  )
  {
    return models
      .GroupBy(x => x.FromDate)
      .Select(
        x => new Expenses(
          x.Key,
          x
            .Select(x => x.Total_EUR)
            .DefaultIfEmpty(0)
            .Sum())
      )
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }

  public List<Expenses> AnalyzeExpenses(
    IEnumerable<IInvoice> models
  )
  {
    return models
      .GroupBy(x => x.FromDate)
      .Select(
        x => new Expenses(
          x.Key,
          x
            .Select(x => x.TotalWithTax_EUR)
            .DefaultIfEmpty(0)
            .Sum()
        ))
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }

  public Expenses AggregateExpenses(
    IEnumerable<Expenses> models)
  {
    return new Expenses(
      models
        .Select(x => x.Timestamp)
        .DefaultIfEmpty(DateTimeOffset.MinValue)
        .Min(),
      models
        .Select(x => x.Total_EUR)
        .DefaultIfEmpty(0)
        .Sum()
    );
  }

  public Load AnalyzeLoad(IMeasurement measurement)
  {
    return new Load(
      measurement.Timestamp,
      measurement.ActivePower_W
        .TariffUnary().DuplexImport().PhaseSum() / 1000M,
      measurement.ReactivePower_VAR
        .TariffUnary().DuplexImport().PhaseSum() / 1000M,
      measurement.ApparentPower_VA
        .TariffUnary().DuplexImport().PhaseSum() / 1000M
    );
  }

  public Load AnalyzeLoad(IEnumerable<IMeasurement> measurements)
  {
    return new Load(
      measurements.FirstOrDefault()?.Timestamp
      ?? DateTimeOffset.MinValue,
      measurements
        .Select(
          measurement => measurement.ActivePower_W
            .TariffUnary().DuplexImport().PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M,
      measurements
        .Select(
          measurement => measurement.ReactivePower_VAR
            .TariffUnary().DuplexImport().PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M,
      measurements
        .Select(
          measurement => measurement.ApparentPower_VA
            .TariffUnary().DuplexImport().PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M
    );
  }
}
