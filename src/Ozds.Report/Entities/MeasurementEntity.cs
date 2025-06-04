using System.Globalization;

namespace Ozds.Report.Entities;

public class MeasurementEntity
{
  public string MeterId { get; set; } = default!;

  public required string MeasurementLocationId { get; set; }

  public DateTimeOffset Timestamp { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public decimal Current_A { get; set; } = default!;

  public decimal Voltage_V { get; set; } = default!;

  public decimal ActivePower_W { get; set; } = default!;

  public decimal ReactivePower_VAR { get; set; } = default!;

  public decimal ApparentPower_VA { get; set; } = default!;

  public decimal ActiveEnergy_Wh { get; set; } = default!;

  public decimal ReactiveEnergy_VARh { get; set; } = default!;

  public decimal ApparentEnergy_VAh { get; set; } = default!;
}
