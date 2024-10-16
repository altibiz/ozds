using Ozds.Business.Models.Abstractions;

// TODO: to base model not abstraction

namespace Ozds.Business.Models.Composite;

public record CalculatedNetworkUserInvoiceModel(
  List<INetworkUserCalculation> Calculations,
  NetworkUserInvoiceModel Invoice
);
