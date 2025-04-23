using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class EnergyCardModelReportEntityConverter :
  ConcreteModelReportEntityConverter<
    EnergyCardReportModel,
    EnergyCardEntity>
{
  public override void InitializeEntity(
    EnergyCardReportModel model,
    EnergyCardEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.SocialSecurityNumber = model.SocialSecurityNumber;
    entity.NetworkUserTitle = model.NetworkUserTitle;
    entity.MeasurementLocationCode = model.MeasurementLocationCode;
    entity.TariffModel = model.TariffModel;
    entity.ConnectionPower_W = model.ConnectionPower_W;
    entity.MeasurementLocationTitle = model.MeasurementLocationTitle;
    entity.LocationTitle = model.LocationTitle;
    entity.LocationAddress = model.LocationAddress;
    entity.LocationCity = model.LocationCity;
    entity.LocationPostalCode = model.LocationPostalCode;
    entity.Year = model.Year;
    entity.BillingPeriod = model.BillingPeriod;
    entity.ActiveEnergyTotalImportT1_kWh = model.ActiveEnergyTotalImportT1_kWh;
    entity.ActiveEnergyTotalImportT2_kWh = model.ActiveEnergyTotalImportT2_kWh;
    entity.ReactiveEnergyTotalImportT0_kVARh =
      model.ReactiveEnergyTotalImportT0_kVARh;
    entity.ReactiveEnergyTotalExportT0_kVARh =
      model.ReactiveEnergyTotalExportT0_kVARh;
    entity.ActivePowerTotalImportT1_kW =
      model.ActivePowerTotalImportT1_kW;
  }

  public override void InitializeModel(
    EnergyCardEntity entity,
    EnergyCardReportModel model)
  {
    base.InitializeModel(entity, model);
    model.SocialSecurityNumber = entity.SocialSecurityNumber;
    model.NetworkUserTitle = entity.NetworkUserTitle;
    model.MeasurementLocationCode = entity.MeasurementLocationCode;
    model.TariffModel = entity.TariffModel;
    model.ConnectionPower_W = entity.ConnectionPower_W;
    model.MeasurementLocationTitle = entity.MeasurementLocationTitle;
    model.LocationTitle = entity.LocationTitle;
    model.LocationAddress = entity.LocationAddress;
    model.LocationCity = entity.LocationCity;
    model.LocationPostalCode = entity.LocationPostalCode;
    model.Year = entity.Year;
    model.BillingPeriod = entity.BillingPeriod;
    model.ActiveEnergyTotalImportT1_kWh = entity.ActiveEnergyTotalImportT1_kWh;
    model.ActiveEnergyTotalImportT2_kWh = entity.ActiveEnergyTotalImportT2_kWh;
    model.ReactiveEnergyTotalImportT0_kVARh =
      entity.ReactiveEnergyTotalImportT0_kVARh;
    model.ReactiveEnergyTotalExportT0_kVARh =
      entity.ReactiveEnergyTotalExportT0_kVARh;
    model.ActivePowerTotalImportT1_kW =
      entity.ActivePowerTotalImportT1_kW;
  }
}
