using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp), nameof(MeterId))]
[PrimaryKey(nameof(Timestamp), nameof(Interval), nameof(MeterId))]
public abstract class AggregateEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }

  [Required] public long Count { get; set; }

  [Required] public IntervalEntity Interval { get; set; }

  [ForeignKey("Meter")] public string MeterId { get; set; } = default!;
}

public abstract class AggregateEntity<T> : AggregateEntity
  where T : MeterEntity
{
  // FIXME: global filter on deleted meters prevents this from being required
  [Required] public virtual T Meter { get; set; } = default!;
}

public class AggregateEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<AggregateEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.UseTpcMappingStrategy();
  }
}
