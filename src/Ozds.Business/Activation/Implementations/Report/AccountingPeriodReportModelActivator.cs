using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Report;

public class AccountingPeriodReportModelActivator(
  ClockQueries clock,
  IServiceProvider serviceProvider
)
  : InheritingModelActivator<AccountingPeriodReportModel, ReportModel>(
    serviceProvider
  )
{
  public override void Initialize(AccountingPeriodReportModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.MeasurementLocationCode = string.Empty;
    model.ObisCode = string.Empty;
    model.Timestamp = now;
    model.Value = default;
    model.Unit = string.Empty;
  }
}
