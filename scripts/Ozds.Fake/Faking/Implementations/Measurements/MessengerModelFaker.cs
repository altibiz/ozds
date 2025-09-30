using Bogus;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Naming;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class MessengerModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<MessengerModel, TrackableModel>(serviceProvider)
{
  private readonly ModelFaker modelFaker =
    serviceProvider.GetRequiredService<ModelFaker>();

  private readonly MessengerNamingConvention namingConvention =
    serviceProvider.GetRequiredService<MessengerNamingConvention>();

  public override void Initialize(MessengerModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.Id = namingConvention.IdPrefixForMessengerType(model.GetType())
      + "-"
      + string.Join(string.Empty, faker.Random.Digits(7));

    model.MaxInactivityPeriod = modelFaker.Fake<PeriodModel>();
    model.PushDelayPeriod = modelFaker.Fake<PeriodModel>();
  }
}
