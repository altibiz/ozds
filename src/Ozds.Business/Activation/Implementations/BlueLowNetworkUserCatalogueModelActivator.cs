using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

public class BlueLowNetworkUserCatalogueModelActivator(
  IServiceProvider serviceProvider) : InheritingModelActivator<
  BlueLowNetworkUserCatalogueModel,
  NetworkUserCatalogueModel>(serviceProvider)
{
  public override void Initialize(BlueLowNetworkUserCatalogueModel model)
  {
    base.Initialize(model);
    model.ActiveEnergyTotalImportT0Price_EUR = 0;
    model.ReactiveEnergyTotalRampedT0Price_EUR = 0;
    model.MeterFeePrice_EUR = 0;
  }
}
