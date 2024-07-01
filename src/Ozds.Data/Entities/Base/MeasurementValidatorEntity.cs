using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class MeasurementValidatorEntity : AuditableEntity
{
}

public class MeasurementValidatorEntity<TMeter> : MeasurementValidatorEntity
  where TMeter : MeterEntity
{
  public virtual ICollection<TMeter> Meters { get; set; } = default!;
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
