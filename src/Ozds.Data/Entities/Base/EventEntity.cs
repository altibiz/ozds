using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class EventEntity : ReadonlyEntity
{
  public string Id { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; } = default!;

  public LevelEntity Level { get; set; } = default!;

  public string Description { get; set; } = default!;
}

public class EventEntityTypeConfiguration : EntityTypeConfiguration<EventEntity>
{
  public override void Configure(EntityTypeBuilder<EventEntity> builder)
  {
    builder.HasKey(nameof(EventEntity.Id));
  }
}

public class EventEntityTypeHierarchyConfiguration : ConcreteHierarchyEntityTypeConfiguration<EventEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");

    builder
      .Property(nameof(EventEntity.Id))
      .ValueGeneratedOnAdd()
      .UseIdentityAlwaysColumn()
      .HasColumnType("bigint")
      .HasConversion<long>();

    builder
      .Property(x => x.Timestamp)
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
