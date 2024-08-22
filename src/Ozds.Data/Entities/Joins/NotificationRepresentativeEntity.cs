using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Joins;

public class NotificationRepresentativeEntity
{
  public string NotificationId { get; set; } = default!;

  public string RepresentativeId { get; set; } = default!;

  public NotificationEntity Notification { get; set; } = default!;

  public RepresentativeEntity Representative { get; set; } = default!;

  public DateTimeOffset? SeenOn { get; set; } = default!;
}

public class NotificationRepresentativeEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var entity = modelBuilder.Entity<NotificationEntity>();

    entity
      .HasMany(nameof(NotificationEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.Notifications))
      .UsingEntity(
        joinEntityType: typeof(NotificationRepresentativeEntity),
        configureLeft: l => l
          .HasOne(nameof(NotificationRepresentativeEntity.Notification))
          .WithMany(nameof(NotificationEntity.NotificationRepresentatives))
          .HasForeignKey(nameof(NotificationRepresentativeEntity.NotificationId)),
        configureRight: r => r
          .HasOne(nameof(NotificationRepresentativeEntity.Representative))
          .WithMany(nameof(RepresentativeEntity.NotificationRepresentatives))
          .HasForeignKey(nameof(NotificationRepresentativeEntity.RepresentativeId)),
        configureJoinEntityType: j => j
          .Property(nameof(NotificationRepresentativeEntity.SeenOn))
          .HasDefaultValue(null)
      );
  }
}
