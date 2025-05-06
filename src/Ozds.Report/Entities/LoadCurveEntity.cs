namespace Ozds.Report.Entities;

public class LoadCurveEntity
{
  public string MeasurementLocationCode { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; } = default!;

  public string ObisCode { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public decimal Energy_kx { get; set; } = default!;

  public decimal Power_kx { get; set; } = default!;
}
