using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class BlueLowCatalogueEntity : CatalogueEntity
{
  [Column("active_energy_total_import_t0_price_eur")]
  public float ActiveEnergyTotalImportT0Price_EUR { get; set; }

  [Column("active_energy_total_import_t1_price_eur")]
  public float ReactiveEnergyTotalImportT0Price_EUR { get; set; }

  [Column("meter_fee_price_eur")]
  public float MeterFeePrice_EUR { get; set; }
}
