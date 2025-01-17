using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class WhiteLowNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      WhiteLowNetworkUserCalculationModel,
      NetworkUserCalculationModel>(serviceProvider)
{
  private readonly ModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

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
