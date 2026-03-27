using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class ShortenedEnergyCardReportModel : ReportModel
{
  public string MeasurementLocationTitle { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT1_kWh { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT2_kWh { get; set; } = default!;
}
