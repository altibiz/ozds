using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity
  : AuditableEntity, ICustomIdentifiableEntity, IMeterEntity
{
  private long _measurementValidatorId;

  public string? MessengerId { get; set; } = default!;

  public virtual MessengerEntity? Messenger { get; set; } = default!;

  public virtual MeasurementLocationEntity? MeasurementLocation { get; set; } =
    default!;

  public float ConnectionPower_W { get; set; } = default!;

  public List<PhaseEntity> Phases { get; set; } = default!;

  public virtual ICollection<NetworkUserCalculationEntity>
    NetworkUserCalculations
  { get; set; } =
    default!;

  public virtual string MeasurementValidatorId
  {
    get { return _measurementValidatorId.ToString(); }
    set { _measurementValidatorId = long.Parse(value); }
  }

  public virtual MeasurementValidatorEntity MeasurementValidator { get; set; } =
    default!;

  public string Kind { get; set; } = default!;
}

public class MeterEntity<
  TMeasurement,
  TAggregate,
#pragma warning disable S2326 // Unused type parameters should be removed
  TMeasurementValidator
#pragma warning restore S2326 // Unused type parameters should be removed
> : MeterEntity
  where TMeasurement : MeasurementEntity
  where TAggregate : AggregateEntity
  where TMeasurementValidator : MeasurementValidatorEntity
{
  public virtual ICollection<TMeasurement> Measurements { get; set; } =
    default!;

  public virtual ICollection<TAggregate> Aggregates { get; set; } = default!;
}

public class
  MeterInheritedEntityTypeConfiguration :
  EntityTypeHierarchyConfiguration<MeterEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("meters")
      .HasDiscriminator<string>(nameof(MeterEntity.Kind));

    builder
      .HasOne(nameof(MeterEntity.Messenger))
      .WithMany(nameof(MessengerEntity.Meters))
      .HasForeignKey(nameof(MeterEntity.MessengerId));

    builder
      .Property(nameof(MeterEntity.ConnectionPower_W))
      .HasColumnName("connection_power_w");

    builder
      .HasOne(
        nameof(MeterEntity.MeasurementValidator))
      .WithMany(nameof(MeasurementValidatorEntity<MeterEntity>.Meters))
      .HasForeignKey("_measurementValidatorId");

    builder.Ignore(
      nameof(MeterEntity.MeasurementValidatorId));
    builder
      .Property("_measurementValidatorId")
      .HasColumnName("measurement_validator_id");

    if (entity != typeof(MeterEntity))
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
    }
  }
}
