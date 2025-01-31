using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Time;

namespace Ozds.Business.Activation.Implementations.Finances;

public class CalculationModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<CalculationModel, IdentifiableModel>(
    serviceProvider
  )
{
  public override void Initialize(CalculationModel model)
  {
    base.Initialize(model);

    var now = DateTimeOffset.UtcNow;
    var startOfLastMonth = now.GetStartOfLastMonth();
    var startOfThisMonth = now.GetStartOfMonth();

    model.Total_EUR = 0;
    model.IssuedOn = now;
    model.IssuedById = default!;
    model.FromDate = startOfLastMonth;
    model.ToDate = startOfThisMonth;
  }
}
