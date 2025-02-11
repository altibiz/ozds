using Ozds.Document.Entities;

namespace Ozds.Document.Test.Fixtures;

public class CalculatedNetworkUserInvoiceEntityFactory
{
  public List<CalculatedNetworkUserInvoiceEntity> Create()
  {
    var fixture = new Fixture();

    fixture.Customizations.Add(
      new TypeRelay(
        typeof(NetworkUserCalculationEntity),
        typeof(BlueLowNetworkUserCalculationEntity)));
    fixture.Customizations.Add(
      new TypeRelay(
        typeof(NetworkUserCatalogueEntity),
        typeof(BlueLowNetworkUserCatalogueEntity)));

    return fixture
      .CreateMany<CalculatedNetworkUserInvoiceEntity>(
        Constants.DefaultFuzzCount)
      .ToList();
  }
}
