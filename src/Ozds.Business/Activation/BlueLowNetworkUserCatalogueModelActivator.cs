using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class BlueLowNetworkUserCatalogueModelActivator : ModelActivator<BlueLowNetworkUserCatalogueModel>
{
  public override BlueLowNetworkUserCatalogueModel ActivateConcrete()
  {
    return New();
  }

  public static BlueLowNetworkUserCatalogueModel New()
  {
    return new BlueLowNetworkUserCatalogueModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      ActiveEnergyTotalImportT0Price_EUR = 0,
      ReactiveEnergyTotalRampedT0Price_EUR = 0,
      MeterFeePrice_EUR = 0
    };
  }
}
