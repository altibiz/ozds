using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RedLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<RedLowCatalogueEntity>
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT1Min_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT1Max_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT1Amount_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT1Total_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Min_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT2Max_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT2Amount_Wh { get; set; }
  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Total_EUR { get; set; }
  public decimal ActivePowerTotalImportT1Peak_W { get; set; }
  public decimal ActivePowerTotalImportT1Amount_W { get; set; }
  public decimal ActivePowerTotalImportT1Price_EUR { get; set; }
  public decimal ActivePowerTotalImportT1Total_EUR { get; set; }
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
  RedLowNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  RedLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<RedLowNetworkUserCalculationEntity> builder)
  {
    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT1Min_Wh))
      .HasColumnName("active_energy_total_import_t1_min_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT1Max_Wh))
      .HasColumnName("active_energy_total_import_t1_max_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT1Amount_Wh))
      .HasColumnName("active_energy_total_import_t1_amount_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT1Total_EUR))
      .HasColumnName("active_energy_total_import_t1_total_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT2Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT2Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT2Amount_Wh))
      .HasColumnName("active_energy_total_import_t2_amount_wh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT2Total_EUR))
      .HasColumnName("active_energy_total_import_t2_total_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.ActivePowerTotalImportT1Peak_W))
      .HasColumnName("max_active_power_total_import_t1_peak_w");

    builder
      .Property(
        nameof(RedLowNetworkUserCalculationEntity.ActivePowerTotalImportT1Amount_W))
      .HasColumnName("max_active_power_total_import_t1_amount_w");

    builder
      .Property(
        nameof(RedLowNetworkUserCalculationEntity.ActivePowerTotalImportT1Price_EUR))
      .HasColumnName("max_active_power_total_import_t1_price_eur");

    builder
      .Property(
        nameof(RedLowNetworkUserCalculationEntity.ActivePowerTotalImportT1Total_EUR))
      .HasColumnName("max_active_power_total_import_t1_total_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalImportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_import_t0_amount_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalExportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_export_t0_amount_varh");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity
        .ReactiveEnergyTotalRampedT0Total_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_total_eur");

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
