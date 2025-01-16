using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class LocationInvoiceModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<LocationInvoiceModel, InvoiceModel>(
    serviceProvider
  )
{
  public override void Initialize(LocationInvoiceModel model)
  {
    base.Initialize(model);
    model.LocationId = default!;
    model.ArchivedLocation = default!;
  }
}
