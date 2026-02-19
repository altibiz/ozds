using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class WhiteLowNetworkUserCatalogueModelFaker(
  IServiceProvider serviceProvider
)
  : InheritingModelFaker<
    WhiteLowNetworkUserCatalogueModel,
    NetworkUserCatalogueModel
  >(serviceProvider)
{
  public override void Initialize(
    WhiteLowNetworkUserCatalogueModel model,
    Faker faker
  )
  {
    base.Initialize(model, faker);

    model.ActiveEnergyTotalImportT1Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2
    );
    model.ActiveEnergyTotalImportT2Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2
    );
    model.ReactiveEnergyTotalRampedT0Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2
    );
  }
}
