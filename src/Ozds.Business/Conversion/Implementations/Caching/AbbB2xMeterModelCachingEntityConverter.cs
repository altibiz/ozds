using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class AbbB2xMeterModelCachingEntityConverter(
  IServiceProvider serviceProvider)
  : InheritingModelCachingEntityConverter<
    AbbB2xMeterModel,
    MeterModel,
    AbbB2xMeterEntity,
    MeterEntity>(serviceProvider)
{
}
