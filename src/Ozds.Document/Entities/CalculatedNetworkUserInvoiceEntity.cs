namespace Ozds.Document.Entities;

public record CalculatedNetworkUserInvoiceEntity
{
  public List<NetworkUserCalculationEntity> Calculations { get; set; } = default!;

  public NetworkUserInvoiceEntity Invoice { get; set; } = default!;
}
