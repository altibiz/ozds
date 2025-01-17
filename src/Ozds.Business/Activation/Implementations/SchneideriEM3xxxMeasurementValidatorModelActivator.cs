using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

public class SchneideriEM3xxxMeasurementValidatorModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  SchneideriEM3xxxMeasurementValidatorModel,
  MeasurementValidatorModel>(serviceProvider)
{
  public override void Initialize(
    SchneideriEM3xxxMeasurementValidatorModel model
  )
  {
    base.Initialize(model);
    model.MinVoltage_V = 0;
    model.MaxVoltage_V = 0;
    model.MinCurrent_A = 0;
    model.MaxCurrent_A = 0;
    model.MinActivePower_W = 0;
    model.MaxActivePower_W = 0;
    model.MinReactivePower_VAR = 0;
    model.MaxReactivePower_VAR = 0;
    model.MinApparentPower_VA = 0;
    model.MaxApparentPower_VA = 0;
  }
}
