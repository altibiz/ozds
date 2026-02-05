using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class AbbB2xAggregateModelReportEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelReportEntityConverter<
    AbbB2xAggregateModel,
    AggregateModel,
    AbbB2xAggregateEntity,
    AggregateEntity
  >(serviceProvider)
{
#pragma warning disable S1185 // Overriding members should do more than simply call the same member in the base class
  public override void InitializeEntity(
    AbbB2xAggregateModel model,
    AbbB2xAggregateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ActiveEnergyTotalImportT0_Wh = model
      .ActiveEnergy_Wh.TariffUnary()
      .DuplexImport()
      .PhaseSum();
    entity.ActiveEnergyTotalImportT1_Wh = model
      .ActiveEnergy_Wh.TariffBinary()
      .T1.DuplexImport()
      .PhaseSum();
    entity.ActiveEnergyTotalImportT2_Wh = model
      .ActiveEnergy_Wh.TariffBinary()
      .T2.DuplexImport()
      .PhaseSum();
    entity.ReactiveEnergyTotalImportT0_VARh = model
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexImport()
      .PhaseSum();
    entity.ReactiveEnergyTotalExportT0_VARh = model
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexExport()
      .PhaseSum();
    entity.MaxActivePowerTotalNetT1_W = model
      .ActivePower_W.TariffBinary()
      .T1.DuplexImport()
      .AggregateMax()
      .PhaseSum();
  }

  public override void InitializeModel(
    AbbB2xAggregateEntity entity,
    AbbB2xAggregateModel model
  )
  {
    base.InitializeModel(entity, model);
  }
#pragma warning restore S1185 // Overriding members should do more than simply call the same member in the base class
}
