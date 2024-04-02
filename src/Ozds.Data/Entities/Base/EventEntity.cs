using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class EventEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  public DateTimeOffset Timestamp { get; set; }

  public LevelEntity Level { get; set; }

  public string Description { get; set; } = default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = long.Parse(value); }
  }

  public string Title { get; set; } = default!;
}

public class
  EventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<EventEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    if (type == typeof(EventEntity))
    {
      builder.HasKey("_id");
    }

    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");

    builder.Ignore(nameof(InvoiceEntity.Id));
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
