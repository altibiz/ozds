using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

// TODO: add foreign keys for location and regulatory catalogue

public class NetworkUserInvoiceEntity : InvoiceEntity
{
  public string? BillId { get; set; }

  protected long _networkUserId;

  public virtual string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    set { _networkUserId = long.Parse(value); }
  }

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public NetworkUserEntity ArchivedNetworkUser { get; set; } = default!;

  protected long _locationId;

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    set { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;

  public LocationEntity ArchivedLocation { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceNotificationEntity>
    Notifications
  { get; set; } = default!;

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
    NetworkUserCalculations
  { get; set; } =
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
      .HasOne(nameof(NetworkUserInvoiceEntity.Location))
      .WithMany(nameof(LocationEntity.Invoices))
      .HasForeignKey("_locationId");

    builder.Ignore(nameof(NetworkUserInvoiceEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");

    builder
      .Ignore(nameof(NetworkUserInvoiceEntity.LocationId));

    builder
      .ArchivedProperty(nameof(NetworkUserInvoiceEntity.ArchivedLocation));

    builder
      .ArchivedProperty(
        nameof(NetworkUserInvoiceEntity
          .ArchivedRegulatoryCatalogue));
  }
}
