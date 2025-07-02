using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class MeasurementModelActivator(ClockQueries clock)
  : ConcreteModelActivator<MeasurementModel>
{
  public override void Initialize(MeasurementModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.Timestamp = now;
    model.MeterId = string.Empty;
    model.MeasurementLocationId = "0";
  }
}
