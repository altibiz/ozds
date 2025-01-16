using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class SchneideriEM3xxxMeterModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  SchneideriEM3xxxMeterModel,
  MeterModel>(serviceProvider)
{
  public override void Initialize(SchneideriEM3xxxMeterModel model)
  {
    base.Initialize(model);
    model.MeasurementValidatorId = default!;
  }
}
