using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations.Finances;

public class RedLowNetworkUserCatalogueModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<RedLowNetworkUserCatalogueModel,
  NetworkUserCatalogueModel>(
  serviceProvider
)
{
  public override void Initialize(
    RedLowNetworkUserCatalogueModel model,
    Faker faker)
  {
    base.Initialize(model, faker);

    model.ActiveEnergyTotalImportT1Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
    model.ActiveEnergyTotalImportT2Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
    model.ActivePowerTotalImportT1Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
    model.ReactiveEnergyTotalRampedT0Price_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2);
  }
}
