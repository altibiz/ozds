using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("measurement_validators")]
public abstract class MeasurementValidatorEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementValidatorEntityTypeConfiguration : IEntityTypeConfiguration<MeasurementValidatorEntity>
{
  public void Configure(EntityTypeBuilder<MeasurementValidatorEntity> builder)
  {
    builder.ToTable("measurement_validators").HasDiscriminator<int>("kind");
  }
}
