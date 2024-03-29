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
    builder.HasKey("_id");
  }
}

public class
  EventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<EventEntity>
{
  public override void Configure<TEntity>(EntityTypeBuilder<TEntity> builder)
  {
    builder.Ignore(nameof(EventEntity.Id));
  }

  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");

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
