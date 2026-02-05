using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class WhiteMediumNetworkUserCatalogueModelFaker(
  IServiceProvider serviceProvider
)
  : InheritingModelFaker<
    WhiteMediumNetworkUserCatalogueModel,
    NetworkUserCatalogueModel
  >(serviceProvider)
{
  public override void Initialize(
    WhiteMediumNetworkUserCatalogueModel model,
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
    model.ActivePowerTotalImportT1Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2
    );
  }
}
