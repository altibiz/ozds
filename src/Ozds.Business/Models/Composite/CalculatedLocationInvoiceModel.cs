using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record CalculatedLoactionInvoiceModel(
  List<INetworkUserCalculation> NetworkUserCalculations,
  LocationInvoiceModel Invoice
);
