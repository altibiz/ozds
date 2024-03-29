using Microsoft.EntityFrameworkCore;
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

public class MeasurementValidatorEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<MeasurementValidatorEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    if (type == typeof(MeasurementValidatorEntity))
    {
      builder
        .UseTphMappingStrategy()
        .ToTable("measurement_validators")
        .HasDiscriminator<string>("kind");
    }

    builder
      .HasOne(nameof(MeasurementValidatorEntity<MeterEntity>.Meter))
      .WithOne(
        nameof(MeterEntity<MeasurementEntity, AggregateEntity,
          MeasurementValidatorEntity<MeterEntity>>.MeasurementValidator))
      .HasForeignKey(type.Name,
        nameof(MeasurementValidatorEntity<MeterEntity>.MeterId));
  }
}
