using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models;

public class BlueLowNetworkUserCatalogueModel : NetworkUserCatalogueModel
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }

  public override IEnumerable<ObisModel> Obis
  {
    get
    {
      return
      [
        ObisModel.ActiveEnergyTotalImportT0_kWh,
        ObisModel.ReactiveEnergyTotalImportT0_kVARh,
        ObisModel.ReactiveEnergyTotalExportT0_kVARh
      ];
    }
  }
}
