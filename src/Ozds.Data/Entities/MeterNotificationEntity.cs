using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MeterNotificationEntity : ResolvableNotificationEntity
{
  public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeterInactivityNotificationEntityConfiguration :
  IEntityTypeConfiguration<MeterNotificationEntity>
{
  public void Configure(EntityTypeBuilder<MeterNotificationEntity> builder)
  {
    builder
      .HasOne(nameof(MeterNotificationEntity.Meter))
      .WithMany(nameof(MeterEntity.InactivityNotifications))
      .HasForeignKey(nameof(MeterNotificationEntity.MeterId));
  }
}
