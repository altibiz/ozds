namespace Ozds.Document.Entities;

public abstract class NetworkUserCalculationEntity : CalculationEntity
{
  public string MeterTitle { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public string MeasurementLocationTitle { get; set; } = default!;

  public string MeasurementLocationId { get; set; } = default!;

  public abstract NetworkUserCatalogueEntity UsageNetworkUserCatalogue { get; }

  public string UsageNetworkUserCatalogueId { get; set; } = default!;

  public string SupplyRegulatoryCatalogueId { get; set; } = default!;

  public string NetworkUserInvoiceId { get; set; } = default!;

  public RegulatoryCatalogueEntity SupplyRegulatoryCatalogue { get; set; } =
    default!;
}
