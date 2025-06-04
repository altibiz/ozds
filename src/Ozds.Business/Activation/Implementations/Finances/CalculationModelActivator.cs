using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Finances;

public class CalculationModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clockQueries,
  TimeQueries timeQueries
)
  : InheritingModelActivator<CalculationModel, IdentifiableModel>(
    serviceProvider
  )
{
  public override void Initialize(CalculationModel model)
  {
    base.Initialize(model);

    var now = clockQueries.Timestamp();
    var startOfLastMonth = timeQueries.GetStartOfLastMonth(now);
    var startOfThisMonth = timeQueries.GetStartOfMonth(now);

    model.Total_EUR = 0;
    model.IssuedOn = now;
    model.IssuedById = default!;
    model.FromDate = startOfLastMonth;
    model.ToDate = startOfThisMonth;
  }
}
