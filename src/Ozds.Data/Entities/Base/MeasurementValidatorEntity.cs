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
  public virtual TMeter Meter { get; set; } = default!;
}

public class MeasurementValidatorEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<MeasurementValidatorEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_validators")
      .HasDiscriminator<string>("kind");
  }
}
