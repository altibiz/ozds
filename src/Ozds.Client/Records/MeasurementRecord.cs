namespace Ozds.Client.Records;

public class MeasurementRecord
{
  public string MeterId { get; set; } = default!;

  public required string MeasurementLocationId { get; set; }

  public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

  public decimal Current_A { get; set; } = default!;

  public decimal Voltage_V { get; set; } = default!;

  public decimal ActivePower_W { get; set; } = default!;

  public decimal ReactivePower_VAR { get; set; } = default!;

  public decimal ApparentPower_VA { get; set; } = default!;

  public decimal ActiveEnergy_Wh { get; set; } = default!;

  public decimal ReactiveEnergy_VARh { get; set; } = default!;

  public decimal ApparentEnergy_VAh { get; set; } = default!;
}
