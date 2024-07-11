using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

public interface IAcknowledgeNetworkUserInvoiceItem
{
  public string Name { get; }
  public string Unit { get; }
  public string Ordinal { get; }
  public decimal Quantity { get; }
  public decimal Price_EUR { get; }
}

[MessageUrn("acknowledge-network-user-invoice")]
public interface IAcknowledgeNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
  public string SubProjectCode { get; }
  public DateTimeOffset InvoiceDate { get; }
  public IAcknowledgeNetworkUserInvoiceItem[] Items { get; }
  public decimal Total_EUR { get; }
  public decimal TaxRate_Percent { get; }
  public decimal Tax_EUR { get; }
  public decimal TotalWithTax_EUR { get; }
}
