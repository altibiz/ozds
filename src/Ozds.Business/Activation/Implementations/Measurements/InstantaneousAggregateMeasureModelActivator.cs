using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class InstantaneousAggregateMeasureModelActivator
  : ConcreteModelActivator<InstantaneousAggregateMeasureModel>
{
  public override void Initialize(
    InstantaneousAggregateMeasureModel model
  )
  {
    base.Initialize(model);

    var now = DateTimeOffset.UtcNow;

    model.Avg = 0;
    model.Min = 0;
    model.Max = 0;
    model.MinTimestamp = now;
    model.MaxTimestamp = now;
  }
}
