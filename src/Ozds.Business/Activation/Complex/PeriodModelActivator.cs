using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Complex;

public class PeriodModelActivator : ConcreteModelActivator<PeriodModel>
{
  public override void Initialize(PeriodModel model)
  {
    model.Duration = DurationModel.Second;
    model.Multiplier = 1;
  }
}
