using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class AbbB2xAggregateModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<AbbB2xAggregateModel, AggregateModel>(
      serviceProvider
    )
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(AbbB2xAggregateModel model)
  {
    base.Initialize(model);

    model.VoltageL1AnyT0_V = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.VoltageL2AnyT0_V = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.VoltageL3AnyT0_V = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.CurrentL1AnyT0_A = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.CurrentL2AnyT0_A = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.CurrentL3AnyT0_A = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActivePowerL1NetT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActivePowerL2NetT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActivePowerL3NetT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactivePowerL1NetT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactivePowerL2NetT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactivePowerL3NetT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL1ImportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL1ImportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL2ImportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL2ImportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL3ImportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL3ImportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL1ExportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL1ExportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL2ExportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL2ExportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyL3ExportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerL3ExportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL1ImportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL1ImportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL2ImportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL2ImportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL3ImportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL3ImportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL1ExportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL1ExportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL2ExportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL2ExportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyL3ExportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerL3ExportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyTotalImportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerTotalImportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyTotalExportT0_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerTotalExportT0_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyTotalImportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerTotalImportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ReactiveEnergyTotalExportT0_VARh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedReactivePowerTotalExportT0_VAR = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyTotalImportT1_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerTotalImportT1_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
    model.ActiveEnergyTotalImportT2_Wh = _agnosticModelActivator
      .Activate<CumulativeAggregateMeasureModel>();
    model.DerivedActivePowerTotalImportT2_W = _agnosticModelActivator
      .Activate<InstantaneousAggregateMeasureModel>();
  }
}
