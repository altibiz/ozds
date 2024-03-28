using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AggregateEntity : ReadonlyEntity
{
  public DateTimeOffset Timestamp { get; set; }

  public long Count { get; set; }

  public IntervalEntity Interval { get; set; }
}

public class AggregateEntity<T> : AggregateEntity where T : MeterEntity
{
  public string MeterId { get; set; } = default!;

  public virtual T Meter { get; set; } = default!;
}

public class
  AggregateEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<
  AggregateEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(
      nameof(AggregateEntity.Timestamp),
      nameof(AggregateEntity.Interval),
      nameof(AggregateEntity<MeterEntity>.MeterId)
    );

    builder.UseTpcMappingStrategy();

    builder.HasTimescaleHypertable(
      nameof(AggregateEntity.Timestamp),
      nameof(AggregateEntity<MeterEntity>.MeterId),
      "number_partitions => 2"
    );

    builder
      .HasOne(nameof(AggregateEntity<MeterEntity>.Meter))
      .WithMany()
      .HasForeignKey(nameof(AggregateEntity<MeterEntity>.MeterId));

    builder
      .Property(x => x.Timestamp)
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
