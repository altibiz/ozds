using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record StatefulNetworkUserInvoiceModel : IComposite
{
  public required NetworkUserInvoiceModel Invoice { get; set; }

  public required NetworkUserInvoiceStateModel? State { get; set; }
}
