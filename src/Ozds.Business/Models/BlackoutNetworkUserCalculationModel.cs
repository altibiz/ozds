using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class BlackoutNetworkUserCalculationModel : NetworkUserCalculationModel
{
  [Required]
  public required NetworkUserCatalogueModel
    ConcreteArchivedUsageNetworkUserCatalogue { get; set; } = default!;

  public override NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue
  {
    get { return ConcreteArchivedUsageNetworkUserCatalogue; }
  }
}
