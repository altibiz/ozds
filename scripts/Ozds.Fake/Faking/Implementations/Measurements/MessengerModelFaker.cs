using Bogus;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class MessengerModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<MessengerModel, AuditableModel>(serviceProvider)
{
  private readonly ModelFaker modelFaker =
    serviceProvider.GetRequiredService<ModelFaker>();

  public override void Initialize(MessengerModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.Id = faker.Random.Uuid().ToString();

    model.MaxInactivityPeriod = modelFaker.Fake<PeriodModel>();
    model.PushDelayPeriod = modelFaker.Fake<PeriodModel>();
  }
}
