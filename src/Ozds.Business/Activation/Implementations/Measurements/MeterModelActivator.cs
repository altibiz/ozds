using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Naming;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class MeterModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
    MeterModel,
    AuditableModel>(serviceProvider)
{
  private readonly MeterNamingConvention meterNamingConvention =
    serviceProvider.GetRequiredService<MeterNamingConvention>();

  public override void Initialize(MeterModel model)
  {
    base.Initialize(model);
    model.ConnectionPower_W = 0;
    model.Phases = new HashSet<PhaseModel>();
    model.MessengerId = null;
    model.MeasurementValidatorId = "0";
    model.Id = meterNamingConvention.IdPrefixForMeterType(model.GetType());
  }
}
