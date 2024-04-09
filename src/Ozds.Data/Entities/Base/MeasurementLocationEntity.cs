using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeasurementLocationEntity : AuditableEntity
{
  public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;

  public virtual ICollection<CalculationEntity> Calculations { get; set; } =
    default!;
}

public class MeasurementLocationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<MeasurementLocationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    if (type == typeof(MeasurementLocationEntity))
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
      .HasForeignKey(type.Name, nameof(MeasurementLocationEntity.MeterId));
  }
}
