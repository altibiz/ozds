using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public class EventEntity : IEventEntity, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  protected readonly long _id;

  public DateTimeOffset Timestamp { get; set; }

  public LevelEntity Level { get; set; }

  public JsonDocument Content { get; set; } = default!;

  public virtual ICollection<NotificationEntity> Notifications { get; set; } =
    default!;

  public List<CategoryEntity> Categories { get; set; } = default!;

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
  public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
  {
    Content?.Dispose();
  }

  public virtual string Id
  {
    get { return _id.ToString(); }
    init
    {
      _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string Title { get; set; } = default!;

  public string Kind { get; set; } = default!;
}

public class
  EventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<EventEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    if (entity == typeof(EventEntity))
    {
      builder.HasKey("_id");
    }

    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>(nameof(EventEntity.Kind));

    builder.Ignore(nameof(EventEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
      .UseIdentityAlwaysColumn();

    builder
      .Property<DateTimeOffset>(nameof(EventEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
