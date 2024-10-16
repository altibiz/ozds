using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class MeasurementValidatorEntity : AuditableEntity, IMeasurementValidatorEntity
{
  public virtual ICollection<MeterEntity> Meters { get; set; } =
    default!;
}

public class MeasurementValidatorEntity<TMeter> : MeasurementValidatorEntity
  where TMeter : MeterEntity
{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
  public virtual ICollection<TMeter> Meters { get; set; } = default!;
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
}

public class MeasurementValidatorEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<MeasurementValidatorEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_validators")
      .HasDiscriminator<string>("kind");
  }
}
