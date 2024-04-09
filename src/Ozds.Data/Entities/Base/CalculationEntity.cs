using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public class CalculationEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = value is { } nonullValue ? long.Parse(nonullValue) : default; }
  }

  public string Title { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  protected readonly long _measurementLocationId;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } = default!;

  public string MeasurementLocationId
  {
    get { return _measurementLocationId.ToString(); }
    init { _measurementLocationId = long.Parse(value); }
  }

  protected readonly long _invoiceId;

  public virtual InvoiceEntity Invoice { get; set; } = default!;

  public string InvoiceId
  {
    get { return _invoiceId.ToString(); }
    init { _invoiceId = long.Parse(value); }
  }

  public decimal Total_EUR { get; set; }

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public MeasurementLocationEntity ArchivedMeasurementLocation { get; set; } = default!;
}

public class CalculationEntity<TCatalogue> : CalculationEntity where TCatalogue : CatalogueEntity
{
  public virtual string CatalogueId { get; set; } = default!;

  public virtual TCatalogue Catalogue { get; set; } = default!;
}

public class
  CalculationEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  CalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder
      .UseTphMappingStrategy()
      .ToTable("calculations")
      .HasDiscriminator<string>("kind");

    builder.HasKey("_id");
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
      .HasOne(nameof(CalculationEntity.Invoice))
      .WithMany(nameof(InvoiceEntity.Calculations))
      .HasForeignKey("_invoice");

    builder
      .ComplexProperty(nameof(CalculationEntity.ArchivedMeter));

    builder
      .ComplexProperty(nameof(CalculationEntity.ArchivedMeasurementLocation));

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


    if (type != typeof(CalculationEntity))
    {
      builder
        .HasOne(nameof(CalculationEntity<CatalogueEntity>.Catalogue))
        .WithMany(nameof(CatalogueEntity.Calculations))
        .HasForeignKey(nameof(CalculationEntity<CatalogueEntity>.CatalogueId));
    }
  }
}
