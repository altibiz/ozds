using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class CalculationItemModelActivator
  : ConcreteModelActivator<CalculationItemModel>
{
  public override void Initialize(CalculationItemModel model)
  {
    base.Initialize(model);
    model.Price_EUR = 0;
    model.Total_EUR = 0;
  }
}
