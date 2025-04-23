using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Report;

public class LoadCurveReportModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<LoadCurveReportModel, ReportModel>(serviceProvider)
{
  public override void Initialize(LoadCurveReportModel model)
  {
    base.Initialize(model);

    model.MeasurementLocationCode = string.Empty;
    model.Timestamp = DateTimeOffset.UtcNow;
    model.ObisCode = string.Empty;
    model.MeterId = string.Empty;
    model.Energy_kx = default!;
    model.Power_kx = default!;
  }
}
