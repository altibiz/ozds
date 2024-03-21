using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationInvoiceEntity : IdEntity
{
  public virtual LocationEntity Location { get; set; } = default!;
}
