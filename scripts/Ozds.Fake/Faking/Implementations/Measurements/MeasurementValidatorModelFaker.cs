using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class MeasurementValidatorModelFaker(
  IServiceProvider serviceProvider
) : InheritingModelFaker<MeasurementValidatorModel, AuditableModel>(serviceProvider)
{
}
