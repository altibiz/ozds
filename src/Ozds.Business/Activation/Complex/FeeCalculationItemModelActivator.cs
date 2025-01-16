using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class FeeCalculationItemModelActivator
  : ConcreteModelActivator<FeeCalculationItemModel>
{
  public override void Initialize(
    FeeCalculationItemModel model
  )
  {
    base.Initialize(model);

    model.Amount_N = 0;
  }
}

public class UsageMeterFeeCalculationItemModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      UsageMeterFeeCalculationItemModel,
      FeeCalculationItemModel>(serviceProvider)
{
}
