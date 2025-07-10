using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Validation;
using Ozds.Fake.Faking;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class NetworkUserModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<NetworkUserModel, AuditableModel>(
  serviceProvider
)
{
  private readonly ModelFaker modelFaker =
    serviceProvider.GetRequiredService<ModelFaker>();

  private readonly HtmlSanitizer htmlSanitizer =
    serviceProvider.GetRequiredService<HtmlSanitizer>();

  public override void Initialize(NetworkUserModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.LegalPerson = modelFaker
      .Fake<LegalPersonModel>();
    model.InvoiceRemark = htmlSanitizer.Sanitize(
      faker.Random.Words(
        faker.Random.Number(1, 10)));
  }
}
