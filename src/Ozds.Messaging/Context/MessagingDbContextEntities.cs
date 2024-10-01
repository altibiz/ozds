using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Context;

public partial class MessagingDbContext
{
  public DbSet<NetworkUserInvoiceStateEntity> NetworkUserInvoiceStates { get; set; } = default!;
}
