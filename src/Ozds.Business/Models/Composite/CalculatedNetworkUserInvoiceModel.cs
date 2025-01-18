using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculatedNetworkUserInvoiceModel
{
  public required List<NetworkUserCalculationModel> Calculations { get; set; }

  public required NetworkUserInvoiceModel Invoice { get; set; }
}
