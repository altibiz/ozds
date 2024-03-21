using Ozds.Data.Entities.Base;

// TODO: figure out how to slap on all the necessary entities without
// ef core automatically making relationships

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceEntity : IdEntity
{
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
}
