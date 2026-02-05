using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class AggregateModelActivator(ClockQueries clock, TimeQueries time)
  : ConcreteModelActivator<AggregateModel>
{
  public override void Initialize(AggregateModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();
    var quarterHour = time.GetStartOfQuarterHour(now);

    model.Interval = IntervalModel.QuarterHour;
    model.Timestamp = quarterHour;
    model.MeterId = string.Empty;
    model.MeasurementLocationId = "0";
  }
}
