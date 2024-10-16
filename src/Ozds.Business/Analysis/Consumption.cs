using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Analysis;

public record Consumption(
  DateTimeOffset Timestamp,
  decimal ActiveEnergy_kWh,
  decimal ReactiveEnergy_kVARh,
  decimal ApparentEnergy_kVAh
);

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
        ActiveEnergy_kWh: x
          .Select(x => x.ActiveEnergySpan_Wh
            .SpanDiff()
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
        ApparentEnergy_kVAh: x
          .Select(x => x.ApparentEnergySpan_VAh
            .SpanDiff()
            .TariffUnary()
            .DuplexImport()
            .PhaseSum())
          .DefaultIfEmpty(0M)
          .Sum() / 1000M
      ))
      .ToList();
  }
}
