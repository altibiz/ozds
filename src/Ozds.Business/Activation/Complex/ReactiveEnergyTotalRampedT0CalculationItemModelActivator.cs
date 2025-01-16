using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class ReactiveEnergyTotalRampedT0CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      ReactiveEnergyTotalRampedT0CalculationItemModel,
      CalculationItemModel>(serviceProvider)
{
  public override void Initialize(
    ReactiveEnergyTotalRampedT0CalculationItemModel model
  )
  {
    base.Initialize(model);
    model.ReactiveImportMin_kVARh = 0;
    model.ReactiveImportMax_kVARh = 0;
    model.ReactiveImportAmount_kVARh = 0;
    model.ReactiveExportMin_kVARh = 0;
    model.ReactiveExportMax_kVARh = 0;
    model.ReactiveExportAmount_kVARh = 0;
    model.ActiveImportMin_kWh = 0;
    model.ActiveImportMax_kWh = 0;
    model.ActiveImportAmount_kWh = 0;
    model.Amount_kVARh = 0;
  }
}

public class UsageReactiveEnergyTotalRampedT0CalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageReactiveEnergyTotalRampedT0CalculationItemModel,
      ReactiveEnergyTotalRampedT0CalculationItemModel>(serviceProvider)
{
}
