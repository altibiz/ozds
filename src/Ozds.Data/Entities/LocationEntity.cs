using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationEntity : SoftDeletableEntity
{
  public virtual List<RepresentativeEntity> Representatives { get; set; } = default!;

  public virtual List<NetworkUserEntity> NetworkUsers { get; set; } = default!;

  public virtual LocationMeasurementLocationEntity MeasurementLocation { get; set; } = default!;

  public virtual List<LocationInvoiceEntity> Invoices { get; set; } = default!;

  public virtual WhiteMediumCatalogueEntity WhiteMediumCatalogue { get; set; } = default!;

  public virtual BlueLowCatalogueEntity BlueLowCatalogue { get; set; } = default!;

  public virtual WhiteLowCatalogueEntity WhiteLowCatalogue { get; set; } = default!;

  public virtual RedLowCatalogueEntity RedLowCatalogue { get; set; } = default!;

  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } = default!;
}
