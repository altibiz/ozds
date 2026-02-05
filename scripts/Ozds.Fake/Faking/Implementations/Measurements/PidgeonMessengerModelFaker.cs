using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class PidgeonMessengerModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<PidgeonMessengerModel, MessengerModel>(
    serviceProvider
  ) { }
