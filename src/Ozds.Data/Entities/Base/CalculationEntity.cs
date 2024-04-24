using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

// TODO: check that all base classes are only used for inheritance

namespace Ozds.Data.Entities.Base;

public class CalculationEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  protected readonly long _measurementLocationId;

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  public string MeasurementLocationId
  {
    get { return _measurementLocationId.ToString(); }
    init { _measurementLocationId = long.Parse(value); }
  }

  public decimal Total_EUR { get; set; }

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public MeasurementLocationEntity ArchivedMeasurementLocation
  {
    get;
    set;
  } =
    default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = value is { } notNullValue ? long.Parse(notNullValue) : default; }
  }

  public string Title { get; set; } = default!;
}

public class
  CalculationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    NetworkUserCalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    if (type == typeof(CalculationEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(type);

    if (type == typeof(NetworkUserCalculationEntity))
    {
      builder.HasKey("_id");
    }

    builder.Ignore(nameof(CalculationEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
      .UseIdentityAlwaysColumn();

    builder
      .HasOne(nameof(CalculationEntity.IssuedBy))
      .WithMany()
      .HasForeignKey(nameof(CalculationEntity.IssuedById));

    builder
      .HasOne(nameof(CalculationEntity.Meter))
      .WithMany(nameof(MeterEntity.Calculations))
      .HasForeignKey(nameof(CalculationEntity.MeterId));

    builder
      .HasOne(nameof(CalculationEntity.MeasurementLocation))
      .WithMany(nameof(MeasurementLocationEntity.Calculations))
      .HasForeignKey("_measurementLocationId");

    builder
      .ArchivedProperty(nameof(CalculationEntity.ArchivedMeter));

    builder
      .ArchivedProperty(nameof(CalculationEntity
        .ArchivedMeasurementLocation));

    builder
      .Property<DateTimeOffset>(nameof(CalculationEntity.IssuedOn))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder.Ignore(nameof(CalculationEntity.MeasurementLocationId));
    builder
      .Property("_measurementLocationId")
      .HasColumnName("measurement_location_id");

    builder
      .Property(nameof(CalculationEntity.Total_EUR))
      .HasColumnName("total_eur");
  }
}
