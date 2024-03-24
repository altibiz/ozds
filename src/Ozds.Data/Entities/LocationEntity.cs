using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationEntity : AuditableEntity
{
  public virtual ICollection<RepresentativeEntity> Representatives { get; set; } = default!;

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } = default!;

  public virtual ICollection<MessengerEntity> Messengers { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(MeasurementLocation))]
  public string MeasurementLocationId { get; set; } = default!;

  public virtual LocationMeasurementLocationEntity MeasurementLocation { get; set; } = default!;

  public virtual ICollection<LocationInvoiceEntity> Invoices { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(WhiteMediumCatalogue))]
  public string WhiteMediumCatalogueId { get; set; } = default!;

  public virtual WhiteMediumCatalogueEntity WhiteMediumCatalogue { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(BlueLowCatalogue))]
  public string BlueLowCatalogueId { get; set; } = default!;

  public virtual BlueLowCatalogueEntity BlueLowCatalogue { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(WhiteLowCatalogue))]
  public string WhiteLowCatalogueId { get; set; } = default!;

  public virtual WhiteLowCatalogueEntity WhiteLowCatalogue { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(RedLowCatalogue))]
  public string RedLowCatalogueId { get; set; } = default!;

  public virtual RedLowCatalogueEntity RedLowCatalogue { get; set; } = default!;

  [CascadeSoftDelete]
  [ForeignKey(nameof(RegulatoryCatalogue))]
  public string RegulatoryCatalogueId { get; set; } = default!;

  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } = default!;
}
