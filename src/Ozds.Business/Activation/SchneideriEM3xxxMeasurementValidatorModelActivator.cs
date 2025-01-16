using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

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
    model.MinVoltage_V = default;
    model.MaxVoltage_V = default;
    model.MinCurrent_A = default;
    model.MaxCurrent_A = default;
    model.MinActivePower_W = default;
    model.MaxActivePower_W = default;
    model.MinReactivePower_VAR = default;
    model.MaxReactivePower_VAR = default;
    model.MinApparentPower_VA = default;
    model.MaxApparentPower_VA = default;
  }
}
