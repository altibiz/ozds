using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculatedNetworkUserInvoiceModel(
  List<NetworkUserCalculationModel> NetworkUserCalculations,
  NetworkUserInvoiceModel Invoice
);
