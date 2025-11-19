using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class MeterModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
    MeterModel,
    TrackableModel>(serviceProvider)
{
  private readonly MeterNamingConvention meterNamingConvention =
    serviceProvider.GetRequiredService<MeterNamingConvention>();

  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(MeterModel model)
  {
    base.Initialize(model);
    model.ConnectionPower_W = 0;
    model.Phases = new HashSet<PhaseModel>();
    model.MessengerId = null;
    model.MeasurementValidatorId = "0";
    model.Id = model.GetType() == typeof(MeterModel)
      ? ""
      : meterNamingConvention.IdPrefixForMeterType(model.GetType());
    model.MaxInactivityPeriod = modelActivator.Activate<PeriodModel>();
  }
}
