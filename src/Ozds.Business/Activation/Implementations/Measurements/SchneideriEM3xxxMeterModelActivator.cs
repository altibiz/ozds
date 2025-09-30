using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class SchneideriEM3xxxMeterModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  SchneideriEM3xxxMeterModel,
  MeterModel>(serviceProvider)
{
}
