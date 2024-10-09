using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity : AuditableEntity, IMeterEntity
{
  private readonly long _measurementValidatorId;
  protected readonly string _stringId = default!;

  public override string Id
  {
    get { return _stringId; }
    init { _stringId = value; }
  }

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
    init { _measurementValidatorId = long.Parse(value); }
  }

  public virtual MeasurementValidatorEntity MeasurementValidator { get; set; } =
    default!;
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

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
  public virtual TMeasurementValidator MeasurementValidator { get; set; } =
    default!;
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
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
      .HasDiscriminator<string>("kind");

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
