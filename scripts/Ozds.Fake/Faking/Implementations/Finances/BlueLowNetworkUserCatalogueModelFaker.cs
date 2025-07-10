using Bogus;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class BlueLowNetworkUserCatalogueModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<BlueLowNetworkUserCatalogueModel,
  NetworkUserCatalogueModel>(
  serviceProvider
)
{
  public override void Initialize(
    BlueLowNetworkUserCatalogueModel model,
    Faker faker)
  {
    base.Initialize(model, faker);

    model.ActiveEnergyTotalImportT0Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
    model.ReactiveEnergyTotalRampedT0Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
  }
}
