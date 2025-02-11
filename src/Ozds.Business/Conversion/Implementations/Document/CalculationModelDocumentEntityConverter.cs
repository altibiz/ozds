using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class CalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  CalculationModel,
  FinancialModel,
  CalculationEntity,
  FinancialEntity>(serviceProvider)
{
}
