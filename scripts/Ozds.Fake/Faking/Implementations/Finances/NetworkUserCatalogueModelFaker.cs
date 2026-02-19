using Bogus;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class NetworkUserCatalogueModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<NetworkUserCatalogueModel, CatalogueModel>(
    serviceProvider
  )
{
  public override void Initialize(NetworkUserCatalogueModel model, Faker faker)
  {
    base.Initialize(model, faker);

    model.MeterFeePrice_EUR = decimal.Round(
      faker.Random.Decimal(uint.MinValue, uint.MaxValue),
      2
    );
  }
}
