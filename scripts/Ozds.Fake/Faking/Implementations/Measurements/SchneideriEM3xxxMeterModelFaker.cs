using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class SchneideriEM3xxxMeterModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<SchneideriEM3xxxMeterModel, MeterModel>(
  serviceProvider)
{
}
