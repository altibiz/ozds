using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserInvoiceModel : InvoiceModel
{
  public string? BillId { get; set; }

  [Required]
  public required string LocationId { get; set; }

  [Required]
  public required LocationModel ArchivedLocation { get; set; } = default!;

  [Required]
  public required string NetworkUserId { get; set; }

  [Required]
  public required NetworkUserModel ArchivedNetworkUser { get; set; } = default!;

  [Required]
  public required RegulatoryCatalogueModel ArchivedRegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  [Required]
  public required decimal UsageActiveEnergyTotalImportT0Fee_EUR { get; set; } =
    default!;

  [Required]
  public required decimal UsageActiveEnergyTotalImportT1Fee_EUR { get; set; } =
    default!;

  [Required]
  public required decimal UsageActiveEnergyTotalImportT2Fee_EUR { get; set; } =
    default!;

  [Required]
  public required decimal
    UsageActivePowerTotalImportT1PeakFee_EUR
  { get; set; } = default!;

  [Required]
  public required decimal
    UsageReactiveEnergyTotalRampedT0Fee_EUR
  { get; set; } = default!;

  [Required]
  public required decimal UsageMeterFee_EUR { get; set; } = default!;

  [Required]
  public required decimal UsageFeeTotal_EUR { get; set; } = default!;

  [Required]
  public required decimal SupplyActiveEnergyTotalImportT1Fee_EUR { get; set; } =
    default!;

  [Required]
  public required decimal SupplyActiveEnergyTotalImportT2Fee_EUR { get; set; } =
    default!;

  [Required]
  public required decimal SupplyBusinessUsageFee_EUR { get; set; } = default!;

  [Required]
  public required decimal SupplyRenewableEnergyFee_EUR { get; set; } = default!;

  [Required]
  public required decimal SupplyFeeTotal_EUR { get; set; } = default!;
}
