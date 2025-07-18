using Bogus;
using Ozds.Business.Models.Complex;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Administration;

public class LegalPersonModelFaker(IServiceProvider serviceProvider)
  : ConcreteModelFaker<LegalPersonModel>(serviceProvider)
{
  public override void Initialize(LegalPersonModel model, Faker faker)
  {
    model.Name = faker.Random.Word();
    model.SocialSecurityNumber = string.Join("", faker.Random.Digits(11));
    model.Address = faker.Random.Word();
    model.PostalCode = string.Join("", faker.Random.Digits(5));
    model.City = faker.Random.Word();
    model.Email = faker.Internet.Email();
    model.PhoneNumber = faker.Phone.PhoneNumber();
  }
}
