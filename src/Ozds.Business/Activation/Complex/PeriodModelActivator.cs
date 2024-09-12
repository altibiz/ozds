using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Complex;

public class PeriodModelActivator : ModelActivator<PeriodModel>
{
  public override PeriodModel ActivateConcrete()
  {
    return New();
  }

  public static PeriodModel New()
  {
    return new PeriodModel
    {
      Duration = DurationModel.Second,
      Multiplier = 1,
    };
  }
}
