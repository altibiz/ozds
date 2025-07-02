using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations.Finances;

public class RegulatoryCatalogueModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<RegulatoryCatalogueModel, CatalogueModel>(
  serviceProvider)
{
}
