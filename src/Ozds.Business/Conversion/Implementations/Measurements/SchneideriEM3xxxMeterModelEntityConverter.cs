using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class SchneideriEM3xxxMeterModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SchneideriEM3xxxMeterModel,
      MeterModel,
      SchneideriEM3xxxMeterEntity,
      MeterEntity>(serviceProvider)
{
}
