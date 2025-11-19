using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xMeterModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    AbbB2xMeterModel,
    MeterModel,
    AbbB2xMeterEntity,
    MeterEntity>(serviceProvider)
{
}
