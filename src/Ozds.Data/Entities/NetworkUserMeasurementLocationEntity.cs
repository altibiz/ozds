using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserMeasurementLocationEntity : MeasurementLocationEntity
{
  public string NetworkUserId { get; set; } = default!;
  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;
}

public class NetworkUserMeasurementLocationEntityTypeConfiguration : EntityTypeConfiguration<NetworkUserMeasurementLocationEntity>
{
  public override void Configure(EntityTypeBuilder<NetworkUserMeasurementLocationEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserMeasurementLocationEntity.NetworkUser))
      .WithMany(nameof(NetworkUserEntity.MeasurementLocations))
      .HasForeignKey(nameof(NetworkUserMeasurementLocationEntity.NetworkUserId));

    builder
      .Property(nameof(NetworkUserMeasurementLocationEntity.NetworkUserId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
