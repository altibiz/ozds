using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class MeasurementValidatorModelCachingEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelCachingEntityConverter<
    MeasurementValidatorModel,
    TrackableModel,
    MeasurementValidatorEntity,
    TrackableEntity
  >(serviceProvider) { }
