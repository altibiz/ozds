using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Report;

public class AccountingPeriodReportModelActivator(
  IServiceProvider serviceProvider)
  : InheritingModelActivator<AccountingPeriodReportModel, ReportModel>(
    serviceProvider)
{
  public override void Initialize(AccountingPeriodReportModel model)
  {
    base.Initialize(model);

    model.MeasurementLocationCode = string.Empty;
    model.ObisCode = string.Empty;
    model.Timestamp = DateTimeOffset.UtcNow;
    model.Value = default;
    model.Unit = string.Empty;
  }
}
