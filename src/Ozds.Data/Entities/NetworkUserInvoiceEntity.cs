using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  private long _networkUserId;

  public virtual string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    init { _networkUserId = long.Parse(value); }
  }

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public LocationEntity ArchivedLocation { get; set; } = default!;

  public NetworkUserEntity ArchivedNetworkUser { get; set; } = default!;
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
      .HasForeignKey("_networkUserId");

    builder.Ignore(nameof(NetworkUserInvoiceEntity.NetworkUserId));
    builder
      .Property("_networkUserId")
      .HasColumnName("network_user_id");

    builder
      .ComplexProperty(nameof(NetworkUserInvoiceEntity.ArchivedLocation));
    builder
      .ComplexProperty(nameof(LocationInvoiceEntity.ArchivedLocation))
      .Ignore(nameof(LocationEntity.CreatedBy))
      .Ignore(nameof(LocationEntity.LastUpdatedBy))
      .Ignore(nameof(LocationEntity.DeletedBy))
      .Ignore(nameof(LocationEntity.Invoices))
      .Ignore(nameof(LocationEntity.BlueLowNetworkUserCatalogue))
      .Ignore(nameof(LocationEntity.RedLowNetworkUserCatalogue))
      .Ignore(nameof(LocationEntity.WhiteLowNetworkUserCatalogue))
      .Ignore(nameof(LocationEntity.WhiteMediumNetworkUserCatalogue))
      .Ignore(nameof(LocationEntity.RegulatoryCatalogue))
      .Ignore(nameof(LocationEntity.Invoices))
      .Ignore(nameof(LocationEntity.NetworkUsers))
      .Ignore(nameof(LocationEntity.Messengers))
      .Ignore(nameof(LocationEntity.MeasurementLocations))
      .Ignore(nameof(LocationEntity.Representatives));

    builder
      .ComplexProperty(nameof(NetworkUserInvoiceEntity.ArchivedNetworkUser));
    builder
      .ComplexProperty(nameof(NetworkUserInvoiceEntity.ArchivedNetworkUser))
      .Ignore(nameof(NetworkUserEntity.CreatedBy))
      .Ignore(nameof(NetworkUserEntity.LastUpdatedBy))
      .Ignore(nameof(NetworkUserEntity.DeletedBy))
      .Ignore(nameof(NetworkUserEntity.Invoices))
      .Ignore(nameof(NetworkUserEntity.MeasurementLocations))
      .Ignore(nameof(NetworkUserEntity.Location))
      .Ignore(nameof(NetworkUserEntity.Representatives));
  }
}
