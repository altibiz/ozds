namespace Ozds.Document.Entities;

public class CalculatedNetworkUserInvoiceEntity
{
  public List<NetworkUserCalculationEntity> Calculations { get; set; } =
    default!;

  public NetworkUserInvoiceEntity Invoice { get; set; } = default!;
}
