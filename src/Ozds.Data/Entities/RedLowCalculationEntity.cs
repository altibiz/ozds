using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RedLowCalculationEntity : CalculationEntity<RedLowCatalogueEntity>
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

public class RedLowCalculationEntityTypeConfiguration : EntityTypeConfiguration<RedLowCalculationEntity>
{
  public override void Configure(EntityTypeBuilder<RedLowCalculationEntity> builder)
  {
    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT1Min_Wh))
      .HasColumnName("active_energy_total_import_t1_min_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT1Max_Wh))
      .HasColumnName("active_energy_total_import_t1_max_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT1Amount_Wh))
      .HasColumnName("active_energy_total_import_t1_amount_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT1Total_EUR))
      .HasColumnName("active_energy_total_import_t1_total_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT2Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT2Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT2Amount_Wh))
      .HasColumnName("active_energy_total_import_t2_amount_wh");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ActiveEnergyTotalImportT2Total_EUR))
      .HasColumnName("active_energy_total_import_t2_total_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ActivePowerTotalImportT1Peak_W))
      .HasColumnName("max_active_power_total_import_t1_peak_w");

    builder
      .Property(nameof(RedLowCalculationEntity.ActivePowerTotalImportT1Amount_W))
      .HasColumnName("max_active_power_total_import_t1_amount_w");

    builder
      .Property(nameof(RedLowCalculationEntity.ActivePowerTotalImportT1Price_EUR))
      .HasColumnName("max_active_power_total_import_t1_price_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ActivePowerTotalImportT1Total_EUR))
      .HasColumnName("max_active_power_total_import_t1_total_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalImportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_import_t0_amount_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalExportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_export_t0_amount_varh");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.ReactiveEnergyTotalRampedT0Total_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_total_eur");

    builder
      .Property(nameof(RedLowCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
