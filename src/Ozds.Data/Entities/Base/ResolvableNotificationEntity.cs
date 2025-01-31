using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class ResolvableNotificationEntity
  : NotificationEntity, IResolvableNotificationEntity
{
  public virtual RepresentativeEntity? ResolvedBy { get; set; } = default!;
  public string? RepresentativeId { get; set; }

  public string? ResolvedById { get; set; } = default!;

  public DateTimeOffset? ResolvedOn { get; set; } = default!;
}

public class ResolvableNotificationEntityModelConfiguration :
  EntityTypeHierarchyConfiguration<ResolvableNotificationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .HasOne(nameof(ResolvableNotificationEntity.ResolvedBy))
      .WithMany(nameof(RepresentativeEntity.ResolvableNotifications))
      .HasForeignKey(nameof(ResolvableNotificationEntity.ResolvedById));

    builder.Ignore(nameof(ResolvableNotificationEntity.RepresentativeId));
  }
}
