using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  private long _networkUserCatalogueId;
  private long _networkUserId;

  public virtual string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    set { _networkUserId = long.Parse(value); }
  }

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public virtual string NetworkUserCatalogueId
  {
    get { return _networkUserCatalogueId.ToString(); }
    set { _networkUserCatalogueId = long.Parse(value); }
  }

  public virtual NetworkUserCatalogueEntity NetworkUserCatalogue { get; set; } =
    default!;

  public virtual ICollection<NetworkUserCalculationEntity>
    NetworkUserCalculations { get; set; } =
    default!;
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
      .WithMany(nameof(NetworkUserEntity.NetworkUserMeasurementLocations))
      .HasForeignKey("_networkUserId");

    builder
      .HasOne(nameof(NetworkUserMeasurementLocationEntity.NetworkUserCatalogue))
      .WithMany(
        nameof(NetworkUserCatalogueEntity
          .NetworkUserMeasurementLocations))
      .HasForeignKey("_networkUserCatalogueId");

    builder.Ignore(nameof(NetworkUserMeasurementLocationEntity.NetworkUserId));
    builder
      .Property("_networkUserId")
      .HasColumnName("network_user_id");

    builder.Ignore(
      nameof(NetworkUserMeasurementLocationEntity
        .NetworkUserCatalogueId));
    builder
      .Property("_networkUserCatalogueId")
      .HasColumnName("network_user_catalogue_id");
  }
}
