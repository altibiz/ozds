using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.Finances;

public class InvoiceModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clock,
  TimeQueries timeQueries
) : InheritingModelActivator<InvoiceModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(InvoiceModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();
    var startOfLastMonth = timeQueries.GetStartOfLastMonth(now);
    var startOfThisMonth = timeQueries.GetStartOfMonth(now);

    model.Total_EUR = 0;
    model.InvoiceTaxRate_Percent = 0;
    model.InvoiceTax_EUR = 0;
    model.InvoiceTotalWithTax_EUR = 0;
    model.IssuedOn = now;
    model.IssuedById = default!;
    model.FromDate = startOfLastMonth;
    model.ToDate = startOfThisMonth;
  }
}
