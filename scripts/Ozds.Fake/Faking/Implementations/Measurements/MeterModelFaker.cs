using Bogus;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class MeterModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<MeterModel, TrackableModel>(serviceProvider)
{
  private readonly MeterNamingConvention meterNamingConvention =
    serviceProvider.GetRequiredService<MeterNamingConvention>();

  private readonly ModelFaker modelFaker =
    serviceProvider.GetRequiredService<ModelFaker>();

  public override void Initialize(MeterModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.ConnectionPower_W = faker.Random.Decimal(0, 1000);
    model.Phases = faker
      .Random.ArrayElements(
        [PhaseModel.L1, PhaseModel.L2, PhaseModel.L3],
        faker.Random.Number(1, 3)
      )
      .ToHashSet();
    model.Id =
      meterNamingConvention.IdPrefixForMeterType(model.GetType())
      + "-"
      + string.Join(string.Empty, faker.Random.Digits(7));
    model.MaxInactivityPeriod = modelFaker.Fake<PeriodModel>();
  }
}
