using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Implementations.Administration;

public class RegisterModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<RegisterModel, TrackableModel>(
  serviceProvider
)
{
  public override void Initialize(RegisterModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.Name = faker.Random.Word();
    model.Measure = faker.PickRandom<MeasureModel>();
    model.OrderOfMagnitude = faker.PickRandom<OrderOfMagnitudeModel>();
    model.Tariff = faker.PickRandom<TariffModel>();
    model.Duplex = faker.PickRandom<DuplexModel>();
    model.Phase = faker.PickRandom<PhaseModel>();
    model.Aggregation = faker.PickRandom<AggregationModel>();
  }
}
