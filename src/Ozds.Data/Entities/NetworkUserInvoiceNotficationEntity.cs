using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceNotificationEntity : NotificationEntity
{
  private long _invoiceId;

  public string InvoiceId
  {
    get { return _invoiceId.ToString(); }
    init { _invoiceId = long.Parse(value); }
  }

  public virtual NetworkUserInvoiceEntity Invoice { get; set; } = default!;
}

public class NetworkUserInvoiceNotificationEntityConfiguration :
  IEntityTypeConfiguration<NetworkUserInvoiceNotificationEntity>
{
  public void Configure(
    EntityTypeBuilder<NetworkUserInvoiceNotificationEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserInvoiceNotificationEntity.Invoice))
      .WithOne(nameof(NetworkUserInvoiceEntity.Notifications))
      .HasForeignKey("_invoiceId");
    builder
      .Property("_invoiceId")
      .HasColumnName("invoice_id");
    builder.Ignore(nameof(NetworkUserInvoiceNotificationEntity.InvoiceId));
  }
}
