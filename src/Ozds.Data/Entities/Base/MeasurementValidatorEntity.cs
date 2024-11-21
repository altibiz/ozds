using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeasurementValidatorEntity : AuditableEntity, IMeasurementValidatorEntity
{
  public virtual ICollection<MeterEntity> Meters { get; set; } =
    default!;

  public string Kind { get; set; } = default!;
}

public class MeasurementValidatorEntity<
#pragma warning disable S2326 // Unused type parameters should be removed
  TMeter
#pragma warning restore S2326 // Unused type parameters should be removed
> : MeasurementValidatorEntity
  where TMeter : MeterEntity
{
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
      .HasDiscriminator<string>(nameof(MeasurementValidatorEntity.Kind));
  }
}
