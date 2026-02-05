using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.Administration;

public class MeasurementScopeModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<MeasurementScopeModel, ScopeModel>(serviceProvider)
{
  public override void Initialize(MeasurementScopeModel model)
  {
    base.Initialize(model);

    model.Interval = IntervalModel.QuarterHour;
  }
}
