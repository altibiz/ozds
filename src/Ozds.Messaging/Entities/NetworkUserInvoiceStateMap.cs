using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Messaging.Entities;

// TODO: work out foreign keys

public class NetworkUserInvoiceStateMap : SagaClassMap<NetworkUserInvoiceStateEntity>
{
  protected override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceStateEntity> entity,
    ModelBuilder model
  )
  {
    entity.ToTable("network_user_invoice_states");

    entity
      .Property(x => x.NetworkUserInvoiceId)
      .IsRequired();

    entity
      .HasIndex(x => x.NetworkUserInvoiceId)
      .IsUnique();

    entity
      .Property(x => x.RowVersion)
      .HasColumnName("xmin")
      .HasColumnType("xid")
      .IsRowVersion();
  }
}
