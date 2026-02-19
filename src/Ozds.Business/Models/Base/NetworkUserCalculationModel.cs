using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class NetworkUserCalculationModel
  : CalculationModel,
    INetworkUserCalculation
{
  [Required]
  public required string MeterId { get; set; } = default!;

  [Required]
  public required IMeter ArchivedMeter { get; set; } = default!;

  [Required]
  public required string NetworkUserMeasurementLocationId { get; set; } =
    default!;

  [Required]
  public required NetworkUserMeasurementLocationModel ArchivedNetworkUserMeasurementLocation { get; set; } =
    default!;

  [Required]
  public required string UsageNetworkUserCatalogueId { get; set; } = default!;

  [Required]
  public required string SupplyRegulatoryCatalogueId { get; set; } = default!;

  [Required]
  public required string NetworkUserInvoiceId { get; set; } = default!;

  [Required]
  public required RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue { get; set; } =
    default!;

  public abstract NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue { get; }
}
