namespace Ozds.Report.Entities;

public class EnergyCardEntity
{
  public string SocialSecurityNumber { get; set; } = default!;

  public string NetworkUserTitle { get; set; } = default!;

  public string MeasurementLocationCode { get; set; } = default!;

  public string TariffModel { get; set; } = default!;

  public decimal ConnectionPower_W { get; set; } = default!;

  public string MeasurementLocationTitle { get; set; } = default!;

  public string LocationTitle { get; set; } = default!;

  public string LocationAddress { get; set; } = default!;

  public string LocationCity { get; set; } = default!;

  public string LocationPostalCode { get; set; } = default!;

  public string Year { get; set; } = default!;

  public string BillingPeriod { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT0_kWh { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT1_kWh { get; set; } = default!;

  public decimal? ActiveEnergyTotalImportT2_kWh { get; set; } = default!;

  public decimal? ReactiveEnergyTotalImportT0_kVARh { get; set; } = default!;

  public decimal? ReactiveEnergyTotalExportT0_kVARh { get; set; } = default!;

  public decimal? ActivePowerTotalImportT1_kW { get; set; } = default!;
}
