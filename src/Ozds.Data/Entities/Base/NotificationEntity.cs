using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class NotificationEntity : IdentifiableEntity, INotificationEntity
{
  protected long? _eventId;

  public virtual EventEntity? Event { get; set; } = default!;

  public virtual ICollection<NotificationRecipientEntity>
    NotificationRepresentatives { get; set; } = default!;

  public virtual ICollection<RepresentativeEntity>
    Representatives { get; set; } = default!;

  public string Kind { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; }

  public string? EventId
  {
    get { return _eventId?.ToString(); }
    set
    {
      _eventId = value is { } notNullValue ? long.Parse(notNullValue) : null;
    }
  }

  public string Summary { get; set; } = default!;

  public string Content { get; set; } = default!;

  public List<TopicEntity> Topics { get; set; } = default!;
}

public class
  NotificationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<NotificationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("notifications")
      .HasDiscriminator<string>(nameof(NotificationEntity.Kind));

    builder
      .Property<DateTimeOffset>(nameof(NotificationEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .HasOne(nameof(NotificationEntity.Event))
      .WithMany(nameof(EventEntity.Notifications))
      .HasForeignKey("_eventId");

    builder.Ignore(nameof(NotificationEntity.EventId));
    builder
      .Property("_eventId")
      .HasColumnName("event_id")
      .HasColumnType("bigint");
  }
}
