using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class CumulativeAggregateMeasureEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  CumulativeAggregateMeasureModel,
  AggregateMeasureModel,
  CumulativeAggregateMeasureEntity,
  AggregateMeasureEntity>(serviceProvider)
{
}
