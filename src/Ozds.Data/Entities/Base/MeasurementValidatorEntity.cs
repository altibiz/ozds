using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class MeasurementValidatorEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))]
  [Column(TypeName = "bigint")]
  public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementValidatorEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<MeasurementValidatorEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_validators")
      .HasDiscriminator<string>("kind");
  }
}
