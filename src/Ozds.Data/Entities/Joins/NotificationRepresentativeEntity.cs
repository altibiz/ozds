using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Joins;

public class NotificationRepresentativeEntity
{
  private long _notificationId;

  public string NotificationId
  {
    get { return _notificationId.ToString(); }
    set
    {
      _notificationId = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string RepresentativeId { get; set; } = default!;

  public virtual NotificationEntity Notification { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;

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
          .HasForeignKey("_notificationId"),
        configureRight: r => r
          .HasOne(nameof(NotificationRepresentativeEntity.Representative))
          .WithMany(nameof(RepresentativeEntity.NotificationRepresentatives))
          .HasForeignKey(nameof(NotificationRepresentativeEntity.RepresentativeId)),
        configureJoinEntityType: entity =>
        {
          entity.Ignore(nameof(NotificationRepresentativeEntity.NotificationId));
          entity
            .Property("_notificationId")
            .HasColumnName("notification_id");
        }
      );
  }
}
