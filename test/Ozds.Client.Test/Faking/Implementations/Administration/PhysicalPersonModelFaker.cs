using Ozds.Business.Models.Complex;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations.Administration;

public class PhysicalPersonModelFaker(IServiceProvider serviceProvider)
  : ConcreteModelFaker<PhysicalPersonModel>(serviceProvider)
{
  public override void Initialize(PhysicalPersonModel model, Faker faker)
  {
    model.Name = faker.Random.Word();
    model.Email = faker.Internet.Email();
    model.PhoneNumber = faker.Phone.PhoneNumber();
  }
}
