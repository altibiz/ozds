using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  [ForeignKey(nameof(NetworkUser))]
  public string NetworkUserId { get; set; } = default!;

  // FIXME: global filter on deleted meters prevents this from being required
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
}
