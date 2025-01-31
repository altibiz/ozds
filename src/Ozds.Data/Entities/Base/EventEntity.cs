using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public class EventEntity : IdentifiableEntity, IEventEntity, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  public virtual ICollection<NotificationEntity> Notifications { get; set; } =
    default!;

  public string Kind { get; set; } = default!;

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
  public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
  {
    Content?.Dispose();
  }

  public DateTimeOffset Timestamp { get; set; }

  public LevelEntity Level { get; set; }

  public JsonDocument Content { get; set; } = default!;

  public List<CategoryEntity> Categories { get; set; } = default!;
}

public class
  EventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<EventEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>(nameof(EventEntity.Kind));

    builder
      .Property<DateTimeOffset>(nameof(EventEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
