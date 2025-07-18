using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Finances;

public class CatalogueModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<CatalogueModel, AuditableModel>(serviceProvider)
{
}
