using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class ScopeModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<ScopeModel, TrackableModel>(
  serviceProvider
)
{
}
