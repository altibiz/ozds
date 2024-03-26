using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

public class MeasurementLocationEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementLocationEntityTypeConfiguration : IEntityTypeConfiguration<MeasurementLocationEntity>
{
  public void Configure(EntityTypeBuilder<MeasurementLocationEntity> builder)
  {
    builder.ToTable("measurement_locations").HasDiscriminator<int>("kind");
  }
}
