using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RegulatoryCatalogueEntity : CatalogueEntity
{
  public virtual ICollection<LocationEntity> Locations { get; set; } = default!;

  public virtual ICollection<NetworkUserCalculationEntity>
    NetworkUserCalculations { get; set; } =
    default!;

#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public decimal RenewableEnergyFeePrice_EUR { get; set; }
  public decimal BusinessUsageFeePrice_EUR { get; set; }
  public decimal TaxRate_Percent { get; set; }
#pragma warning restore CA1707
}

public class
  RegulatoryCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  RegulatoryCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<RegulatoryCatalogueEntity> builder)
  {
    builder
      .ToTable("regulatory_catalogues");

    builder
      .Property(
        nameof(RegulatoryCatalogueEntity
          .ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(
        nameof(RegulatoryCatalogueEntity
          .ActiveEnergyTotalImportT2Price_EUR))
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
