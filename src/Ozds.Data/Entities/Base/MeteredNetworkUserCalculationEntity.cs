using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeteredNetworkUserCalculationEntity
  : NetworkUserCalculationEntity, IMeteredNetworkUserCalculationEntity
{
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

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }
}

public class
  MeteredNetworkUserCalculationEntity<TUsageNetworkUserCatalogue> :
  MeteredNetworkUserCalculationEntity
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
  MeteredNetworkUserCalculationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    MeteredNetworkUserCalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.ComplexProperty(
        nameof(MeteredNetworkUserCalculationEntity.UsageMeterFee))
      .UsageMeterFeeCalculationItem();

    builder.ComplexProperty(
        nameof(MeteredNetworkUserCalculationEntity
          .SupplyActiveEnergyTotalImportT1))
      .SupplyActiveEnergyTotalImportT1CalculationItem();

    builder.ComplexProperty(
        nameof(MeteredNetworkUserCalculationEntity
          .SupplyActiveEnergyTotalImportT2))
      .SupplyActiveEnergyTotalImportT2CalculationItem();

    builder.ComplexProperty(
        nameof(MeteredNetworkUserCalculationEntity
          .SupplyBusinessUsageFee))
      .SupplyBusinessUsageCalculationItem();

    builder.ComplexProperty(
        nameof(MeteredNetworkUserCalculationEntity
          .SupplyRenewableEnergyFee))
      .SupplyRenewableEnergyCalculationItem();

    builder
      .MonetaryValue(
        nameof(MeteredNetworkUserCalculationEntity.UsageFeeTotal_EUR),
        "usage_fee_total_eur"
      );

    builder
      .MonetaryValue(
        nameof(MeteredNetworkUserCalculationEntity
          .SupplyFeeTotal_EUR),
        "supply_fee_total_eur"
      );

    if (entity != typeof(MeteredNetworkUserCalculationEntity))
    {
      builder
        .HasOne(
          nameof(MeteredNetworkUserCalculationEntity<NetworkUserCatalogueEntity>
            .UsageNetworkUserCatalogue))
        .WithMany(
          nameof(NetworkUserCatalogueEntity<MeteredNetworkUserCalculationEntity>
            .NetworkUserCalculations))
        .HasForeignKey("_usageNetworkUserCatalogueId");

      builder.Ignore(
        nameof(MeteredNetworkUserCalculationEntity<NetworkUserCatalogueEntity>
          .UsageNetworkUserCatalogueId));
      builder
        .Property("_usageNetworkUserCatalogueId")
        .HasColumnName("usage_network_user_catalogue_id");

      builder
        .ArchivedProperty(
          nameof(MeteredNetworkUserCalculationEntity<NetworkUserCatalogueEntity>
            .ArchivedUsageNetworkUserCatalogue));
    }
  }
}
