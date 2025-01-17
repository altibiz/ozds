using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Time;

namespace Ozds.Business.Activation.Implementations;

public class InvoiceModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<InvoiceModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(InvoiceModel model)
  {
    base.Initialize(model);

    var now = DateTimeOffset.UtcNow;
    var startOfLastMonth = now.GetStartOfLastMonth();
    var startOfThisMonth = now.GetStartOfMonth();

    model.Total_EUR = 0;
    model.TaxRate_Percent = 0;
    model.Tax_EUR = 0;
    model.TotalWithTax_EUR = 0;
    model.IssuedOn = now;
    model.IssuedById = default!;
    model.FromDate = startOfLastMonth;
    model.ToDate = startOfThisMonth;
  }
}
