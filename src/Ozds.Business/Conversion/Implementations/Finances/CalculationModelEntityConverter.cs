using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculationModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      CalculationModel,
      IdentifiableModel,
      CalculationEntity,
      IdentifiableEntity>(serviceProvider)
{
}
