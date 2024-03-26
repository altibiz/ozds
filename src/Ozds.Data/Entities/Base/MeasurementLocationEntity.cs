using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[Table("measurement_locations")]
public abstract class MeasurementLocationEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementLocationEntityTypeConfiguration : EntityTypeConfiguration<MeasurementLocationEntity>
{
  public override void Configure(EntityTypeBuilder<MeasurementLocationEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_locations")
      .HasDiscriminator<int>("kind");
  }
}
