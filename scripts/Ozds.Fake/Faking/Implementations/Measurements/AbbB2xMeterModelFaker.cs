using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class AbbB2xMeterModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<AbbB2xMeterModel, MeterModel>(serviceProvider) { }
