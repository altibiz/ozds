using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class AbbB2xMeasurementValidatorModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<AbbB2xMeasurementValidatorModel,
  MeasurementValidatorModel>(serviceProvider)
{
  public override void Initialize(
    AbbB2xMeasurementValidatorModel model,
    Faker faker)
  {
    base.Initialize(model, faker);

    model.MinVoltage_V = faker.Random.Decimal(0, 200);
    model.MaxVoltage_V = faker.Random.Decimal(model.MinVoltage_V, 240 * 10);
    model.MinCurrent_A = faker.Random.Decimal(0, 10);
    model.MaxCurrent_A = faker.Random.Decimal(model.MinCurrent_A, 1000 * 10);
    model.MinActivePower_W = faker.Random.Decimal(0, 1000);
    model.MaxActivePower_W =
      faker.Random.Decimal(model.MinActivePower_W, 1000 * 10);
    model.MinReactivePower_VAR = faker.Random.Decimal(0, 1000);
    model.MaxReactivePower_VAR = faker.Random.Decimal(
      model.MinReactivePower_VAR, 1000 * 10);
  }
}
