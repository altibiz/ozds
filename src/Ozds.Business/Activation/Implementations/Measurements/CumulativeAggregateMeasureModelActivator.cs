using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class CumulativeAggregateMeasureModelActivator
  : ConcreteModelActivator<CumulativeAggregateMeasureModel>
{
  public override void Initialize(
    CumulativeAggregateMeasureModel model
  )
  {
    base.Initialize(model);

    model.Min = 0;
    model.Max = 0;
  }
}
