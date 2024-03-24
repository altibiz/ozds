using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationInvoiceEntity : InvoiceEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;
}
