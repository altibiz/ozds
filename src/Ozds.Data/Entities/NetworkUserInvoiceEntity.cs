using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  public string NetworkUserId { get; set; } = default!;
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
}

public class
  NetworkUserInvoiceEntityTypeConfiguration : EntityTypeConfiguration<
  NetworkUserInvoiceEntity>
{
  public override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserInvoiceEntity.NetworkUser))
      .WithMany(nameof(NetworkUserEntity.Invoices))
      .HasForeignKey(nameof(NetworkUserInvoiceEntity.NetworkUserId));

    builder
      .Property(nameof(NetworkUserInvoiceEntity.NetworkUserId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
