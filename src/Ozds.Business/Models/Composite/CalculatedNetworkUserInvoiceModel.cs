using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record CalculatedNetworkUserInvoiceModel(
  List<INetworkUserCalculation> Calculations,
  NetworkUserInvoiceModel Invoice
);
