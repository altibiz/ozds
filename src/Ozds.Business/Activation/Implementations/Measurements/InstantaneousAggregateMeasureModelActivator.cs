using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class InstantaneousAggregateMeasureModelActivator(ClockQueries clock)
  : ConcreteModelActivator<InstantaneousAggregateMeasureModel>
{
  public override void Initialize(
    InstantaneousAggregateMeasureModel model
  )
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.Avg = 0;
    model.Min = 0;
    model.Max = 0;
    model.MinTimestamp = now;
    model.MaxTimestamp = now;
  }
}
