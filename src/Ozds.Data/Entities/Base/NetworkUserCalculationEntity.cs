using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public class NetworkUserCalculationEntity : IReadonlyEntity, IIdentifiableEntity
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

  protected readonly long _networkUserInvoiceId;

  public virtual NetworkUserInvoiceEntity NetworkUserInvoice { get; set; } = default!;

  public string NetworkUserInvoiceId
  {
    get { return _networkUserInvoiceId.ToString(); }
    init { _networkUserInvoiceId = long.Parse(value); }
  }

  public decimal Total_EUR { get; set; }

  public CatalogueEntity ArchivedCatalogue { get; set; } = default!;

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public MeasurementLocationEntity ArchivedMeasurementLocation { get; set; } =
    default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = value is { } nonullValue ? long.Parse(nonullValue) : default; }
  }

  public string Title { get; set; } = default!;
}

public class NetworkUserCalculationEntity<TCatalogue> : NetworkUserCalculationEntity
  where TCatalogue : CatalogueEntity
{
  public virtual string CatalogueId { get; set; } = default!;

  public virtual TCatalogue Catalogue { get; set; } = default!;
}

public class
  NetworkUserCalculationEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration
<
  NetworkUserCalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder
      .UseTphMappingStrategy()
      .ToTable("calculations")
      .HasDiscriminator<string>("kind");

    if (type == typeof(NetworkUserCalculationEntity))
    {
      builder.HasKey("_id");
    }

    builder.Ignore(nameof(NetworkUserCalculationEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
      .UseIdentityAlwaysColumn();

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.IssuedBy))
      .WithMany()
      .HasForeignKey(nameof(NetworkUserCalculationEntity.IssuedById));

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.Meter))
      .WithMany(nameof(MeterEntity.NetworkUserCalculations))
      .HasForeignKey(nameof(NetworkUserCalculationEntity.MeterId));

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.MeasurementLocation))
      .WithMany(nameof(MeasurementLocationEntity.NetworkUserCalculations))
      .HasForeignKey("_measurementLocationId");

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.NetworkUserInvoice))
      .WithMany(nameof(InvoiceEntity.NetworkUserCalculations))
      .HasForeignKey("_networkUserId");

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedMeter));

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedCatalogue));

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedMeasurementLocation));

    builder
      .Property<DateTimeOffset>(nameof(NetworkUserCalculationEntity.IssuedOn))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder.Ignore(nameof(NetworkUserCalculationEntity.MeasurementLocationId));
    builder
      .Property("_measurementLocationId")
      .HasColumnName("measurement_location_id");

    builder.Ignore(nameof(NetworkUserCalculationEntity.NetworkUserInvoiceId));
    builder
      .Property("_networkUserInvoiceId")
      .HasColumnName("network_user_invoice_id");

    builder
      .Property(nameof(NetworkUserCalculationEntity.Total_EUR))
      .HasColumnName("total_eur");


    if (type != typeof(NetworkUserCalculationEntity))
    {
      builder
        .HasOne(nameof(NetworkUserCalculationEntity<CatalogueEntity>.Catalogue))
        .WithMany(nameof(CatalogueEntity.NetworkUserCalculations))
        .HasForeignKey(nameof(NetworkUserCalculationEntity<CatalogueEntity>.CatalogueId));
    }
  }
}
