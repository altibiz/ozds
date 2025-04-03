using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record StatefulNetworkUserInvoiceModel
{
  public required NetworkUserInvoiceModel Invoice { get; set; }

  public required NetworkUserInvoiceStateModel? State { get; set; }
}
