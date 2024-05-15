using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class AcknowledgeNetworkUserInvoiceItem(
  string Name,
  string Unit,
  string Ordinal,
  decimal Quantity,
  decimal Price_EUR
) : IAcknowledgeNetworkUserInvoiceItem;

public record class AcknowledgeNetworkUserInvoice(
  string NetworkUserInvoiceId,
  string SubProjectCode,
  DateTimeOffset InvoiceDate,
  IAcknowledgeNetworkUserInvoiceItem[] Items,
  decimal Total_EUR,
  decimal TaxRate_Percent,
  decimal Tax_EUR,
  decimal TotalWithTax_EUR
) : IAcknowledgeNetworkUserInvoice;
