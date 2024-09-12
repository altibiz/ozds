using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class BlueLowNetworkUserCatalogueModel : NetworkUserCatalogueModel
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal MeterFeePrice_EUR { get; set; }
}
