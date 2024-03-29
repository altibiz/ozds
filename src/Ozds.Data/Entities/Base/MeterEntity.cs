using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity : AuditableEntity
{
  private string _stringId = default!;

  public override string Id { get => _stringId; init => _stringId = value; }

  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  private long _catalogueId = default!;

  public virtual string CatalogueId { get => _catalogueId.ToString(); init => _catalogueId = long.Parse(value); }

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

  private long _measurementValidatorId = default!;

  public virtual string MeasurementValidatorId { get => _measurementValidatorId.ToString(); init => _measurementValidatorId = long.Parse(value); }

  public virtual TMeasurementValidator MeasurementValidator { get; set; } =
    default!;
}

public class
  MeterInheritedEntityTypeConfiguration :
  EntityTypeHierarchyConfiguration<MeterEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder
      .UseTphMappingStrategy()
      .ToTable("meters")
      .HasDiscriminator<string>("kind");

    builder
      .HasOne(nameof(MeterEntity.Messenger))
      .WithMany(nameof(MessengerEntity.Meters))
      .HasForeignKey(nameof(MeterEntity.MessengerId));

    builder
      .HasOne(nameof(MeterEntity.Catalogue))
      .WithMany(nameof(CatalogueEntity.Meters))
      .HasForeignKey("_catalogueId");

    builder
      .Property(nameof(MeterEntity.Id))
      .HasColumnType("text")
      .HasConversion((Type?)null);

    builder
      .Property(nameof(MeterEntity.ConnectionPower_W))
      .HasColumnName("connection_power_w");

    builder.Ignore(nameof(MeterEntity.CatalogueId));
    builder
      .Property("_catalogueId")
      .HasColumnName("catalogue_id");

    if (type != typeof(MeterEntity))
    {
      builder
        .HasMany(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.Measurements))
        .WithOne(nameof(MeasurementEntity<MeterEntity>.Meter));

      builder
        .HasMany(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.Aggregates))
        .WithOne(nameof(AggregateEntity<MeterEntity>.Meter));

      builder
        .HasOne(
          nameof(MeterEntity<MeasurementEntity, AggregateEntity,
            MeasurementValidatorEntity>.MeasurementValidator))
        .WithOne(nameof(MeasurementValidatorEntity<MeterEntity>.Meter))
        .HasForeignKey(type.Name, "_measurementValidatorId");

      builder.Ignore(nameof(MeterEntity<MeasurementEntity, AggregateEntity,
        MeasurementValidatorEntity>.MeasurementValidatorId));
      builder
        .Property("_measurementValidatorId")
        .HasColumnName("measurement_validator_id");
    }
  }
}
