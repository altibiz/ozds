using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Messaging.Sagas;

// TODO: work out foreign keys

public class NetworkUserInvoiceStateMap : SagaClassMap<NetworkUserInvoiceState>
{
  protected override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceState> entity,
    ModelBuilder model
  )
  {
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
