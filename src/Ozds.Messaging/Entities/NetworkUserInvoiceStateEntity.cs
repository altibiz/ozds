using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Messaging.Entities.Base;

namespace Ozds.Messaging.Entities;

public class NetworkUserInvoiceStateEntity : Entity
{
  public string NetworkUserInvoiceId { get; set; } = default!;

  public string? BillId { get; set; }

  public string? AbortReason { get; set; }
}

public class
  NetworkUserInvoiceStateMap : SagaClassMap<NetworkUserInvoiceStateEntity>
{
  protected override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceStateEntity> entity,
    ModelBuilder model
  )
  {
    entity.ToTable("network_user_invoice_states");

    entity.ConfigureEntity();

    entity
      .Property(x => x.NetworkUserInvoiceId)
      .IsRequired();

    entity
      .HasIndex(x => x.NetworkUserInvoiceId)
      .IsUnique();
  }
}
