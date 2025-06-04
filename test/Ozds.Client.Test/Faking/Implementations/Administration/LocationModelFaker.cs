using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Client.Test.Faking;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Implementations.Administration;

public class LocationModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<LocationModel, AuditableModel>(
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
