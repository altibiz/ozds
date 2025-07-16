using Bogus;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class PeriodModelFaker(IServiceProvider serviceProvider)
  : ConcreteModelFaker<PeriodModel>(serviceProvider)
{
  public override void Initialize(PeriodModel model, Faker faker)
  {
    model.Duration = faker.PickRandom<DurationModel>();
    model.Multiplier = model.Duration switch
    {
      DurationModel.Second => faker.Random.UInt(1, 60),
      DurationModel.Minute => faker.Random.UInt(1, 60),
      DurationModel.Hour => faker.Random.UInt(1, 24),
      DurationModel.Day => faker.Random.UInt(1, 7),
      DurationModel.Week => faker.Random.UInt(1, 52),
      DurationModel.Month => faker.Random.UInt(1, 12),
      DurationModel.Year => faker.Random.UInt(1, 100),
      _ => throw new NotImplementedException()
    };
  }
}
