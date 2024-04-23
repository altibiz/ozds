using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

// TODO: interface
// TODO: check that all base classes are only used for inheritance

namespace Ozds.Data.Entities.Base;

public class NetworkUserCalculationEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  protected readonly long _measurementLocationId;

  protected readonly long _networkUserInvoiceId;

  protected readonly long _supplyRegulatoryCatalogueId;

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

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }

  public decimal Total_EUR { get; set; }

  public RegulatoryCatalogueEntity ArchivedSupplyRegulatoryCatalogue
  {
    get;
    set;
  } =
    default!;

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public NetworkUserMeasurementLocationEntity ArchivedMeasurementLocation
  {
    get;
    set;
  } =
    default!;

  public string SupplyRegulatoryCatalogueId
  {
    get { return _supplyRegulatoryCatalogueId.ToString(); }
    init { _supplyRegulatoryCatalogueId = long.Parse(value); }
  }

  public virtual RegulatoryCatalogueEntity SupplyRegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  public UsageMeterFeeCalculationItemEntity UsageMeterFee { get; set; } =
    default!;

  public SupplyActiveEnergyTotalImportT1CalculationItemEntity
    SupplyActiveEnergyTotalImportT1 { get; set; } = default!;

  public SupplyActiveEnergyTotalImportT2CalculationItemEntity
    SupplyActiveEnergyTotalImportT2 { get; set; } = default!;

  public SupplyBusinessUsageFeeCalculationItemEntity SupplyBusinessUsageFee
  {
    get;
    set;
  } =
    default!;

  public SupplyRenewableEnergyFeeCalculationItemEntity SupplyRenewableEnergyFee
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
}

public class
  NetworkUserCalculationEntity<TUsageNetworkUserCatalogue> :
  NetworkUserCalculationEntity
  where TUsageNetworkUserCatalogue : NetworkUserCatalogueEntity
{
  protected readonly long _usageNetworkUserCatalogueId;

  public string UsageNetworkUserCatalogueId
  {
    get { return _usageNetworkUserCatalogueId.ToString(); }
    init { _usageNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual TUsageNetworkUserCatalogue UsageNetworkUserCatalogue
  {
    get;
    set;
  } = default!;

  public TUsageNetworkUserCatalogue ArchivedUsageNetworkUserCatalogue
  {
    get;
    set;
  } = default!;
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
      .HasForeignKey("_networkUserInvoiceId");

    builder
      .ArchivedProperty(nameof(NetworkUserCalculationEntity.ArchivedMeter));

    builder
      .ArchivedProperty(nameof(NetworkUserCalculationEntity
        .ArchivedSupplyRegulatoryCatalogue));

    builder
      .ArchivedProperty(nameof(NetworkUserCalculationEntity
        .ArchivedMeasurementLocation));

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

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.SupplyRegulatoryCatalogue))
      .WithMany(nameof(RegulatoryCatalogueEntity.NetworkUserCalculations))
      .HasForeignKey("_supplyRegulatoryCatalogueId");

    builder.Ignore(nameof(NetworkUserCalculationEntity
      .SupplyRegulatoryCatalogueId));
    builder
      .Property("_supplyRegulatoryCatalogueId")
      .HasColumnName("supply_regulatory_catalogue_id");

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity.UsageMeterFee))
      .UsageMeterFeeCalculationItem();

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity
        .SupplyActiveEnergyTotalImportT1))
      .SupplyActiveEnergyTotalImportT1CalculationItem();

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity
        .SupplyActiveEnergyTotalImportT2))
      .SupplyActiveEnergyTotalImportT2CalculationItem();

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity
        .SupplyBusinessUsageFee))
      .SupplyBusinessUsageCalculationItem();

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity
        .SupplyRenewableEnergyFee))
      .SupplyRenewableEnergyCalculationItem();

    if (type != typeof(NetworkUserCalculationEntity))
    {
      builder
        .HasOne(nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>
          .UsageNetworkUserCatalogue))
        .WithMany(nameof(NetworkUserCatalogueEntity.NetworkUserCalculations))
        .HasForeignKey("_usageNetworkUserCatalogueId");

      builder.Ignore(
        nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>
          .UsageNetworkUserCatalogueId));
      builder
        .Property("_usageNetworkUserCatalogueId")
        .HasColumnName("usage_network_user_catalogue_id");

      builder
        .ArchivedProperty(
          nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>
            .ArchivedUsageNetworkUserCatalogue));
    }
  }
}
