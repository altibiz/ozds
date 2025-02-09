using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeasurementValidatorModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  MeasurementValidatorModel,
  AuditableModel,
  MeasurementValidatorEntity,
  AuditableEntity>(serviceProvider)
{
}
