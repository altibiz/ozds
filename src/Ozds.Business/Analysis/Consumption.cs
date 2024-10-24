using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Analysis;

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

public static class ConsumptionExtensions
{
  public static List<Consumption> Consumption(
    this IEnumerable<IAggregate> models
  )
  {
    return models
      .GroupBy(x => x.Timestamp)
      .Select(x => new Consumption(
        Timestamp: x.Key,
        MinActiveEnergy_kWh: x
          .Select(x => x.ActiveEnergySpan_Wh
            .SpanMin()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        MaxActiveEnergy_kWh: x
          .Select(x => x.ActiveEnergySpan_Wh
            .SpanMax()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        ActiveEnergy_kWh: x
          .Select(x => x.ActiveEnergySpan_Wh
            .SpanDiff()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        MinReactiveEnergy_kVARh: x
          .Select(x => x.ReactiveEnergySpan_VARh
            .SpanMin()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        MaxReactiveEnergy_kVARh: x
          .Select(x => x.ReactiveEnergySpan_VARh
            .SpanMax()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        ReactiveEnergy_kVARh: x
          .Select(x => x.ReactiveEnergySpan_VARh
            .SpanDiff()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        MinApparentEnergy_kVAh: x
          .Select(x => x.ApparentEnergySpan_VAh
            .SpanMin()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        MaxApparentEnergy_kVAh: x
          .Select(x => x.ApparentEnergySpan_VAh
            .SpanMax()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M,
        ApparentEnergy_kVAh: x
          .Select(x => x.ApparentEnergySpan_VAh
            .SpanDiff()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M
      ))
      .OrderByDescending(x => x.Timestamp)
      .ToList();
  }
}