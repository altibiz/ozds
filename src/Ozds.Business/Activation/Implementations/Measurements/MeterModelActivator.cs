using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class MeterModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
    MeterModel,
    AuditableModel>(serviceProvider)
{
  public override void Initialize(MeterModel model)
  {
    base.Initialize(model);
    model.ConnectionPower_W = 0;
    model.Phases = new HashSet<PhaseModel>();
    model.MessengerId = string.Empty;
    model.MeasurementValidatorId = "0";
  }
}
