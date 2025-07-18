using Bogus;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations;

public class IdentifiableModelFaker(
  IServiceProvider serviceProvider
) : ConcreteModelFaker<IdentifiableModel>(serviceProvider)
{
  public override void Initialize(IdentifiableModel model, Faker faker)
  {
    model.Title = faker.Random.Word();
  }
}
