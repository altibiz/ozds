using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class MeasurementModelActivator : ConcreteModelActivator<MeasurementModel>
{
  public override void Initialize(MeasurementModel model)
  {
    base.Initialize(model);

    var now = DateTimeOffset.UtcNow;

    model.Timestamp = now;
    model.MeterId = string.Empty;
    model.MeasurementLocationId = "0";
  }
}
