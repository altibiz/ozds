using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceNotificationEntity : NotificationEntity
{
  private long _invoiceId;

  public string InvoiceId
  {
    get { return _invoiceId.ToString(); }
    init
    {
      _invoiceId = value is { } notNullValue
        ? long.Parse(notNullValue)
        : default;
    }
  }

  public virtual NetworkUserInvoiceEntity Invoice { get; set; } = default!;
}

public class NetworkUserInvoiceNotificationEntityConfiguration :
  EntityTypeConfiguration<NetworkUserInvoiceNotificationEntity>
{
  public override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceNotificationEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserInvoiceNotificationEntity.Invoice))
      .WithMany(nameof(NetworkUserInvoiceEntity.Notifications))
      .HasForeignKey("_invoiceId");

    builder.Ignore(nameof(NetworkUserInvoiceNotificationEntity.InvoiceId));
    builder
      .Property("_invoiceId")
      .HasColumnName("invoice_id");
  }
}
