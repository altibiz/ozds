using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Analysis;

public record Load(
  decimal ActivePower_kW,
  decimal ReactivePower_kVAR,
  decimal ApparentPower_kVA
);

public static class LoadExtensions
{
  public static Load Load(this IEnumerable<IMeasurement> measurements)
  {
    return new Load(
      ActivePower_kW: measurements
        .Select(measurement => measurement.ActivePower_W
          .TariffUnary()
          .DuplexImport()
          .PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M,
      ReactivePower_kVAR: measurements
        .Select(measurement => measurement.ReactivePower_VAR
          .TariffUnary()
          .DuplexImport()
          .PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M,
      ApparentPower_kVA: measurements
        .Select(measurement => measurement.ApparentPower_VA
          .TariffUnary()
          .DuplexImport()
          .PhaseSum())
        .DefaultIfEmpty(0M)
        .Sum() / 1000M
    );
  }
}
