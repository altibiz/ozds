using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class SchneideriEM3xxxMeasurementModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      SchneideriEM3xxxMeasurementModel,
      MeasurementModel>(serviceProvider)
{
  public override void Initialize(SchneideriEM3xxxMeasurementModel model)
  {
    base.Initialize(model);

    model.VoltageL1AnyT0_V = 0;
    model.VoltageL2AnyT0_V = 0;
    model.VoltageL3AnyT0_V = 0;
    model.CurrentL1AnyT0_A = 0;
    model.CurrentL2AnyT0_A = 0;
    model.CurrentL3AnyT0_A = 0;
    model.ActivePowerL1NetT0_W = 0;
    model.ActivePowerL2NetT0_W = 0;
    model.ActivePowerL3NetT0_W = 0;
    model.ReactivePowerTotalNetT0_VAR = 0;
    model.ApparentPowerTotalNetT0_VA = 0;
    model.ActiveEnergyL1ImportT0_Wh = 0;
    model.ActiveEnergyL2ImportT0_Wh = 0;
    model.ActiveEnergyL3ImportT0_Wh = 0;
    model.ActiveEnergyTotalImportT0_Wh = 0;
    model.ReactiveEnergyTotalImportT0_VARh = 0;
    model.ActiveEnergyTotalExportT0_Wh = 0;
    model.ReactiveEnergyTotalExportT0_VARh = 0;
    model.ActiveEnergyTotalImportT1_Wh = 0;
    model.ActiveEnergyTotalImportT2_Wh = 0;
  }
}
