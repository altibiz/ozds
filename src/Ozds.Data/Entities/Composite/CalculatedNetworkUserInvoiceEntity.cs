using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public class CalculatedNetworkUserInvoiceEntity
{
  public List<NetworkUserCalculationEntity> Calculations { get; set; }
    = default!;

  public NetworkUserInvoiceEntity Invoice { get; set; } = default!;
}
