using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MessengerNotificationEntity : ResolvableNotificationEntity
{
  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;
}

public class MessengerInactivityNotificationEntityConfiguration :
  IEntityTypeConfiguration<MessengerNotificationEntity>
{
  public void Configure(EntityTypeBuilder<MessengerNotificationEntity> builder)
  {
    builder
      .HasOne(nameof(MessengerNotificationEntity.Messenger))
      .WithOne(nameof(MessengerEntity.InactivityNotifications))
      .HasForeignKey(nameof(MessengerNotificationEntity.MessengerId));
  }
}
