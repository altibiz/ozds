using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class
  AbbB2xMeasurementValidatorModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
  AbbB2xMeasurementValidatorModel,
  MeasurementValidatorModel>(serviceProvider)
{
  public override void Initialize(AbbB2xMeasurementValidatorModel model)
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
  }
}
