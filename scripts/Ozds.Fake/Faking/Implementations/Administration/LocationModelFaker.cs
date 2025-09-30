using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Fake.Faking;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class LocationModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<LocationModel, TrackableModel>(
  serviceProvider
)
{
  private readonly ModelFaker modelFaker =
    serviceProvider.GetRequiredService<ModelFaker>();

  public override void Initialize(LocationModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.LegalPerson = modelFaker
      .Fake<LegalPersonModel>();
  }
}
