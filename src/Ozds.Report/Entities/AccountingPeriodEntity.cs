namespace Ozds.Report.Entities;

public class AccountingPeriodEntity
{
  public string MeasurementLocationCode { get; set; } = default!;

  public string ObisCode { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; } = default!;

  public decimal Value { get; set; } = default!;

  public string Unit { get; set; } = default!;
}
