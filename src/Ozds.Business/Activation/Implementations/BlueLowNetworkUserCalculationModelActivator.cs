using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class BlueLowNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      BlueLowNetworkUserCalculationModel,
      NetworkUserCalculationModel>(serviceProvider)
{
  private readonly ModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(
    BlueLowNetworkUserCalculationModel model
  )
  {
    base.Initialize(model);

    model.UsageActiveEnergyTotalImportT0 = _agnosticModelActivator
      .Activate<UsageActiveEnergyTotalImportT0CalculationItemModel>();
    model.UsageReactiveEnergyTotalRampedT0 = _agnosticModelActivator
      .Activate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>();
    model.ConcreteArchivedUsageNetworkUserCatalogue = default!;
  }
}
