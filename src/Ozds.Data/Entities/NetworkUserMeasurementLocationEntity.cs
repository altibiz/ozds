using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  private long _networkUserId = default!;
  public virtual string NetworkUserId { get => _networkUserId.ToString(); init => _networkUserId = long.Parse(value); }
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
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

    builder.Ignore(nameof(NetworkUserMeasurementLocationEntity.NetworkUserId));
  }
}
