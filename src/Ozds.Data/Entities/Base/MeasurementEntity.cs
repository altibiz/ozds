using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp))]
public abstract class MeasurementEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }
}

[PrimaryKey(nameof(Timestamp), nameof(MeterId))]
public abstract class MeasurementEntity<T> : MeasurementEntity
  where T : MeterEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  // FIXME: global filter on deleted meters prevents this from being required
  [Required] public virtual T Meter { get; set; } = default!;
}

public class MeasurementEntityTypeConfiguration : InheritedEntityTypeConfiguration<MeasurementEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.UseTpcMappingStrategy();
  }
}

