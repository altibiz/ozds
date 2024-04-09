using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  private readonly long _catalogueId;
  private long _networkUserId;

  public virtual string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    init { _networkUserId = long.Parse(value); }
  }

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public virtual string CatalogueId
  {
    get { return _catalogueId.ToString(); }
    init { _catalogueId = long.Parse(value); }
  }

  public virtual CatalogueEntity Catalogue { get; set; } = default!;
}

public class
  NetworkUserMeasurementLocationEntityTypeConfiguration :
  EntityTypeConfiguration<NetworkUserMeasurementLocationEntity>
{
  public override void Configure(
    EntityTypeBuilder<NetworkUserMeasurementLocationEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserMeasurementLocationEntity.NetworkUser))
      .WithMany(nameof(NetworkUserEntity.MeasurementLocations))
      .HasForeignKey("_networkUserId");

    builder
      .HasOne(nameof(NetworkUserMeasurementLocationEntity.Catalogue))
      .WithMany(nameof(CatalogueEntity.Meters))
      .HasForeignKey("_catalogueId");

    builder.Ignore(nameof(NetworkUserMeasurementLocationEntity.NetworkUserId));
    builder
      .Property("_networkUserId")
      .HasColumnName("network_user_id");

    builder.Ignore(nameof(NetworkUserMeasurementLocationEntity.CatalogueId));
    builder
      .Property("_catalogueId")
      .HasColumnName("catalogue_id");
  }
}
