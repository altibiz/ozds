using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class AbbB2xMeterModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<AbbB2xMeterModel, MeterModel>(serviceProvider)
{
}
