using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity : AuditableEntity
{
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

  public virtual ICollection<CalculationEntity> Calculations { get; set; } = default!;
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
  private readonly long _measurementValidatorId;

  public virtual ICollection<TMeasurement> Measurements { get; set; } =
    default!;

  public virtual ICollection<TAggregate> Aggregates { get; set; } = default!;

  public virtual string MeasurementValidatorId
  {
    get { return _measurementValidatorId.ToString(); }
    init { _measurementValidatorId = long.Parse(value); }
  }

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
      .Property(nameof(MeterEntity.ConnectionPower_W))
      .HasColumnName("connection_power_w");

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
