using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class EventEntity : ReadonlyEntity
{
  public string Id { get; init; } = default!;

  public DateTimeOffset Timestamp { get; set; }

  public LevelEntity Level { get; set; }

  public string Description { get; set; } = default!;
}

public class EventEntityTypeConfiguration : EntityTypeConfiguration<EventEntity>
{
  public override void Configure(EntityTypeBuilder<EventEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");

    builder.HasKey(nameof(EventEntity.Id));
  }
}

public class
  EventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<EventEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder.HasKey("_id");
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
