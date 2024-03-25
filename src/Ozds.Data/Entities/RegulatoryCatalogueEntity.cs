using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RegulatoryCatalogueEntity : CatalogueEntity
{
  [Column("active_energy_total_import_t1_price_eur")]
  public float ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Column("active_energy_total_import_t2_price_eur")]
  public float ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Column("renewable_energy_fee_price_eur")]
  public float RenewableEnergyFeePrice_EUR { get; set; }

  [Column("business_usage_fee_price_eur")]
  public float BusinessUsageFeePrice_EUR { get; set; }

  [Column("tax_rate_percent")] public float TaxRate_Percent { get; set; }
}
