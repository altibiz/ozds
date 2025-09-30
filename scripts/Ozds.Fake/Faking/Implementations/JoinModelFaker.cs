using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations;

public class JoinModelFaker(
  IServiceProvider serviceProvider
) : ConcreteModelFaker<JoinModel>(serviceProvider)
{
}
