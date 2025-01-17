using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Finances;

public class ActivePowerTotalImportT1PeakCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      ActivePowerTotalImportT1PeakCalculationItemModel,
      CalculationItemModel>(serviceProvider)
{
  public override void Initialize(
    ActivePowerTotalImportT1PeakCalculationItemModel model
  )
  {
    base.Initialize(model);

    model.Peak_kW = 0;
    model.Amount_kW = 0;
  }
}

public class UsageActivePowerTotalImportT1PeakCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageActivePowerTotalImportT1PeakCalculationItemModel,
      ActivePowerTotalImportT1PeakCalculationItemModel>(serviceProvider)
{
}
