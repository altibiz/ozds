using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CatalogueModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    CatalogueModel,
    AuditableModel,
    CatalogueEntity,
    AuditableEntity>(serviceProvider)
{
}
