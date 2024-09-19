using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Joins;

// TODO: to notification_recipients

public class NotificationRecipientEntity : IEntity
{
  private long _notificationId;

  public string NotificationId
  {
    get { return _notificationId.ToString(); }
    set
    {
      _notificationId = value is { } notNullValue
        ? long.Parse(notNullValue)
        : default;
    }
  }

  public string RepresentativeId { get; set; } = default!;

  public virtual NotificationEntity Notification { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;

  public DateTimeOffset? SeenOn { get; set; } = default!;
}

public class
  NotificationRepresentativeEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var entity = modelBuilder.Entity<NotificationEntity>();

    entity
      .HasMany(nameof(NotificationEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.Notifications))
      .UsingEntity(
        typeof(NotificationRecipientEntity),
        configureLeft: l => l
          .HasOne(nameof(NotificationRecipientEntity.Notification))
          .WithMany(nameof(NotificationEntity.NotificationRepresentatives))
          .HasForeignKey("_notificationId"),
        configureRight: r => r
          .HasOne(nameof(NotificationRecipientEntity.Representative))
          .WithMany(nameof(RepresentativeEntity.NotificationRepresentatives))
          .HasForeignKey(nameof(NotificationRecipientEntity.RepresentativeId)),
        configureJoinEntityType: entity =>
        {
          entity.ToTable("notification_recipient_entity");
          entity.Ignore(nameof(NotificationRecipientEntity.NotificationId));
          entity
            .Property("_notificationId")
            .HasColumnName("notification_id");
        }
      );
  }
}
