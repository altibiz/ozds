using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class EventEntity : ReadonlyEntity
{
  private readonly long _id;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = long.Parse(value); }
  }

  public DateTimeOffset Timestamp { get; set; }

  public LevelEntity Level { get; set; }

  public string Description { get; set; } = default!;
}

public class EventEntityTypeConfiguration : EntityTypeConfiguration<EventEntity>
{
  public override void Configure(EntityTypeBuilder<EventEntity> builder)
  {
    builder.HasKey(nameof(EventEntity.Id));
  }
}

public class
  EventEntityTypeHierarchyConfiguration :
  ConcreteHierarchyEntityTypeConfiguration<EventEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");

    builder.Ignore(nameof(EventEntity.Id));
    builder
      .Property("_id")
      .HasColumnType("bigint")
      .HasColumnName("id")
      .ValueGeneratedOnAdd()
      .UseIdentityAlwaysColumn();

    builder
      .Property(x => x.Timestamp)
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
