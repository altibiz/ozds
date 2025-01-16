using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class AggregateModelActivator : ConcreteModelActivator<AggregateModel>
{
  public override void Initialize(AggregateModel model)
  {
    base.Initialize(model);

    model.Interval = IntervalModel.QuarterHour;
    model.Timestamp = DateTimeOffset.UtcNow;
    model.MeterId = default!;
    model.MeasurementLocationId = default!;
  }
}
