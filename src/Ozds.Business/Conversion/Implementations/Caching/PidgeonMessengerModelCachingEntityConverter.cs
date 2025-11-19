using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class PidgeonMessengerModelCachingEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelCachingEntityConverter<
  PidgeonMessengerModel,
  MessengerModel,
  PidgeonMessengerEntity,
  MessengerEntity>(serviceProvider)
{
}
