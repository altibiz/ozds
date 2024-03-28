using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RegulatoryCatalogueEntity : CatalogueEntity
{
#pragma warning disable CA1707
  public float ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public float ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public float RenewableEnergyFeePrice_EUR { get; set; }
  public float BusinessUsageFeePrice_EUR { get; set; }
  public float TaxRate_Percent { get; set; }
#pragma warning restore CA1707
}

public class RegulatoryCatalogueEntityTypeConfiguration : EntityTypeConfiguration<RegulatoryCatalogueEntity>
{
  public override void Configure(EntityTypeBuilder<RegulatoryCatalogueEntity> builder)
  {
    builder
      .Property(nameof(RegulatoryCatalogueEntity.ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(nameof(RegulatoryCatalogueEntity.ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(RegulatoryCatalogueEntity.RenewableEnergyFeePrice_EUR))
      .HasColumnName("renewable_energy_fee_price_eur");

    builder
      .Property(nameof(RegulatoryCatalogueEntity.BusinessUsageFeePrice_EUR))
      .HasColumnName("business_usage_fee_price_eur");

    builder
      .Property(nameof(RegulatoryCatalogueEntity.TaxRate_Percent))
      .HasColumnName("tax_rate_percent");
  }
}
