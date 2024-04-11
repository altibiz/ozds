using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public class NetworkUserCalculationEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  protected readonly long _measurementLocationId;

  protected readonly long _networkUserInvoiceId;

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

  public virtual NetworkUserInvoiceEntity NetworkUserInvoice { get; set; } =
    default!;

  public string NetworkUserInvoiceId
  {
    get { return _networkUserInvoiceId.ToString(); }
    init { _networkUserInvoiceId = long.Parse(value); }
  }

  public decimal Total_EUR { get; set; }

  public NetworkUserCatalogueEntity ArchivedUsageNetworkUserCatalogue { get; set; } = default!;

  public RegulatoryCatalogueEntity ArchivedSupplyRegulatoryCatalogue { get; set; } =
    default!;

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public NetworkUserMeasurementLocationEntity ArchivedMeasurementLocation
  {
    get;
    set;
  } =
    default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = value is { } nonullValue ? long.Parse(nonullValue) : default; }
  }

  public string Title { get; set; } = default!;

  protected readonly long _supplyRegulatoryCatalogueId;

  public string SupplyRegulatoryCatalogueId
  {
    get { return _supplyRegulatoryCatalogueId.ToString(); }
    init { _supplyRegulatoryCatalogueId = long.Parse(value); }
  }

  public virtual RegulatoryCatalogueEntity SupplyRegulatoryCatalogue { get; set; } = default!;
}

public class
  NetworkUserCalculationEntity<TUsageNetworkUserCatalogue> : NetworkUserCalculationEntity
  where TUsageNetworkUserCatalogue : NetworkUserCatalogueEntity
{
  protected readonly long _usageNetworkUserCatalogueId;

  public string UsageNetworkUserCatalogueId
  {
    get { return _usageNetworkUserCatalogueId.ToString(); }
    init { _usageNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual TUsageNetworkUserCatalogue UsageNetworkUserCatalogue { get; set; } = default!;
}

public class
  NetworkUserCalculationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    NetworkUserCalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    builder
      .UseTphMappingStrategy()
      .ToTable("network_user_calculations")
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
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedMeter))
      .Ignore(nameof(MeterEntity.LastUpdatedBy))
      .Ignore(nameof(MeterEntity.CreatedBy))
      .Ignore(nameof(MeterEntity.DeletedBy))
      .Ignore(nameof(MeterEntity.MeasurementLocation))
      .Ignore(nameof(MeterEntity.Messenger))
      .Ignore(nameof(MeterEntity.NetworkUserCalculations));

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedUsageNetworkUserCatalogue))
      .Ignore(nameof(NetworkUserCatalogueEntity.LastUpdatedBy))
      .Ignore(nameof(NetworkUserCatalogueEntity.CreatedBy))
      .Ignore(nameof(NetworkUserCatalogueEntity.DeletedBy))
      .Ignore(nameof(NetworkUserCatalogueEntity.Location))
      .Ignore(nameof(NetworkUserCatalogueEntity.MeasurementLocations))
      .Ignore(nameof(NetworkUserCatalogueEntity.NetworkUserCalculations));

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity.ArchivedSupplyRegulatoryCatalogue))
      .Ignore(nameof(RegulatoryCatalogueEntity.LastUpdatedBy))
      .Ignore(nameof(RegulatoryCatalogueEntity.CreatedBy))
      .Ignore(nameof(RegulatoryCatalogueEntity.DeletedBy))
      .Ignore(nameof(RegulatoryCatalogueEntity.Location))
      .Ignore(nameof(RegulatoryCatalogueEntity.NetworkUserCalculations));

    builder
      .ComplexProperty(nameof(NetworkUserCalculationEntity
        .ArchivedMeasurementLocation))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.LastUpdatedBy))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.CreatedBy))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.DeletedBy))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.NetworkUserCatalogue))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.Meter))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity.NetworkUser))
      .Ignore(nameof(NetworkUserMeasurementLocationEntity
        .NetworkUserCalculations));

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
      .Property("_networkUserId")
      .HasColumnName("network_user_id");

    builder
      .Property(nameof(NetworkUserCalculationEntity.Total_EUR))
      .HasColumnName("total_eur");

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.SupplyRegulatoryCatalogue))
      .WithMany(nameof(RegulatoryCatalogueEntity.NetworkUserCalculations))
      .HasForeignKey("_supplyRegulatoryCatalogueId");

    builder.Ignore(nameof(NetworkUserCalculationEntity.SupplyRegulatoryCatalogueId));
    builder
      .Property("_supplyRegulatoryCatalogueId")
      .HasColumnName("supply_regulatory_catalogue_id");


    if (type != typeof(NetworkUserCalculationEntity))
    {
      builder
        .HasOne(nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>.UsageNetworkUserCatalogue))
        .WithMany(nameof(NetworkUserCatalogueEntity.NetworkUserCalculations))
        .HasForeignKey("_usageNetworkUserCatalogueId");

      builder.Ignore(nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>
        .UsageNetworkUserCatalogueId));
      builder
        .Property("_usageNetworkUserCatalogueId")
        .HasColumnName("usage_network_user_catalogue_id");
    }
  }
}
