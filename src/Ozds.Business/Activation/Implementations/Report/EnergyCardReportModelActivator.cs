using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Report;

public class EnergyCardReportModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<EnergyCardReportModel, ReportModel>(
    serviceProvider
  )
{
  public override void Initialize(EnergyCardReportModel model)
  {
    base.Initialize(model);

    model.SocialSecurityNumber = string.Empty;
    model.NetworkUserTitle = string.Empty;
    model.MeasurementLocationCode = string.Empty;
    model.TariffModel = string.Empty;
    model.ConnectionPower_W = default;
    model.MeasurementLocationTitle = string.Empty;
    model.LocationTitle = string.Empty;
    model.LocationAddress = string.Empty;
    model.LocationCity = string.Empty;
    model.LocationPostalCode = string.Empty;
    model.Year = string.Empty;
    model.BillingPeriod = string.Empty;
    model.ActiveEnergyTotalImportT1_kWh = default;
    model.ActiveEnergyTotalImportT2_kWh = default;
    model.ReactiveEnergyTotalImportT0_kVARh = default;
    model.ReactiveEnergyTotalExportT0_kVARh = default;
    model.ActivePowerTotalImportT1_kW = default;
  }
}
