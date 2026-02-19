using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class RegulatoryCatalogueModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<RegulatoryCatalogueModel, CatalogueModel>(
    serviceProvider
  ) { }
