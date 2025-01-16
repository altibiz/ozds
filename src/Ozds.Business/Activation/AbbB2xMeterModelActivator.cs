using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class AbbB2xMeterModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<AbbB2xMeterModel, MeterModel>(serviceProvider)
{
  public override void Initialize(AbbB2xMeterModel model)
  {
    base.Initialize(model);

    model.MeasurementValidatorId = "0";
  }
}
