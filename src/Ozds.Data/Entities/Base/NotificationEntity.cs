using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class NotificationEntity : IIdentifiableEntity
{
  protected readonly long? _eventId;

  protected readonly long _id;

  public DateTimeOffset Timestamp { get; set; }

  public virtual string Id
  {
    get { return _id.ToString(); }
    init
    {
      _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string? EventId
  {
    get { return _eventId?.ToString(); }
    init { _eventId = value is { } notNullValue ? long.Parse(notNullValue) : default; }
  }

  public virtual EventEntity? Event { get; set; } = default!;

  public virtual ICollection<NotificationRecipientEntity> NotificationRepresentatives { get; set; } = default!;

  public virtual ICollection<RepresentativeEntity> Representatives { get; set; } = default!;

  public string Title { get; set; } = default!;

  public string Summary { get; set; } = default!;

  public string Content { get; set; } = default!;

  public TopicEntity Topic { get; set; } = default!;
}

public class
  AlertEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<NotificationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    if (entity == typeof(NotificationEntity))
    {
      builder.HasKey("_id");
    }

    builder
      .UseTphMappingStrategy()
      .ToTable("notifications")
      .HasDiscriminator<string>("kind");

    builder.Ignore(nameof(NotificationEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
      .UseIdentityAlwaysColumn();

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
