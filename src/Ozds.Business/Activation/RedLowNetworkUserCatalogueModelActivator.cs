using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class RedLowNetworkUserCatalogueModelActivator : ModelActivator<RedLowNetworkUserCatalogueModel>
{
  public override RedLowNetworkUserCatalogueModel ActivateConcrete()
  {
    return New();
  }

  public static RedLowNetworkUserCatalogueModel New()
  {
    return new RedLowNetworkUserCatalogueModel
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
      ActiveEnergyTotalImportT1Price_EUR = 0,
      ActiveEnergyTotalImportT2Price_EUR = 0,
      ActivePowerTotalImportT1Price_EUR = 0,
      ReactiveEnergyTotalRampedT0Price_EUR = 0,
      MeterFeePrice_EUR = 0
    };
  }
}
