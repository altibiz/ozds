using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationInvoiceEntity : InvoiceEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  // FIXME: global filter on deleted meters prevents this from being required
  public virtual LocationEntity Location { get; set; } = default!;
}
