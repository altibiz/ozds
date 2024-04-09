using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  BlueLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<BlueLowCatalogueEntity>
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT0Min_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT0Max_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT0Amount_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT0Total_EUR { get; set; }
  public decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; }
  public decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; }
  public decimal ReactiveEnergyTotalImportT0Amount_VARh { get; set; }
  public decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; }
  public decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; }
  public decimal ReactiveEnergyTotalExportT0Amount_VARh { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Amount_VARh { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Total_EUR { get; set; }
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  BlueLowNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  BlueLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlueLowNetworkUserCalculationEntity> builder)
  {
    builder
      .Property(
        nameof(BlueLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT0Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(
        nameof(BlueLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT0Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT0Amount_Wh))
      .HasColumnName("active_energy_total_import_t2_amount_wh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT0Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT0Total_EUR))
      .HasColumnName("active_energy_total_import_t2_total_eur");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_import_t0_amount_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_export_t0_amount_varh");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalRampedT0Total_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_total_eur");

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
