using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : AuditableEntity
{
  public virtual ICollection<RepresentativeEntity> Representatives { get; set; } = default!;

  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<NetworkUserMeasurementLocationEntity> MeasurementLocations { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceEntity> Invoices { get; set; } = default!;
}
