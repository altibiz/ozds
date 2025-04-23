using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class AccountingPeriodReportModel : ReportModel
{
  public string MeasurementLocationCode { get; set; } = default!;

  public string ObisCode { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; } = default!;

  public decimal Value { get; set; } = default!;

  public string Unit { get; set; } = default!;
}
