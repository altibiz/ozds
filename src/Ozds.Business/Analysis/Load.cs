using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Analysis;

public record Load(
  DateTimeOffset Timestamp,
  decimal ActivePower_kW,
  decimal ReactivePower_kVAR,
  decimal ApparentPower_kVA
);

public static class LoadExtensions
{
  public static Load Load(this IMeasurement measurement)
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

  public static Load Load(this IEnumerable<IMeasurement> measurements)
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
