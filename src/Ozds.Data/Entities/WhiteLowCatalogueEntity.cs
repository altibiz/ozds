using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class WhiteLowCatalogueEntity : CatalogueEntity
{
#pragma warning disable CA1707
  public float ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public float ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public float ReactiveEnergyTotalImportT0Price_EUR { get; set; }
  public float MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  WhiteLowCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  WhiteLowCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteLowCatalogueEntity> builder)
  {
    builder
      .Property(nameof(WhiteLowCatalogueEntity
        .ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(nameof(WhiteLowCatalogueEntity
        .ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(WhiteLowCatalogueEntity
        .ReactiveEnergyTotalImportT0Price_EUR))
      .HasColumnName("reactive_energy_total_import_t0_price_eur");

    builder
      .Property(nameof(WhiteLowCatalogueEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
