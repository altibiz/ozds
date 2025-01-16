using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation;

public class WhiteLowNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      WhiteLowNetworkUserCalculationModel,
      NetworkUserCalculationModel>(serviceProvider)
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(WhiteLowNetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.UsageActiveEnergyTotalImportT1 = _agnosticModelActivator
      .Activate<UsageActiveEnergyTotalImportT1CalculationItemModel>();
    model.UsageActiveEnergyTotalImportT2 = _agnosticModelActivator
      .Activate<UsageActiveEnergyTotalImportT2CalculationItemModel>();
    model.UsageReactiveEnergyTotalRampedT0 = _agnosticModelActivator
      .Activate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>();
    model.ConcreteArchivedUsageNetworkUserCatalogue = default!;
  }
}
