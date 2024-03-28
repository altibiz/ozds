using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public abstract class MeasurementValidatorEntity : AuditableEntity
{
}

public class MeasurementValidatorEntity<TMeter> : MeasurementValidatorEntity
  where TMeter : MeterEntity
{
  public string MeterId { get; set; } = default!;

  public virtual TMeter Meter { get; set; } = default!;
}

public class MeasurementValidatorTypeConfiguration : EntityTypeConfiguration<MeasurementValidatorEntity>
{
  public override void Configure(EntityTypeBuilder<MeasurementValidatorEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_validators")
      .HasDiscriminator<string>("kind");
  }
}

public class MeasurementValidatorEntityTypeHierarchyConfiguration : ConcreteHierarchyEntityTypeConfiguration<MeasurementValidatorEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_validators")
      .HasDiscriminator<string>("kind");

    builder
      .HasOne(nameof(MeasurementValidatorEntity<MeterEntity>.Meter))
      .WithOne(nameof(MeterEntity<MeasurementEntity, AggregateEntity, MeasurementValidatorEntity<MeterEntity>>.MeasurementValidator))
      .HasForeignKey(typeof(T).Name, nameof(MeasurementValidatorEntity<MeterEntity>.MeterId));
  }
}
