using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class NetworkUserCalculationEntity
  : CalculationEntity, INetworkUserCalculationEntity
{
  protected long _networkUserInvoiceId;
  protected long _networkUserMeasurementLocationId;

  protected long _supplyRegulatoryCatalogueId;

  public virtual NetworkUserInvoiceEntity NetworkUserInvoice { get; set; } =
    default!;

  public RegulatoryCatalogueEntity ArchivedSupplyRegulatoryCatalogue
  {
    get;
    set;
  } =
    default!;

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

  public SupplyBusinessUsageCalculationItemEntity SupplyBusinessUsageFee
  {
    get;
    set;
  } =
    default!;

  public SupplyRenewableEnergyCalculationItemEntity SupplyRenewableEnergyFee
  {
    get;
    set;
  } =
    default!;

  public virtual NetworkUserMeasurementLocationEntity
    NetworkUserMeasurementLocation { get; set; } =
    default!;

  public NetworkUserMeasurementLocationEntity
    ArchivedNetworkUserMeasurementLocation { get; set; } =
    default!;

  public string Kind { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public string NetworkUserInvoiceId
  {
    get { return _networkUserInvoiceId.ToString(); }
    set { _networkUserInvoiceId = long.Parse(value); }
  }

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }

  public string SupplyRegulatoryCatalogueId
  {
    get { return _supplyRegulatoryCatalogueId.ToString(); }
    set { _supplyRegulatoryCatalogueId = long.Parse(value); }
  }

  public string NetworkUserMeasurementLocationId
  {
    get { return _networkUserMeasurementLocationId.ToString(); }
    set { _networkUserMeasurementLocationId = long.Parse(value); }
  }
}

public class
  NetworkUserCalculationEntity<TUsageNetworkUserCatalogue> :
  NetworkUserCalculationEntity
  where TUsageNetworkUserCatalogue : NetworkUserCatalogueEntity
{
  protected long _usageNetworkUserCatalogueId;

  public string UsageNetworkUserCatalogueId
  {
    get { return _usageNetworkUserCatalogueId.ToString(); }
    set { _usageNetworkUserCatalogueId = long.Parse(value); }
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
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("network_user_calculations")
      .HasDiscriminator<string>(nameof(NetworkUserCalculationEntity.Kind));

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.NetworkUserInvoice))
      .WithMany(nameof(NetworkUserInvoiceEntity.NetworkUserCalculations))
      .HasForeignKey("_networkUserInvoiceId");

    builder
      .HasOne(
        nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation))
      .WithMany(
        nameof(NetworkUserMeasurementLocationEntity
          .NetworkUserCalculations))
      .HasForeignKey("_networkUserMeasurementLocationId");

    builder
      .ArchivedProperty(
        nameof(NetworkUserCalculationEntity
          .ArchivedNetworkUserMeasurementLocation));

    builder.Ignore(
      nameof(NetworkUserCalculationEntity
        .NetworkUserMeasurementLocationId));
    builder
      .Property("_networkUserMeasurementLocationId")
      .HasColumnName("network_user_measurement_location_id");

    builder
      .ArchivedProperty(
        nameof(NetworkUserCalculationEntity
          .ArchivedSupplyRegulatoryCatalogue));

    builder.Ignore(nameof(NetworkUserCalculationEntity.NetworkUserInvoiceId));
    builder
      .Property("_networkUserInvoiceId")
      .HasColumnName("network_user_invoice_id");

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.SupplyRegulatoryCatalogue))
      .WithMany(nameof(RegulatoryCatalogueEntity.NetworkUserCalculations))
      .HasForeignKey("_supplyRegulatoryCatalogueId");

    builder.Ignore(
      nameof(NetworkUserCalculationEntity
        .SupplyRegulatoryCatalogueId));
    builder
      .Property("_supplyRegulatoryCatalogueId")
      .HasColumnName("supply_regulatory_catalogue_id");

    builder.ComplexProperty(nameof(NetworkUserCalculationEntity.UsageMeterFee))
      .UsageMeterFeeCalculationItem();

    builder.ComplexProperty(
        nameof(NetworkUserCalculationEntity
          .SupplyActiveEnergyTotalImportT1))
      .SupplyActiveEnergyTotalImportT1CalculationItem();

    builder.ComplexProperty(
        nameof(NetworkUserCalculationEntity
          .SupplyActiveEnergyTotalImportT2))
      .SupplyActiveEnergyTotalImportT2CalculationItem();

    builder.ComplexProperty(
        nameof(NetworkUserCalculationEntity
          .SupplyBusinessUsageFee))
      .SupplyBusinessUsageCalculationItem();

    builder.ComplexProperty(
        nameof(NetworkUserCalculationEntity
          .SupplyRenewableEnergyFee))
      .SupplyRenewableEnergyCalculationItem();

    builder
      .HasOne(nameof(NetworkUserCalculationEntity.Meter))
      .WithMany(nameof(MeterEntity.NetworkUserCalculations))
      .HasForeignKey(nameof(NetworkUserCalculationEntity.MeterId));

    builder
      .ArchivedProperty(nameof(NetworkUserCalculationEntity.ArchivedMeter));

    if (entity != typeof(NetworkUserCalculationEntity))
    {
      builder
        .HasOne(
          nameof(NetworkUserCalculationEntity<NetworkUserCatalogueEntity>
            .UsageNetworkUserCatalogue))
        .WithMany(
          nameof(NetworkUserCatalogueEntity<NetworkUserCalculationEntity>
            .NetworkUserCalculations))
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
