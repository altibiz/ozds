using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class WhiteMediumCatalogueEntity : CatalogueEntity
{
  [Column("active_energy_total_import_t1_price_eur")]
  public float ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Column("active_energy_total_import_t2_price_eur")]
  public float ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Column("max_active_power_total_import_t1_price_eur")]
  public float MaxActivePowerTotalImportT1Price_EUR { get; set; }

  [Column("reactive_energy_total_import_t0_price_eur")]
  public float ReactiveEnergyTotalImportT0Price_EUR { get; set; }

  [Column("meter_fee_price_eur")]
  public float MeterFeePrice_EUR { get; set; }
}
