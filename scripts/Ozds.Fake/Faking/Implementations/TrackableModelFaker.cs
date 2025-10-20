using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations;

public class TrackableModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<TrackableModel, IdentifiableModel>(serviceProvider)
{
}
