using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeasurementLocationEntity : AuditableEntity
{
  public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementLocationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<MeasurementLocationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    if (entity == typeof(MeasurementLocationEntity))
    {
      builder
        .UseTphMappingStrategy()
        .ToTable("measurement_locations")
        .HasDiscriminator<string>("kind");
    }

    builder
      .HasOne(nameof(MeasurementLocationEntity.Meter))
      .WithOne(
        nameof(MeterEntity<MeasurementEntity, AggregateEntity,
          MeasurementValidatorEntity>.MeasurementLocation))
      .HasForeignKey(entity.Name, nameof(MeasurementLocationEntity.MeterId));
  }
}
