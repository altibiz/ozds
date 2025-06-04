using Ozds.Business.Models.Base;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations;

public class IdentifiableModelFaker(
  IServiceProvider serviceProvider
) : ConcreteModelFaker<IdentifiableModel>(serviceProvider)
{
  public override void Initialize(IdentifiableModel model, Faker faker)
  {
    model.Title = faker.Random.Word();
  }
}
