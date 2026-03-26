namespace Ozds.Report.Entities;

public class ShortenedEnergyCardReportEntity
{
  public string MeasurementLocationTitle { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT1_kWh { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT2_kWh { get; set; } = default!;
}
