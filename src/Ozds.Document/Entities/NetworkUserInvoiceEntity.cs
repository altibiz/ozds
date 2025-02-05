namespace Ozds.Document.Entities;

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  public string? BillId { get; set; }

  public LocationEntity Location { get; set; } = default!;

  public NetworkUserEntity NetworkUser { get; set; } = default!;

  public RegulatoryCatalogueEntity RegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  public decimal UsageActiveEnergyTotalImportT0Fee_EUR { get; set; } =
    default!;

  public decimal UsageActiveEnergyTotalImportT1Fee_EUR { get; set; } =
    default!;

  public decimal UsageActiveEnergyTotalImportT2Fee_EUR { get; set; } =
    default!;

  public decimal
    UsageActivePowerTotalImportT1PeakFee_EUR
  { get; set; } = default!;

  public decimal
    UsageReactiveEnergyTotalRampedT0Fee_EUR
  { get; set; } = default!;

  public decimal UsageMeterFee_EUR { get; set; } = default!;

  public decimal UsageFeeTotal_EUR { get; set; } = default!;

  public decimal SupplyActiveEnergyTotalImportT1Fee_EUR { get; set; } =
    default!;

  public decimal SupplyActiveEnergyTotalImportT2Fee_EUR { get; set; } =
    default!;

  public decimal SupplyBusinessUsageFee_EUR { get; set; } = default!;

  public decimal SupplyRenewableEnergyFee_EUR { get; set; } = default!;

  public decimal SupplyFeeTotal_EUR { get; set; } = default!;

  public string NetworkUserId { get; set; } = default!;
}
