using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

public class AbbB2xMeasurementModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<AbbB2xMeasurementModel, MeasurementModel>(
      serviceProvider
    )
{
  public override void Initialize(AbbB2xMeasurementModel model)
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
    model.ReactivePowerL1NetT0_VAR = 0;
    model.ReactivePowerL2NetT0_VAR = 0;
    model.ReactivePowerL3NetT0_VAR = 0;
    model.ActiveEnergyL1ImportT0_Wh = 0;
    model.ActiveEnergyL2ImportT0_Wh = 0;
    model.ActiveEnergyL3ImportT0_Wh = 0;
    model.ActiveEnergyL1ExportT0_Wh = 0;
    model.ActiveEnergyL2ExportT0_Wh = 0;
    model.ActiveEnergyL3ExportT0_Wh = 0;
    model.ReactiveEnergyL1ImportT0_VARh = 0;
    model.ReactiveEnergyL2ImportT0_VARh = 0;
    model.ReactiveEnergyL3ImportT0_VARh = 0;
    model.ReactiveEnergyL1ExportT0_VARh = 0;
    model.ReactiveEnergyL2ExportT0_VARh = 0;
    model.ReactiveEnergyL3ExportT0_VARh = 0;
    model.ActiveEnergyTotalImportT0_Wh = 0;
    model.ReactiveEnergyTotalImportT0_VARh = 0;
    model.ActiveEnergyTotalExportT0_Wh = 0;
    model.ReactiveEnergyTotalExportT0_VARh = 0;
    model.ActiveEnergyTotalImportT1_Wh = 0;
    model.ActiveEnergyTotalImportT2_Wh = 0;
  }
}
