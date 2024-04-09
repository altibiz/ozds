using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  BlueLowCalculationEntity : CalculationEntity<BlueLowCatalogueEntity>
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
  BlueLowCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  BlueLowCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlueLowCalculationEntity> builder)
  {
    builder
      .Property(
        nameof(BlueLowCalculationEntity.ActiveEnergyTotalImportT0Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(
        nameof(BlueLowCalculationEntity.ActiveEnergyTotalImportT0Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ActiveEnergyTotalImportT0Amount_Wh))
      .HasColumnName("active_energy_total_import_t2_amount_wh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ActiveEnergyTotalImportT0Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ActiveEnergyTotalImportT0Total_EUR))
      .HasColumnName("active_energy_total_import_t2_total_eur");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalImportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_import_t0_amount_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalExportT0Amount_VARh))
      .HasColumnName("reactive_energy_total_export_t0_amount_varh");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(BlueLowCalculationEntity
        .ReactiveEnergyTotalRampedT0Total_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_total_eur");

    builder
      .Property(nameof(BlueLowCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
