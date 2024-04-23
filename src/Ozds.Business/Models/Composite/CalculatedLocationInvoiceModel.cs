using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record CalculatedLocationInvoiceModel(
  List<INetworkUserCalculation> NetworkUserCalculations,
  LocationInvoiceModel Invoice
);
