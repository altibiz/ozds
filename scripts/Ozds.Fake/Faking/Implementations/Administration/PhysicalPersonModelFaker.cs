using Bogus;
using Ozds.Business.Models.Complex;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Administration;

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
