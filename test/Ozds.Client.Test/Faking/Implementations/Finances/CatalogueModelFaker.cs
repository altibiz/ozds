using Ozds.Business.Models.Base;
using Ozds.Client.Test.Faking.Base;

namespace Ozds.Client.Test.Faking.Implementations.Finances;

public class CatalogueModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<CatalogueModel, AuditableModel>(serviceProvider)
{
}
