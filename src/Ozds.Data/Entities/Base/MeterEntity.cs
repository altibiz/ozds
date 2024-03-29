using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity : AuditableEntity
{
  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;

  public string MeasurementLocationId { get; set; } = default!;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  public string CatalogueId { get; set; } = default!;

  public virtual CatalogueEntity Catalogue { get; set; } = default!;

  public float ConnectionPower_W { get; set; } = default!;

  public List<PhaseEntity> Phases { get; set; } = default!;
}

public class MeterEntity<
  TMeasurement,
  TAggregate,
  TMeasurementValidator
> : MeterEntity
  where TMeasurement : MeasurementEntity
  where TAggregate : AggregateEntity
  where TMeasurementValidator : MeasurementValidatorEntity
{
  public virtual ICollection<TMeasurement> Measurements { get; set; } =
    default!;

  public virtual ICollection<TAggregate> Aggregates { get; set; } = default!;

  public string MeasurementValidatorId { get; set; } = default!;

  public virtual TMeasurementValidator MeasurementValidator { get; set; } =
    default!;
}

public class
  MeterInheritedEntityTypeConfiguration :
  EntityTypeHierarchyConfiguration<MeterEntity>
{
  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("meters")
      .HasDiscriminator<string>("kind");

    builder
      .HasOne(nameof(MeterEntity.Messenger))
      .WithMany(nameof(MessengerEntity.Meters))
      .HasForeignKey(nameof(MeterEntity.MessengerId));

    builder
      .HasOne(nameof(MeterEntity.MeasurementLocation))
      .WithOne(nameof(MeasurementLocationEntity.Meter))
      .HasForeignKey(typeof(T).Name, nameof(MeterEntity.MeasurementLocationId));

    builder
      .HasOne(nameof(MeterEntity.Catalogue))
      .WithMany(nameof(CatalogueEntity.Meters))
      .HasForeignKey(nameof(MeterEntity.CatalogueId));

    builder
      .Property(nameof(MeterEntity.Id))
      .HasColumnType("text")
      .HasConversion((Type?)null);

    builder
      .Property(nameof(MeterEntity.ConnectionPower_W))
      .HasColumnName("connection_power_w");

    if (typeof(T) != typeof(MeterEntity))
    {
      builder
        .HasMany(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.Measurements))
        .WithOne(nameof(MeasurementEntity<MeterEntity>.Meter))
        .HasForeignKey(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.MessengerId));

      builder
        .HasMany(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.Aggregates))
        .WithOne(nameof(AggregateEntity<MeterEntity>.Meter))
        .HasForeignKey(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.MessengerId));

      builder
        .HasOne(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.MeasurementValidator))
        .WithOne(nameof(MeasurementValidatorEntity<MeterEntity>.Meter))
        .HasForeignKey(typeof(T).Name,
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.MeasurementValidatorId));
    }
  }
}
