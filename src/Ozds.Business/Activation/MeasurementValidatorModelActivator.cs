using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class MeasurementValidatorModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  MeasurementValidatorModel,
  AuditableModel>(serviceProvider)
{
}
