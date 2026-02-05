using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Validation;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class NetworkUserMeasurementLocationModelFaker(
  IServiceProvider serviceProvider
)
  : InheritingModelFaker<
    NetworkUserMeasurementLocationModel,
    MeasurementLocationModel
  >(serviceProvider)
{
  private readonly HtmlSanitizer htmlSanitizer =
    serviceProvider.GetRequiredService<HtmlSanitizer>();

  public override void Initialize(
    NetworkUserMeasurementLocationModel model,
    Faker faker
  )
  {
    base.Initialize(model, faker);

    model.CalculationRemark = htmlSanitizer.Sanitize(
      faker.Random.Words(faker.Random.Number(1, 10))
    );
  }
}
