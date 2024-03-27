using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp), nameof(MeterId), "number_partitions => 2")]
[PrimaryKey(nameof(Timestamp), nameof(MeterId))]
public abstract class MeasurementEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }

  [ForeignKey("Meter")] public string MeterId { get; set; } = default!;
}

public abstract class MeasurementEntity<T> : MeasurementEntity
  where T : MeterEntity
{

  // FIXME: global filter on deleted meters prevents this from being required
  [Required] public virtual T Meter { get; set; } = default!;
}

public class MeasurementEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<MeasurementEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.UseTpcMappingStrategy();
  }
}
