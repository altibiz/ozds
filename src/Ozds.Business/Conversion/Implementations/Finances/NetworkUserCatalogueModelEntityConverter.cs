using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NetworkUserCatalogueModel,
      CatalogueModel,
      NetworkUserCatalogueEntity,
      CatalogueEntity>(serviceProvider)
{
}
