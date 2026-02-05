using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class MeasurementScopeModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<MeasurementScopeModel, ScopeModel>(serviceProvider)
{
  public override void Initialize(MeasurementScopeModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.ScopeAction = faker.PickRandom<ActionModel>();
  }
}
