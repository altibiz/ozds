using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

// TODO: add foreign keys for location and regulatory catalogue

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  protected long _networkUserId;
  public string? BillId { get; set; }

  public virtual string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    set { _networkUserId = long.Parse(value); }
  }

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public NetworkUserEntity ArchivedNetworkUser { get; set; } = default!;

  public LocationEntity ArchivedLocation { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceNotificationEntity>
    Notifications { get; set; } = default!;

  public RegulatoryCatalogueEntity ArchivedRegulatoryCatalogue { get; set; } =
    default!;

  public decimal UsageActiveEnergyTotalImportT0Fee_EUR { get; set; }

  public decimal UsageActiveEnergyTotalImportT1Fee_EUR { get; set; }

  public decimal UsageActiveEnergyTotalImportT2Fee_EUR { get; set; }

  public decimal UsageActivePowerTotalImportT1PeakFee_EUR { get; set; }

  public decimal UsageReactiveEnergyTotalRampedT0Fee_EUR { get; set; }

  public decimal UsageMeterFee_EUR { get; set; }

  public decimal UsageFeeTotal_EUR { get; set; }

  public decimal SupplyActiveEnergyTotalImportT1Fee_EUR { get; set; }

  public decimal SupplyActiveEnergyTotalImportT2Fee_EUR { get; set; }

  public decimal SupplyBusinessUsageFee_EUR { get; set; }

  public decimal SupplyRenewableEnergyFee_EUR { get; set; }

  public decimal SupplyFeeTotal_EUR { get; set; }

  public virtual ICollection<NetworkUserCalculationEntity>
    NetworkUserCalculations { get; set; } =
    default!;
}

public class
  NetworkUserInvoiceEntityTypeConfiguration : EntityTypeConfiguration<
  NetworkUserInvoiceEntity>
{
  public override void Configure(
    EntityTypeBuilder<NetworkUserInvoiceEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserInvoiceEntity.NetworkUser))
      .WithMany(nameof(NetworkUserEntity.Invoices))
      .HasForeignKey("_networkUserId");

    builder.Ignore(nameof(NetworkUserInvoiceEntity.NetworkUserId));
    builder
      .Property("_networkUserId")
      .HasColumnName("network_user_id");

    builder
      .ArchivedProperty(nameof(NetworkUserInvoiceEntity.ArchivedNetworkUser));

    builder
      .ArchivedProperty(nameof(NetworkUserInvoiceEntity.ArchivedLocation));

    builder
      .ArchivedProperty(
        nameof(NetworkUserInvoiceEntity
          .ArchivedRegulatoryCatalogue));
  }
}
