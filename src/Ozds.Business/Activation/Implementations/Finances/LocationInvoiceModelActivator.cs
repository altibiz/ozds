using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class LocationInvoiceModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<LocationInvoiceModel, InvoiceModel>(
    serviceProvider
  )
{
  public override void Initialize(LocationInvoiceModel model)
  {
    base.Initialize(model);
    model.LocationId = "0";
    model.ArchivedLocation = default!;
  }
}
