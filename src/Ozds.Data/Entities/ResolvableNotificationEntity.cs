using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class ResolvableNotificationEntity : NotificationEntity
{
  public string? ResolvedById { get; set; } = default!;

  public virtual RepresentativeEntity? ResolvedBy { get; set; } = default!;

  public DateTimeOffset? ResolvedOn { get; set; } = default!;
}

public class ResolvableNotificationEntityModelConfiguration :
  EntityTypeConfiguration<ResolvableNotificationEntity>
{
  public override void Configure(EntityTypeBuilder<ResolvableNotificationEntity> modelBuilder)
  {
    modelBuilder
      .HasOne(nameof(ResolvableNotificationEntity.ResolvedBy))
      .WithMany(nameof(RepresentativeEntity.ResolvedNotifications))
      .HasForeignKey(nameof(ResolvableNotificationEntity.ResolvedById));
  }
}
